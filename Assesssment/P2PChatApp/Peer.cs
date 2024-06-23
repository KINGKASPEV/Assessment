using System.Net;
using System.Net.Sockets;
using System.Text;

namespace P2PChatApp
{
    public class Peer
    {
        private TcpListener _listener;
        private List<TcpClient> _clients = new List<TcpClient>();

        public Peer(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
        }

        public void StartListening()
        {
            try
            {
                _listener.Start();
                Console.WriteLine("Listening for connections...");

                Task.Run(async () =>
                {
                    while (true)
                    {
                        try
                        {
                            var client = await _listener.AcceptTcpClientAsync();
                            _clients.Add(client);
                            Console.WriteLine("Client connected.");
                            HandleClient(client);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error accepting client: {ex.Message}");
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting listener: {ex.Message}");
            }
        }

        public async Task ConnectToPeer(string ipAddress, int port)
        {
            try
            {
                var client = new TcpClient();
                await client.ConnectAsync(ipAddress, port);
                _clients.Add(client);
                Console.WriteLine("Connected to peer.");
                HandleClient(client);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to peer: {ex.Message}");
            }
        }

        private async void HandleClient(TcpClient client)
        {
            var buffer = new byte[1024];
            var stream = client.GetStream();

            while (true)
            {
                try
                {
                    var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        Console.WriteLine("Client disconnected.");
                        _clients.Remove(client);
                        client.Close();
                        break;
                    }

                    var message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Received: {message}");
                    BroadcastMessage(message, client);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error handling client: {ex.Message}");
                    _clients.Remove(client);
                    client.Close();
                    break;
                }
            }
        }

        public void SendMessage(string message)
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            foreach (var client in _clients)
            {
                try
                {
                    var stream = client.GetStream();
                    stream.Write(buffer, 0, buffer.Length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending message to client: {ex.Message}");
                }
            }
        }

        private void BroadcastMessage(string message, TcpClient excludeClient)
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            foreach (var client in _clients)
            {
                if (client != excludeClient)
                {
                    try
                    {
                        var stream = client.GetStream();
                        stream.Write(buffer, 0, buffer.Length);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error broadcasting message to client: {ex.Message}");
                    }
                }
            }
        }
    }
}

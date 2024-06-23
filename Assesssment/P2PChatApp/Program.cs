using P2PChatApp;

Console.WriteLine("Enter the port you want to use (e.g., 5000):");
string? portInput = Console.ReadLine();
if (string.IsNullOrEmpty(portInput) || !int.TryParse(portInput, out int port) || port < 1024 || port > 65535)
{
    Console.WriteLine("Invalid port number. Please enter a number between 1024 and 65535.");
    return;
}

var peer = new Peer(port);
peer.StartListening();

Console.WriteLine("Do you want to connect to another peer? (yes/no)");
string? response = Console.ReadLine();

if (response?.ToLower() == "yes")
{
    Console.WriteLine("Enter peer IP address:");
    string? ipAddress = Console.ReadLine();
    if (string.IsNullOrEmpty(ipAddress))
    {
        Console.WriteLine("Invalid IP address.");
        return;
    }

    Console.WriteLine("Enter peer port:");
    string? peerPortInput = Console.ReadLine();
    if (string.IsNullOrEmpty(peerPortInput) || !int.TryParse(peerPortInput, out int peerPort) || peerPort < 1024 || peerPort > 65535)
    {
        Console.WriteLine("Invalid peer port number. Please enter a number between 1024 and 65535.");
        return;
    }

    await peer.ConnectToPeer(ipAddress, peerPort);
}

Console.WriteLine("You can start chatting now...");

while (true)
{
    string? message = Console.ReadLine();
    if (!string.IsNullOrEmpty(message))
    {
        peer.SendMessage(message);
    }
}

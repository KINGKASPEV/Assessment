// See https://aka.ms/new-console-template for more information
using System.Net.Http.Json;
using TemperatureServer.Models;
class Program
{
    private static readonly string apiUrl = "https://localhost:7255/api/temperature";
    private static readonly Random random = new Random();
    private static readonly int retryAttempts = 3;
    private static readonly TimeSpan delayBetweenRetries = TimeSpan.FromSeconds(5);

    static async Task Main(string[] args)
    {
        using var httpClient = new HttpClient();

        while (true)
        {
            var temperature = random.NextDouble() * 100;
            var timestamp = DateTime.UtcNow;

            var temperatureData = new TemperatureData
            {
                Timestamp = timestamp,
                Temperature = temperature
            };

            HttpResponseMessage response = null;

            for (int attempt = 1; attempt <= retryAttempts; attempt++)
            {
                try
                {
                    response = await httpClient.PostAsJsonAsync(apiUrl, temperatureData);
                    response.EnsureSuccessStatusCode(); 

                    Console.WriteLine($"Temperature data sent: {temperatureData.Temperature} °C at {timestamp}");
                    break;
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Failed to send temperature data on attempt {attempt}. Exception: {ex.Message}");

                    if (attempt < retryAttempts)
                    {
                        await Task.Delay(delayBetweenRetries);
                    }
                    else
                    {
                        Console.WriteLine($"All retry attempts exhausted. Exiting program.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                    return;
                }
            }

            await Task.Delay(5000); 
        }
    }
}

namespace NetworkDevicesApp
{
    public class Router : NetworkDevice
    {
        public int NumberOfPorts { get; set; }

        public Router(string ipAddress, string macAddress, int numberOfPorts)
            : base(ipAddress, macAddress)
        {
            NumberOfPorts = numberOfPorts;
        }

        public override void Connect()
        {
            Console.WriteLine($"Router with IP {IPAddress} and MAC {MACAddress} is connecting with {NumberOfPorts} ports.");
        }
    }
}

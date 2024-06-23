namespace NetworkDevicesApp
{
    public class Switch : NetworkDevice
    {
        public int NumberOfPorts { get; set; }

        public Switch(string ipAddress, string macAddress, int numberOfPorts)
            : base(ipAddress, macAddress)
        {
            NumberOfPorts = numberOfPorts;
        }

        public override void Connect()
        {
            Console.WriteLine($"Switch with IP {IPAddress} and MAC {MACAddress} is connecting with {NumberOfPorts} ports.");
        }
    }
}

namespace NetworkDevicesApp
{
    public class AccessPoint : NetworkDevice
    {
        public string SSID { get; set; }

        public AccessPoint(string ipAddress, string macAddress, string ssid)
            : base(ipAddress, macAddress)
        {
            SSID = ssid;
        }

        public override void Connect()
        {
            Console.WriteLine($"AccessPoint with IP {IPAddress}, MAC {MACAddress}, and SSID {SSID} is connecting.");
        }
    }
}

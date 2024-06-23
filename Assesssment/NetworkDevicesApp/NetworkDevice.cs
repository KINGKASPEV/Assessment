namespace NetworkDevicesApp
{
    public abstract class NetworkDevice
    {
        public string IPAddress { get; set; }
        public string MACAddress { get; set; }

        public NetworkDevice(string ipAddress, string macAddress)
        {
            IPAddress = ipAddress;
            MACAddress = macAddress;
        }

        public abstract void Connect();
    }
}

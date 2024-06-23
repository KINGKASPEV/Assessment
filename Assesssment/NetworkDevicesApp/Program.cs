// See https://aka.ms/new-console-template for more information
using NetworkDevicesApp;

Router router = new Router("192.168.1.1", "00:0a:95:9d:68:16", 4);
Switch swtch = new Switch("192.168.1.2", "00:0a:95:9d:68:17", 24);
AccessPoint ap = new AccessPoint("192.168.1.3", "00:0a:95:9d:68:18", "MyWiFi");

router.Connect();
swtch.Connect();
ap.Connect();
using System.Net.NetworkInformation;

namespace IdleWatch;

public static class NetworkUsage
{
    public static float GetNetworkDownloadSpeedKbps(string adapterName)
    {
        var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
        NetworkInterface selectedInterface = null;

        // Find the correct network interface by its name
        foreach (var networkInterface in networkInterfaces)
            if (networkInterface.Name.Equals(adapterName, StringComparison.OrdinalIgnoreCase))
            {
                selectedInterface = networkInterface;
                break;
            }

        if (selectedInterface == null) throw new ArgumentException("Adapter not found: " + adapterName);

        var interfaceStats1 = selectedInterface.GetIPv4Statistics();
        var bytesReceived1 = interfaceStats1.BytesReceived;

        // Wait for 1 second to measure the change
        Thread.Sleep(1000);

        var interfaceStats2 = selectedInterface.GetIPv4Statistics();
        var bytesReceived2 = interfaceStats2.BytesReceived;

        // Calculate difference and convert to kilobits per second
        var bytesReceived = bytesReceived2 - bytesReceived1;
        var downloadSpeedKbps = bytesReceived * 8f / 1000f; // Convert to Kbps

        return downloadSpeedKbps;
    }

    internal static List<string> ListAvailableNetworkAdapters()
    {
        var availableNetworkAdapters = new List<string>();
        var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
        Console.WriteLine("Available Network Adapters:");
        foreach (var networkInterface in networkInterfaces) availableNetworkAdapters.Add(networkInterface.Name);

        return availableNetworkAdapters;
    }
}
using System.Net.NetworkInformation;

namespace IdleWatch;

public static class NetworkUsage
{
    public static float GetNetworkDownloadSpeedKbps(string adapterName)
    {
        if (string.IsNullOrWhiteSpace(adapterName))
            throw new ArgumentException("Adapter name must be non-empty.", nameof(adapterName));

        var selectedInterface = NetworkInterface
            .GetAllNetworkInterfaces()
            .FirstOrDefault(ni =>
                ni.Name.Equals(adapterName, StringComparison.OrdinalIgnoreCase));

        if (selectedInterface == null)
            throw new ArgumentException($"Adapter not found: {adapterName}", nameof(adapterName));

        var stats1 = selectedInterface.GetIPv4Statistics();
        var bytes1 = stats1.BytesReceived;

        Thread.Sleep(1_000);

        var stats2 = selectedInterface.GetIPv4Statistics();
        var bytes2 = stats2.BytesReceived;

        var delta = bytes2 - bytes1;
        return delta * 8f / 1_000f;
    }

    /// <summary>
    ///     Lists only “real” network adapters that are currently Up (Ethernet or Wireless).
    ///     Filters out loopbacks, tunnels, unknowns, virtual/hidden, etc.
    /// </summary>
    public static List<string> ListAvailableNetworkAdapters()
    {
        var adapters = NetworkInterface
            .GetAllNetworkInterfaces()
            .Where(ni =>
                    ni.OperationalStatus == OperationalStatus.Up &&
                    (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                     ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211) &&
                    !ni.Description.ToLowerInvariant().Contains("virtual") &&
                    !ni.Name.ToLowerInvariant().Contains("pseudo") // e.g. Hyper‑V, VMware
            )
            .Select(ni => ni.Name)
            .ToList();

        Console.WriteLine("Available Network Adapters (Up, Ethernet/Wireless):");
        foreach (var name in adapters)
            Console.WriteLine("  • " + name);

        return adapters;
    }
}
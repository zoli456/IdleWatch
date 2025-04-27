using Vanara.PInvoke;
using static Vanara.PInvoke.Pdh;

namespace IdleWatch;

public class CpuUsageMonitor : IDisposable
{
    private static SafePDH_HQUERY _queryHandle;
    private static SafePDH_HCOUNTER _counterHandle;
    private bool _disposed;

    public CpuUsageMonitor()
    {
        // Open a query for performance data
        PdhHelpers.ThrowIfFailed((uint)PdhOpenQuery(null, IntPtr.Zero, out _queryHandle));

        // Add a counter to measure total processor time
        PdhHelpers.ThrowIfFailed(
            (uint)PdhAddEnglishCounter(_queryHandle, "\\Processor(_Total)\\% Processor Time",
                IntPtr.Zero, out _counterHandle));

        // Initialize the counter by making an initial call
        PdhHelpers.ThrowIfFailed((uint)PdhCollectQueryData(_queryHandle));
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///     Gets the current CPU usage percentage
    /// </summary>
    /// <returns>CPU usage percentage (0-100)</returns>
    public static double GetCpuUsage()
    {
        // Collect the current data
        PdhHelpers.ThrowIfFailed((uint)PdhCollectQueryData(_queryHandle));

        // Get the formatted counter value
        PdhHelpers.ThrowIfFailed((uint)PdhGetFormattedCounterValue(
            _counterHandle,
            PDH_FMT.PDH_FMT_DOUBLE,
            out _,
            out var counterValue));

        return counterValue.doubleValue;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (!_queryHandle.IsInvalid)
        {
            if (!_counterHandle.IsInvalid)
            {
                PdhRemoveCounter(_counterHandle);
                _counterHandle.Dispose();
            }

            PdhCloseQuery(_queryHandle);
            _queryHandle.Dispose();
        }

        _disposed = true;
    }

    ~CpuUsageMonitor()
    {
        Dispose(false);
    }
}

// Helper class for PDH error handling
public static class PdhHelpers
{
    public static void ThrowIfFailed(uint status)
    {
        if (status == 0) return;

        // Get error message using FormatMessage instead of PdhFormatError
        string errorMessage;
        try
        {
            errorMessage = $"PDH error (0x{status:X8}): {Win32Error.GetLastError().ToString()}";
        }
        catch
        {
            errorMessage = $"PDH error occurred (0x{status:X8})";
        }

        throw new InvalidOperationException(errorMessage);
    }
}
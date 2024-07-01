using OpenTK.Windowing.Common;

namespace Pineapple.Core;

public class Monitor
{
    /// <summary>
    /// Gets or sets the current MonitorHandle
    /// </summary>
    public static MonitorHandle CurrentMonitor
    {
        get
        {
            return Application.ApplicationInternal.CurrentMonitor;
        }

        set
        {
            Application.ApplicationInternal.CurrentMonitor = value;
        }
    }

    /// <summary>
    /// Gets the dpi of the current monitor
    /// </summary>
    /// <param name="horizontalDpi">Horizontal dpi</param>
    /// <param name="verticalDpi">Vertical dpi</param>
    /// <returns>true, if current monitor's dpi was gotten correctly, false otherwise</returns>
    /// <remarks>
    /// This methods approximates the dpi of the monitor by multiplying the monitor scale
    /// received from OpenTK.Windowing.Desktop.NativeWindow.TryGetCurrentMonitorScale(System.Single@,System.Single@)
    /// by each platforms respective default dpi (72 for macOS and 96 for other systems).
    /// </remarks>
    public static bool TryGetCurrentMonitorDpi(out float horizontalDpi, out float verticalDpi)
    {
        return Application.ApplicationInternal.TryGetCurrentMonitorDpi(out horizontalDpi, out verticalDpi);
    }

    /// <summary>
    /// Gets the raw dpi of current monitor
    /// </summary>
    /// <param name="horizontalRawDpi">Raw horizontal dpi</param>
    /// <param name="verticalRawDpi">Raw vertical dpi</param>
    /// <returns>true, if current monitor's raw dpi was gotten correctly, false otherwise</returns>
    /// <remarks>
    /// This method calculates dpi by retrieving monitor dimensions and resolution. However
    /// on certain platforms (such as Windows) these values may not be scaled correctly.
    /// </remarks>
    public static bool TryGetCurrentMonitorDpiRaw(out float horizontalRawDpi, out float verticalRawDpi)
    {
        return Application.ApplicationInternal.TryGetCurrentMonitorDpiRaw(out horizontalRawDpi, out verticalRawDpi);
    }

    /// <summary>
    /// Gets the current monitor scale
    /// </summary>
    /// <param name="horizontalScale">Horizontal scale</param>
    /// <param name="verticalScale">Vertical scale</param>
    /// <returns>true, if current monitor scale was gotten correctly, false otherwise</returns>
    public static bool TryGetCurrentMonitorScale(out float horizontalScale, out float verticalScale)
    {
        return Application.ApplicationInternal.TryGetCurrentMonitorScale(out horizontalScale, out verticalScale);
    }
}

using System.Runtime.InteropServices;

namespace SnapNET.Model.PInvoke
{
    /// <summary>
    /// Simple class for monitor info used by GetMonitorInfo
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
    internal class MonitorInfoEx
    {
        internal int cbSize = Marshal.SizeOf(typeof(MonitorInfoEx));
        internal Rect rcMonitor = new Rect();
        internal Rect rcWork = new Rect();
        internal int dwFlags = 0;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        internal char[] szDevice = new char[32];

        internal static int MonitorPrimaryFlag = 0x00000001;
    }
}

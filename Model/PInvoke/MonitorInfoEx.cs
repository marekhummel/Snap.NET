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
        internal Rect rcMonitor = new();
        internal Rect rcWork = new();
        internal int dwFlags = 0;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        internal char[] szDevice = new char[32];


        /// <summary>
        /// If this bit is set, its the primary monitor
        /// </summary>
        internal static int MonitorPrimaryFlag = 0x00000001;
    }
}

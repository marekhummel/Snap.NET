using SnapNET.Model.PInvoke;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SnapNET.Model.Monitor
{
    /// <summary>
    /// Class for a connected monitor, contains info about name, primary flag and bounds
    /// </summary>
    public class Monitor
    {
        // ***** Properties *****

        /// <summary>
        /// Name of the monitor
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The bounds of the monitor (comparable to its resolution)
        /// </summary>
        public System.Windows.Rect Bounds { get; private set; }

        /// <summary>
        /// The working area of the monitor. Doesn't count the taskbar and is relative to other monitors!
        /// </summary>
        public System.Windows.Rect WorkingArea { get; private set; }

        /// <summary>
        /// Flag if this is the primary monitor
        /// </summary>
        public bool IsPrimary { get; private set; }


        // ***** Constructor *****

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="monitor">Pointer to monitor for handle</param>
        private Monitor(IntPtr monitor)
        {
            var info = new MonitorInfoEx();
            User32.GetMonitorInfo(monitor, info);
            Bounds = new System.Windows.Rect(
                        info.rcMonitor.left, info.rcMonitor.top,
                        info.rcMonitor.right - info.rcMonitor.left,
                        info.rcMonitor.bottom - info.rcMonitor.top);
            WorkingArea = new System.Windows.Rect(
                       info.rcWork.left, info.rcWork.top,
                       info.rcWork.right - info.rcWork.left,
                       info.rcWork.bottom - info.rcWork.top);
            IsPrimary = ((info.dwFlags & MonitorInfoEx.MonitorPrimaryFlag) != 0);
            Name = new string(info.szDevice).TrimEnd((char)0);
        }


        // ***** Static methods *****

        /// <summary>
        /// Returns a list of connected monitors 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Monitor> GetAllMonitors()
        {
            var monitors = new List<Monitor>();
            var proc = new User32.MonitorEnumProc((monPtr, hdc, lprc, lparam) => {
                monitors.Add(new Monitor(monPtr));
                return true;
            });
            User32.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, proc, IntPtr.Zero);
            return monitors;
        }
    }
}

using SnapNET.Model.PInvoke;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SnapNET.Model
{
    internal static class Utilities
    {



        /// <summary>
        /// Returns the screen resolutions for all connected monitors
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<(double width, double height)> GetScreenResoultions()
        {
            var mons = Monitor.Monitor.GetAllMonitors();
            foreach (var m in mons)
                yield return (m.Bounds.Width, m.Bounds.Height);
        }
        public static string GetActiveWindowTitle()
            => Window.Window.GetCurrentForegroundWindow();



    }
}

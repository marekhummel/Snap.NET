using SnapNET.Model.PInvoke;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapNET.Model.Window
{
    /// <summary>
    /// Methods to interact with window handles
    /// </summary>
    internal static class WindowHelper
    {
        // ***** Public methods *****

        /// <summary>
        /// Gets the handle and title of the current foreground window
        /// </summary>
        /// <returns></returns>
        internal static (IntPtr Handle, string Title) GetCurrentForegroundWindow()
        {
            var hWnd = User32.GetForegroundWindow();
            return (hWnd, GetWindowTitle(hWnd));
        }

        /// <summary>
        /// Reads title of window given by the handle
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        internal static string GetWindowTitle(IntPtr hWnd)
        {
            var sbTitle = new StringBuilder(255);
            User32.GetWindowText(hWnd, sbTitle, sbTitle.Capacity + 1);
            return sbTitle.ToString();
        }


        internal static void SetWindowSize(IntPtr hWnd, double left, double top, double width, double height)
        {
            User32.MoveWindow(hWnd, (int)left, (int)top, (int)width, (int)height, true);
        }


        internal static bool IsHandleFromThisApplication(IntPtr hWnd)
        {
            User32.GetWindowThreadProcessId(hWnd, out uint pid);
            return pid == System.Diagnostics.Process.GetCurrentProcess().Id;
        }
    }
}

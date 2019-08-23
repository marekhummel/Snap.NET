using SnapNET.Model.PInvoke;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
            //User32.MoveWindow(hWnd, (int)left, (int)top, (int)width, (int)height, true);

            
            var margin = GetSystemWindowMargins(hWnd);
            uint flags = Constants.SWP_ASYNCWINDOWPOS | Constants.SWP_NOOWNERZORDER | Constants.SWP_NOZORDER;
            User32.SetWindowPos(hWnd, IntPtr.Zero, (int)left - margin.left, (int)top - margin.top, (int)width + margin.left + margin.right, (int)height + margin.top + margin.bottom, flags);
        }


        private static Rect GetSystemWindowMargins(IntPtr hWnd)
        {
            User32.GetWindowRect(hWnd, out var noMargin);
            User32.DwmGetWindowAttribute(hWnd, Constants.DWMWA_EXTENDED_FRAME_BOUNDS, out var withMargin, Marshal.SizeOf<Rect>());
            return new Rect() {
                left = noMargin.left - withMargin.left,
                top = noMargin.top - withMargin.top,
                right = noMargin.right - withMargin.right,
                bottom = noMargin.bottom - withMargin.bottom,
            };
        }


        internal static bool IsHandleFromThisApplication(IntPtr hWnd)
        {
            User32.GetWindowThreadProcessId(hWnd, out uint pid);
            return pid == System.Diagnostics.Process.GetCurrentProcess().Id;
        }
    }
}

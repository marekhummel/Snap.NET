﻿using System;
using System.Runtime.InteropServices;
using System.Text;

using SnapNET.Model.PInvoke;

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
            var hWnd = NativeMethods.GetForegroundWindow();
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
            _ = NativeMethods.GetWindowText(hWnd, sbTitle, sbTitle.Capacity + 1);
            return sbTitle.ToString();
        }

        /// <summary>
        /// Sets window size of given handle (in absoulte coordinates)
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="focusAfterResizing"></param>
        internal static void SetWindowSize(IntPtr hWnd, double left, double top, double width, double height, bool focusAfterResizing)
        {
            // Window is maximized, minimize it to calc margin size
            if (IsWindowMaximized(hWnd)) {
                _ = NativeMethods.ShowWindow(hWnd, Constants.SW_SHOWNORMAL);
            }

            // Get window margin
            var margin = GetSystemWindowMargins(hWnd);

            // Calc offset location and size to achieve desired behaviour
            int oleft = (int)left - margin.left;
            int otop = (int)top - margin.top;
            int owidth = (int)width + margin.left + margin.right;
            int oheight = (int)height + margin.top + margin.bottom;

            // Set window pos
            uint flags = Constants.SWP_ASYNCWINDOWPOS | Constants.SWP_NOOWNERZORDER | Constants.SWP_NOZORDER;
            Console.WriteLine(NativeMethods.SetWindowPos(hWnd, IntPtr.Zero, oleft, otop, owidth, oheight, flags));

            // Focus
            if (focusAfterResizing) {
                _ = NativeMethods.SetForegroundWindow(hWnd);
            }

            // ToDo: Set to maximized if rect covers full screen ?
        }

        /// <summary>
        /// Returns true, if the given handle belongs to this process
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        internal static bool IsHandleFromThisApplication(IntPtr hWnd)
        {
            _ = NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
            return pid == Environment.ProcessId;
        }




        // ***** Private methods *****

        /// <summary>
        /// Returns invis margins around window
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        private static Rect GetSystemWindowMargins(IntPtr hWnd)
        {
            _ = NativeMethods.GetWindowRect(hWnd, out var extendendRect);
            _ = NativeMethods.DwmGetWindowAttribute(hWnd, Constants.DWMWA_EXTENDED_FRAME_BOUNDS, out var visRect, Marshal.SizeOf<Rect>());
            return new Rect() {
                left = visRect.left - extendendRect.left,
                top = visRect.top - extendendRect.top,
                right = extendendRect.right - visRect.right,
                bottom = extendendRect.bottom - visRect.bottom,
            };
        }

        /// <summary>
        /// Returns true if window handle is maximized
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        private static bool IsWindowMaximized(IntPtr hWnd)
        {
            uint style = NativeMethods.GetWindowLong(hWnd, Constants.GWL_STYLE);
            return (style & Constants.WS_MAXIMIZE) == Constants.WS_MAXIMIZE;
        }
    }
}

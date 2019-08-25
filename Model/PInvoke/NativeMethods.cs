using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

namespace SnapNET.Model.PInvoke
{
    internal static class NativeMethods
    {
        // *****************************
        // *****      EXTERNS      *****
        // *****************************

        /// <summary>
        /// Return windows text
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpWindowText"></param>
        /// <param name="nMaxCount"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowText", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);

        /// <summary>
        /// Gets current foreground window (different to active, because its global and not thread specific
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// Sets the foreground window
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// Returns info about the referenced monitor
        /// </summary>
        /// <param name="hmonitor"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        internal static extern bool GetMonitorInfo(IntPtr hmonitor, [In, Out]MonitorInfoEx info);

        /// <summary>
        /// Enumerator on all connected monitors
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="rcClip"></param>
        /// <param name="lpfnEnum"></param>
        /// <param name="dwData"></param>
        /// <returns></returns>
        [DllImport("user32.dll", ExactSpelling = true)]
        [ResourceExposure(ResourceScope.None)]
        internal static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr rcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);

        /// <summary>
        /// Set up an global low level keyboard hook
        /// </summary>
        /// <param name="hookType"></param>
        /// <param name="lpfn"></param>
        /// <param name="hMod"></param>
        /// <param name="dwThreadId"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        /// <summary>
        /// Unhook keyboard hooks
        /// </summary>
        /// <param name="hhk"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnhookWindowsHookEx(IntPtr hhk);

        /// <summary>
        /// Pass info to next callback
        /// </summary>
        /// <param name="hhk"></param>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Returns handle for given module (of process)
        /// </summary>
        /// <param name="lpModuleName"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr GetModuleHandle(string lpModuleName);

        /// <summary>
        /// Registers global event hook
        /// </summary>
        /// <param name="eventMin"></param>
        /// <param name="eventMax"></param>
        /// <param name="hmodWinEventProc"></param>
        /// <param name="lpfnWinEventProc"></param>
        /// <param name="idProcess"></param>
        /// <param name="idThread"></param>
        /// <param name="dwFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        /// <summary>
        /// Returns creating thread id and process id of handle
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpdwProcessId"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        /// <summary>
        /// Sets location and position of given window handle in absolute (not client-based) coordinates 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hWndInsertAfter"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        /// <summary>
        /// Gets bounds of window handle
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lpRect"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool GetWindowRect(IntPtr hwnd, out Rect lpRect);

        /// <summary>
        /// Gets dwm attribute of window handle
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="dwAttribute"></param>
        /// <param name="lpRect"></param>
        /// <param name="cbAttribute"></param>
        /// <returns></returns>
        [DllImport("dwmapi.dll")]
        internal static extern int DwmGetWindowAttribute(IntPtr hWnd, int dwAttribute, out Rect lpRect, int cbAttribute);

        /// <summary>
        /// Gets requested attribute of window
        /// </summary>
        /// <param name="window"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint GetWindowLong(IntPtr window, int index);

        /// <summary>
        /// Changes window state (minimized, maximized etc.)
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nCmdShow"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


        // *****************************
        // *****     DELEGATES     *****
        // *****************************

        /// <summary>
        /// filter function
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        internal delegate bool EnumDelegate(IntPtr hWnd, int lParam);

        /// <summary>
        /// Delegate as callback for EnumDisplayMonitors
        /// </summary>
        /// <param name="monitor"></param>
        /// <param name="hdc"></param>
        /// <param name="lprcMonitor"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        internal delegate bool MonitorEnumProc(IntPtr monitor, IntPtr hdc, IntPtr lprcMonitor, IntPtr lParam);

        /// <summary>
        /// Delegate as callback for keyboard hooks
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        internal delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Delegate as callback for global events
        /// </summary>
        /// <param name="hWinEventHook"></param>
        /// <param name="eventType"></param>
        /// <param name="hwnd"></param>
        /// <param name="idObject"></param>
        /// <param name="idChild"></param>
        /// <param name="dwEventThread"></param>
        /// <param name="dwmsEventTime"></param>
        internal delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);
    }
}

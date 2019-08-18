using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

namespace SnapNET.Model.PInvoke
{
    internal static class User32
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
        [DllImport("user32.dll", EntryPoint = "GetWindowText",
        ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
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
        internal static extern bool GetMonitorInfo(HandleRef hmonitor, [In, Out]MonitorInfoEx info);

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
        internal static extern bool EnumDisplayMonitors(HandleRef hdc, IntPtr rcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);

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
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
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

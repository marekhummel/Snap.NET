using System;

namespace SnapNET.Model.PInvoke
{
    /// <summary>
    /// Some constants needed for the pinvoke calls
    /// </summary>
    internal static class Constants
    {
        // Keyboard hook set up and wparam check in callback
        internal static int WH_KEYBOARD_LL = 13;
        internal static IntPtr WM_KEYDOWN = (IntPtr)0x0100;
        internal static IntPtr WM_KEYUP = (IntPtr)0x0101;

        // Eventhook setup
        internal static uint WINEVENT_OUTOFCONTEXT = 0;
        internal static uint EVENT_SYSTEM_FOREGROUND = 3;

        // Set window pos flags
        internal static uint SWP_ASYNCWINDOWPOS = 0x4000;
        internal static uint SWP_NOZORDER = 0x0004;
        internal static uint SWP_NOOWNERZORDER = 0x0200;

        // To get visible window bounds (instead of the extra margin)
        internal static int DWMWA_EXTENDED_FRAME_BOUNDS = 9;

        // Check for maximized flag in styles of handle
        internal static int GWL_STYLE = -16;
        internal static long WS_MAXIMIZE = 0x01000000L;

        // Values to set window state
        internal static int SW_SHOWNORMAL = 1;
        internal static int SW_SHOWMAXIMIZED = 3;
    }
}

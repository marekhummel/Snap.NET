using System;

namespace SnapNET.Model.PInvoke
{
    /// <summary>
    /// Some constants needed for the pinvoke calls
    /// </summary>
    internal static class Constants
    {
        internal static int GA_ROOTOWNER = 3;

        internal static int WH_KEYBOARD_LL = 13;
        internal static IntPtr WM_KEYDOWN = (IntPtr)0x0100;
        internal static IntPtr WM_KEYUP = (IntPtr)0x0101;

        internal static uint WINEVENT_OUTOFCONTEXT = 0;
        internal static uint EVENT_SYSTEM_FOREGROUND = 3;

        internal static uint SWP_ASYNCWINDOWPOS = 0x4000;
        internal static uint SWP_NOZORDER = 0x0004;
        internal static uint SWP_NOOWNERZORDER = 0x0200;

        internal static int DWMWA_EXTENDED_FRAME_BOUNDS = 9;
    }
}

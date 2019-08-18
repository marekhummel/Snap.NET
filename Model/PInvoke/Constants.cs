using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapNET.Model.PInvoke
{
    internal static class Constants
    {  
        internal static int GA_ROOTOWNER = 3;

        internal static int WH_KEYBOARD_LL = 13;
        internal static IntPtr WM_KEYDOWN = (IntPtr)0x0100;
        internal static IntPtr WM_KEYUP = (IntPtr)0x0101;

        internal static uint WINEVENT_OUTOFCONTEXT = 0;
        internal static uint EVENT_SYSTEM_FOREGROUND = 3;
    }
}

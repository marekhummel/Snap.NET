using SnapNET.Model.PInvoke;
using System;

namespace SnapNET.Model.Window
{
    internal static class ForegroundWindowListener
    {

        private static IntPtr _hook;
        private static User32.WinEventDelegate _eventDelegate;


        internal static event EventHandler<ForegroundWindowChangedEventArgs> OnForegroundWindowChanged;

        internal static void StartListener()
        {
            _eventDelegate = new User32.WinEventDelegate((hWinEventHook, eventType, hwnd, idObject, idChild, dwEventThread, dwmsEventTime) => {
                OnForegroundWindowChanged?.Invoke(null, new ForegroundWindowChangedEventArgs(Window.GetCurrentForegroundWindow()));
            });
            _hook = User32.SetWinEventHook(Constants.EVENT_SYSTEM_FOREGROUND, Constants.EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, _eventDelegate, 0, 0, Constants.WINEVENT_OUTOFCONTEXT);
        }


        internal static void StopListener()
            => User32.UnhookWindowsHookEx(_hook);
    }
}

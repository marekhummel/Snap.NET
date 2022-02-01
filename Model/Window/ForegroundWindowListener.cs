using System;

using SnapNET.Model.PInvoke;

namespace SnapNET.Model.Window
{
    /// <summary>
    /// Functionality to react to changed in the foreground window
    /// </summary>
    internal static class ForegroundWindowListener
    {
        // ***** Private members *****

        private static bool _isRunning = false;
        private static IntPtr _hook;
        private static NativeMethods.WinEventDelegate? _eventDelegate;


        // ***** Public members *****

        /// <summary>
        /// Triggers when the foreground window changed, according to the GetForegroundWindow function
        /// </summary>
        internal static event EventHandler<ForegroundWindowChangedEventArgs>? OnForegroundWindowChanged;


        // ***** Public methods *****

        /// <summary>
        /// Starts listening to foreground window changes
        /// </summary>
        internal static void StartListener()
        {
            if (_isRunning) {
                return;
            }

            _eventDelegate = new NativeMethods.WinEventDelegate((hWinEventHook, eventType, hwnd, idObject, idChild, dwEventThread, dwmsEventTime)
                => OnForegroundWindowChanged?.Invoke(null, new ForegroundWindowChangedEventArgs(hwnd)));

            _hook = NativeMethods.SetWinEventHook(Constants.EVENT_SYSTEM_FOREGROUND, Constants.EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, _eventDelegate, 0, 0, Constants.WINEVENT_OUTOFCONTEXT);
            OnForegroundWindowChanged?.Invoke(null, new ForegroundWindowChangedEventArgs(WindowHelper.GetCurrentForegroundWindow().Handle));
            _isRunning = true;
        }

        /// <summary>
        /// Stops the listener
        /// </summary>
        internal static void StopListener()
        {
            if (!_isRunning) {
                return;
            }

            _ = NativeMethods.UnhookWindowsHookEx(_hook);
            _isRunning = false;
        }
    }
}

using SnapNET.Model.PInvoke;
using System;
using System.Text;

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
        private static User32.WinEventDelegate _eventDelegate;


        // ***** Public members *****

        /// <summary>
        /// Triggers when the foreground window changed, according to the GetForegroundWindow function
        /// </summary>
        internal static event EventHandler<ForegroundWindowChangedEventArgs> OnForegroundWindowChanged;


        // ***** Public methods *****

        /// <summary>
        /// Starts listening to foreground window changes
        /// </summary>
        internal static void StartListener()
        {
            if (_isRunning)
                return;

            _eventDelegate = new User32.WinEventDelegate((hWinEventHook, eventType, hwnd, idObject, idChild, dwEventThread, dwmsEventTime) => {
                OnForegroundWindowChanged?.Invoke(null, new ForegroundWindowChangedEventArgs(GetCurrentForegroundWindow()));
            });

            _hook = User32.SetWinEventHook(Constants.EVENT_SYSTEM_FOREGROUND, Constants.EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, _eventDelegate, 0, 0, Constants.WINEVENT_OUTOFCONTEXT);
            OnForegroundWindowChanged?.Invoke(null, new ForegroundWindowChangedEventArgs(GetCurrentForegroundWindow()));
            _isRunning = true;
        }

        /// <summary>
        /// Stops the listener
        /// </summary>
        internal static void StopListener()
        {
            if (!_isRunning)
                return;

            User32.UnhookWindowsHookEx(_hook);
            _isRunning = false;
        }


        // ***** Private methods *****

        /// <summary>
        /// Gets the title of the current foreground window
        /// </summary>
        /// <returns></returns>
        private static string GetCurrentForegroundWindow()
        {
            var hWnd = User32.GetForegroundWindow();
            return GetWindowTitle(hWnd);
        }

        /// <summary>
        /// Reads title of window given by the handle
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        private static string GetWindowTitle(IntPtr hWnd)
        {
            var sbTitle = new StringBuilder(255);
            User32.GetWindowText(hWnd, sbTitle, sbTitle.Capacity + 1);
            return sbTitle.ToString();
        }
    }
}

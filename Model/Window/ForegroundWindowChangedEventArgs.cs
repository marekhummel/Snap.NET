using System;

namespace SnapNET.Model.Window
{
    /// <summary>
    /// EventArgs which contains the new foreground window title
    /// </summary>
    internal class ForegroundWindowChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Handle of the foreground window
        /// </summary>
        public IntPtr ForegroundWindowHandle { get; private set; }

        /// <summary>
        /// Title of the foreground window
        /// </summary>
        public string ForegroundWindowTitle { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title"></param>
        public ForegroundWindowChangedEventArgs(IntPtr hwnd)
        {
            ForegroundWindowHandle = hwnd;
            ForegroundWindowTitle = WindowHelper.GetWindowTitle(hwnd);
        }
    }
}

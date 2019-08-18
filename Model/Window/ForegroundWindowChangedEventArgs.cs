using System;

namespace SnapNET.Model.Window
{
    /// <summary>
    /// EventArgs which contains the new foreground window title
    /// </summary>
    internal class ForegroundWindowChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Title of the foreground window
        /// </summary>
        public string ForegroundWindowTitle { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title"></param>
        public ForegroundWindowChangedEventArgs(string title)
        {
            ForegroundWindowTitle = title;
        }
    }
}

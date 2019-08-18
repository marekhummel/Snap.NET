using System;
using System.Windows.Input;

namespace SnapNET.Model.Window
{
    internal class ForegroundWindowChangedEventArgs : EventArgs
    {
        public string ForegroundWindowTitle { get; private set; }

        public ForegroundWindowChangedEventArgs(string title)
        {
            ForegroundWindowTitle = title;
        }
    }
}

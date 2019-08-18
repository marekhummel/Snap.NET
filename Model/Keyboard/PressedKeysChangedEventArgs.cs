using System;
using System.Windows.Input;

namespace SnapNET.Model.Keyboard
{
    internal class PressedKeysChangedEventArgs : EventArgs
    {
        public Key ChangedKey { get; private set; }
        public bool OnKeyDown { get; private set; }

        public PressedKeysChangedEventArgs(Key k, bool down)
        {
            ChangedKey = k;
            OnKeyDown = down;
        }
    }
}

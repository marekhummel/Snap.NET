using System;
using System.Windows.Input;

namespace SnapNET.Model.Keyboard
{
    /// <summary>
    /// EventArgs which contain the pressed key
    /// </summary>
    internal class PressedKeysChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The key which changed state
        /// </summary>
        public Key ChangedKey { get; private set; }

        /// <summary>
        /// True, if on key down, false on key up
        /// </summary>
        public bool OnKeyDown { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="k">The key</param>
        /// <param name="down">Keydown flag</param>
        public PressedKeysChangedEventArgs(Key k, bool down)
        {
            ChangedKey = k;
            OnKeyDown = down;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;

using SnapNET.Model.PInvoke;

namespace SnapNET.Model.Keyboard
{
    /// <summary>
    /// Gives the functionality to listen to global keyboard inputs
    /// </summary>
    internal static class KeyboardListener
    {
        // ***** Private members *****

        private static bool _isRunning;
        private static readonly List<IntPtr> _hooks = new();
        private static readonly List<NativeMethods.LowLevelKeyboardProc> _hookCallbacks = new();


        // ***** Public members *****

        /// <summary>
        /// Set of currently pressed keys
        /// </summary>
        internal static HashSet<Key> PressedKeys { get; private set; } = new HashSet<Key>();

        /// <summary>
        /// Callback delegate to handle a keypress (or release)
        /// </summary>
        /// <param name="key"></param>
        internal delegate void KeyboardCallback(int key);

        /// <summary>
        /// Event which is fired upon keypress and keyrelease
        /// </summary>
        internal static event EventHandler<PressedKeysChangedEventArgs>? OnPressedKeysChanged;


        // ***** Public methods *****

        /// <summary>
        /// Starts the listener
        /// </summary>
        internal static void StartListener()
        {
            if (_isRunning) {
                return;
            }

            // Key down
            _hooks.Add(AddKeyboardCallback((keyInt) => {
                var key = KeyInterop.KeyFromVirtualKey(keyInt);
                _ = PressedKeys.Add(key);
                OnPressedKeysChanged?.Invoke(null, new PressedKeysChangedEventArgs(key, true));
            }, Constants.WM_KEYDOWN));

            // Key up
            _hooks.Add(AddKeyboardCallback((keyInt) => {
                var key = KeyInterop.KeyFromVirtualKey(keyInt);
                _ = PressedKeys.Remove(key);
                OnPressedKeysChanged?.Invoke(null, new PressedKeysChangedEventArgs(key, false));
            }, Constants.WM_KEYUP));

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

            // Unhook everything
            foreach (var hook in _hooks) {
                _ = NativeMethods.UnhookWindowsHookEx(hook);
            }

            _isRunning = false;
        }


        // ***** Private methods ***** 

        /// <summary>
        /// Add given callback as hook for the keyboard
        /// </summary>
        /// <param name="kc">Callback method</param>
        /// <param name="expectedWParam">Specify the expected wParam (keydown or keyup)</param>
        /// <returns>Returns pointer to hook</returns>
        private static IntPtr AddKeyboardCallback(KeyboardCallback kc, IntPtr expectedWParam)
        {
            var newProc = CreateHookCallBack(kc, expectedWParam);
            _hookCallbacks.Add(newProc);

            using var curProcess = Process.GetCurrentProcess();
            using var curModule = curProcess.MainModule;
            return NativeMethods.SetWindowsHookEx(Constants.WH_KEYBOARD_LL, newProc, NativeMethods.GetModuleHandle(curModule?.ModuleName ?? ""), 0);
        }

        /// <summary>
        /// Creates actual keyboard procedure from callback
        /// </summary>
        /// <param name="kc">The callback</param>
        /// <param name="expectedWParam">Specify the expected wParam (keydown or keyup)</param>
        /// <returns>LowLevelKeyboardProc for the keyboard hook</returns>
        private static NativeMethods.LowLevelKeyboardProc CreateHookCallBack(KeyboardCallback kc, IntPtr expectedWParam)
        {
            return new NativeMethods.LowLevelKeyboardProc((code, wparam, lparam) => {
                if (code >= 0 && wparam == expectedWParam) {
                    int keyCode = Marshal.ReadInt32(lparam);
                    kc.Invoke(keyCode);
                }

                return NativeMethods.CallNextHookEx(IntPtr.Zero, code, wparam, lparam);
            });
        }
    }
}

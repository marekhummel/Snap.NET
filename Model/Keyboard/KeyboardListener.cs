using SnapNET.Model.PInvoke;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SnapNET.Model.Keyboard
{
    internal static class KeyboardListener
    {
        private static readonly List<IntPtr> _hooks = new List<IntPtr>();
        private static readonly List<User32.LowLevelKeyboardProc> _hookCallbacks = new List<User32.LowLevelKeyboardProc>();


        internal static HashSet<Key> PressedKeys { get; private set; } = new HashSet<Key>();


        internal delegate void KeyboardCallback(int key);
        internal static event EventHandler<PressedKeysChangedEventArgs> OnPressedKeysChanged;


        internal static void StartListener()
        {
            // Key down
            _hooks.Add(AddKeyboardCallback((keyInt) => {
                var key = KeyInterop.KeyFromVirtualKey(keyInt);
                PressedKeys.Add(key);
                OnPressedKeysChanged?.Invoke(null, new PressedKeysChangedEventArgs(key, true));
            }, Constants.WM_KEYDOWN));

            // Key up
            _hooks.Add(AddKeyboardCallback((keyInt) => {
                var key = KeyInterop.KeyFromVirtualKey(keyInt);
                PressedKeys.Remove(key);
                OnPressedKeysChanged?.Invoke(null, new PressedKeysChangedEventArgs(key, false));
            }, Constants.WM_KEYUP));
        }


        internal static void StopListener()
        {
            foreach (var hook in _hooks)
                User32.UnhookWindowsHookEx(hook);
        }





        private static IntPtr AddKeyboardCallback(KeyboardCallback kc, IntPtr expectedWParam)
        {
            var newProc = CreateHookCallBack(kc, expectedWParam);
            _hookCallbacks.Add(newProc);

            using (var curProcess = Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
                return User32.SetWindowsHookEx(Constants.WH_KEYBOARD_LL, newProc, User32.GetModuleHandle(curModule.ModuleName), 0);
        }


        private static User32.LowLevelKeyboardProc CreateHookCallBack(KeyboardCallback kc, IntPtr expectedWParam)
        {
            return new User32.LowLevelKeyboardProc((code, wparam, lparam) => {
                if (code >= 0 && wparam == expectedWParam) {
                    int keyCode = Marshal.ReadInt32(lparam);
                    kc.Invoke(keyCode);
                }

                return User32.CallNextHookEx(IntPtr.Zero, code, wparam, lparam);
            });
        }
    }
}

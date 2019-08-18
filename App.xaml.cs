using SnapNET.Model.Keyboard;
using SnapNET.Model.Monitor;
using SnapNET.Model.Window;
using SnapNET.View;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using SnapNET.Model.PInvoke;

namespace SnapNET
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private List<ResizingWindow> _resWindows;

        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Get and store previous focussed window handle
            var hwnd = User32.GetForegroundWindow();

            // ** Load settings

            // ** Preset applications




            // ** Create windows
            _resWindows = new List<ResizingWindow>();
            foreach (var mon in Monitor.GetAllMonitors()) {
                var rw = new ResizingWindow();
                rw.Left = mon.WorkingArea.Left + 0.5 * mon.WorkingArea.Width - 0.5 * rw.Width;
                rw.Top = mon.WorkingArea.Top + 0.5 * mon.WorkingArea.Height - 0.5 * rw.Height;
                _resWindows.Add(rw);
                rw.Show();
            }


            // ** Fire up listeners
            KeyboardListener.OnPressedKeysChanged += ((sender, args) => {
                foreach (var rw in _resWindows) {
                    rw.pressedKeysTextBox.Text = String.Join(" + ", KeyboardListener.PressedKeys);

                    if (KeyboardListener.PressedKeys.Count == 1 && KeyboardListener.PressedKeys.Contains(Key.Space))
                        rw.Visibility = rw.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
                }

            });
            ForegroundWindowListener.OnForegroundWindowChanged += ((sender, args) => {
                foreach (var rw in _resWindows)
                    rw.windowTitleTextBox.Text = args.ForegroundWindowTitle;
            });

            // Reset focussed window
            User32.SetForegroundWindow(hwnd);

            KeyboardListener.StartListener();
            ForegroundWindowListener.StartListener();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            KeyboardListener.StopListener();
            ForegroundWindowListener.StopListener();
            base.OnExit(e);
        }
    }
}

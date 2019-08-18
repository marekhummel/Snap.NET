using SnapNET.Model.Keyboard;
using SnapNET.Model.Window;
using SnapNET.View;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace SnapNET
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ResizingWindow _resWindow;


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Load settings

            // Preset applications




            // Create windows
            _resWindow = new ResizingWindow();
            _resWindow.Show();

            // Fire up listeners
            KeyboardListener.OnPressedKeysChanged += ((sender, args) => {
                _resWindow.pressedKeysTextBox.Text = String.Join(" + ", KeyboardListener.PressedKeys);

                if (KeyboardListener.PressedKeys.Count == 1 && KeyboardListener.PressedKeys.Contains(Key.Space))
                    _resWindow.Visibility = _resWindow.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
            });
            KeyboardListener.StartListener();

            ForegroundWindowListener.OnForegroundWindowChanged += ((sender, args) => {
                _resWindow.windowTitleTextBox.Text = args.ForegroundWindowTitle;
            });
            ForegroundWindowListener.StartListener();
        }


        protected override void OnExit(ExitEventArgs e)
        {
            KeyboardListener.StopListener();
            base.OnExit(e);
        }
    }
}

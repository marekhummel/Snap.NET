using SnapNET.Model;
using SnapNET.Model.Keyboard;
using SnapNET.Model.Monitor;
using SnapNET.Model.PInvoke;
using SnapNET.Model.Window;
using SnapNET.View;
using SnapNET.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace SnapNET
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private List<ResizingWindow> _resWindows;
        private List<ResizingWindowChildViewModel> _childVms;
        private ResizingWindowParentViewModel _parentVm;
        

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Get and store previous focussed window handle
            var hwnd = User32.GetForegroundWindow();

            // ** Load settings

            // ** Preset applications

            


            // ** Create windows
            _resWindows = new List<ResizingWindow>();
            _childVms = new List<ResizingWindowChildViewModel>();
            _parentVm = new ResizingWindowParentViewModel();
            foreach (var mon in Monitor.GetAllMonitors()) {
                var vm = new ResizingWindowChildViewModel(_parentVm) { Monitor = mon };

                var rw = new ResizingWindow();
                //rw.Left = mon.WorkingArea.Left + 0.5 * mon.WorkingArea.Width - 0.5 * rw.Width;
                //rw.Top = mon.WorkingArea.Top + 0.5 * mon.WorkingArea.Height - 0.5 * rw.Height;
                rw.DataContext = vm;

                var visBind = new Binding("Parent.IsVisible") { Source = vm, Converter = new BooleanToVisibilityConverter(), Mode = BindingMode.TwoWay};
                rw.SetBinding(Window.VisibilityProperty, visBind);


                // Debug
                rw.listBox.Items.Add(mon.Name);
                rw.listBox.Items.Add(mon.IsPrimary);

                _resWindows.Add(rw);
                rw.Show();
            }


            // ** Fire up listeners
            KeyboardListener.OnPressedKeysChanged += ((sender, args) => {
                // DEBUG
                foreach (var rw in _resWindows) {
                    rw.pressedKeysTextBox.Text = String.Join(" + ", KeyboardListener.PressedKeys);
                }

                // Update visibility
                if (KeyboardListener.PressedKeys.Count == 1 && KeyboardListener.PressedKeys.Contains(Key.Space)) {
                    _parentVm.IsVisible = !_parentVm.IsVisible;
                }
            });
            ForegroundWindowListener.OnForegroundWindowChanged += ((sender, args) => {
                if (!WindowHelper.IsHandleFromThisApplication(args.ForegroundWindowHandle)) {
                    _parentVm.ForegroundWindowTitle = args.ForegroundWindowTitle;
                }
            });

            // Reset focussed window
            User32.SetForegroundWindow(hwnd);

            KeyboardListener.StartListener();
            ForegroundWindowListener.StartListener();
        }

        /// <summary>
        /// Called on exit of application, takes care of pinvoke hooks
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExit(ExitEventArgs e)
        {
            KeyboardListener.StopListener();
            ForegroundWindowListener.StopListener();
            base.OnExit(e);
        }
    }
}

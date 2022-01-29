using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

using SnapNET.Model.Keyboard;
using SnapNET.Model.Monitor;
using SnapNET.Model.PInvoke;
using SnapNET.Model.Settings;
using SnapNET.Model.Window;
using SnapNET.View;
using SnapNET.ViewModel;

namespace SnapNET
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private List<ResizingWindow> _resWindows;
        private List<ResizingWindowViewModel> _childVms;
        private ResizingWindowSharedViewModel _parentVm;


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // ToDo: Request admin rights

            // Get and store previous focussed window handle
            var hwnd = NativeMethods.GetForegroundWindow();

            // ** Load settings
            SettingsManager.LoadSettings();

            // ** Preset applications
            // ToDo



            // ** Create windows
            _resWindows = new List<ResizingWindow>();
            _childVms = new List<ResizingWindowViewModel>();
            _parentVm = new ResizingWindowSharedViewModel();

            // Create view instance for each monitor
            foreach (var mon in Monitor.GetAllMonitors()) {
                // Viewmodel
                var vm = new ResizingWindowViewModel(_parentVm, mon, SettingsManager.GetGridSettings(mon.Name));
                _childVms.Add(vm);

                // Window
                var rw = new ResizingWindow();
                rw.Left = mon.WorkingArea.Left + (0.5 * mon.WorkingArea.Width) - (0.5 * rw.Width);
                rw.Top = mon.WorkingArea.Top + (0.5 * mon.WorkingArea.Height) - (0.5 * rw.Height);
                rw.DataContext = vm;

                // Bindings
                var visBind = new Binding("Shared.IsVisible") { Source = vm, Converter = new BooleanToVisibilityConverter(), Mode = BindingMode.TwoWay };
                _ = rw.SetBinding(UIElement.VisibilityProperty, visBind);

                // Show
                _resWindows.Add(rw);
                rw.Show();
            }

            // Config listeners
            KeyboardListener.OnPressedKeysChanged += (sender, args) => {
                // DEBUG
                //foreach (var rw in _resWindows) {
                //    rw.pressedKeysTextBox.Text = string.Join(" + ", KeyboardListener.PressedKeys);
                //}

                // Update visibility
                if (KeyboardListener.PressedKeys.SetEquals(new[] { Key.LeftCtrl, Key.Space })) {
                    _parentVm.IsVisible = !_parentVm.IsVisible;
                }
                else if (KeyboardListener.PressedKeys.SetEquals(new Key[] { Key.LeftCtrl, Key.LeftShift, Key.Space })) {
                    Application.Current.Shutdown();
                }
            };
            ForegroundWindowListener.OnForegroundWindowChanged += (sender, args) => {
                if (WindowHelper.IsHandleFromThisApplication(args.ForegroundWindowHandle)) 
                    return;
                _parentVm.ForegroundWindowHandle = args.ForegroundWindowHandle;
                _parentVm.ForegroundWindowTitle = args.ForegroundWindowTitle;
            };

            // Reset focussed window
            _ = NativeMethods.SetForegroundWindow(hwnd);

            // Start listeners
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

            SettingsManager.StoreSettings();

            base.OnExit(e);
        }
    }
}

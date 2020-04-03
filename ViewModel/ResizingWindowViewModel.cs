using System.Windows.Input;
using SnapNET.Model.Monitor;
using SnapNET.Model.Settings;
using SnapNET.Model.Window;

namespace SnapNET.ViewModel
{
    /// <summary>
    /// ViewModel for the resizing window (one for each window)
    /// </summary>
    internal class ResizingWindowViewModel : BaseViewModel
    {

        // ***** Private members *****

        private ICommand _resizeCommand;
        private readonly Monitor _monitor;
        private readonly GridSettings _gridSettings;
        private (int, int, int, int) _gridSelection;


        // ***** Public members *****

        /// <summary>
        /// Reference to the viewmodel shared by all windows
        /// </summary>
        public ResizingWindowSharedViewModel Shared { get; private set; }

        /// <summary>
        /// Current selection in the grid
        /// </summary>
        public (int l, int t, int w, int h) GridSelection {
            get => _gridSelection;
            set {
                _gridSelection = value;
                OnPropertyChanged(nameof(GridSelection));
            }
        }

        /// <summary>
        /// Test command to resize
        /// </summary>
        public ICommand ResizeCommand
            => _resizeCommand ?? (_resizeCommand = new CommandHandler(() => {
                var (l, t, w, h) = GridSelection;
                var rect = _gridSettings.ApplyIndicesToMonitor(_monitor, l, w, t, h);
                WindowHelper.SetWindowSize(Shared.ForegroundWindowHandle, rect.Left, rect.Top, rect.Width, rect.Height, true);
            }, () => true));


        // ***** Constructor *****

        /// <summary>
        /// Creates a new viewmodel
        /// </summary>
        /// <param name="par">The shared viewmodel reference</param>
        /// <param name="mon">The monitor this window is shown on</param>
        /// <param name="gridSettings">The gridsettings for this monitor</param>
        internal ResizingWindowViewModel(ResizingWindowSharedViewModel par, Monitor mon, GridSettings gridSettings)
        {
            Shared = par;
            _monitor = mon;
            _gridSettings = gridSettings;
        }
    }
}

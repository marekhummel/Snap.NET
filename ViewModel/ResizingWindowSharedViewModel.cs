using System;
using System.Windows.Input;
using SnapNET.Model.Monitor;

namespace SnapNET.ViewModel
{
    /// <summary>
    /// View model for the resizing window shared among all instances
    /// </summary>
    internal class ResizingWindowSharedViewModel : BaseViewModel
    {

        // ***** Private members *****

        private bool _isVisible;
        private string _foregroundWindowTitle;
        private ICommand _exitCommand, _saveCommand;



        // ***** PUBLIC  FIELDS *****

        /// <summary>
        /// ???
        /// </summary>
        public Monitor Monitor { get; set; }

        /// <summary>
        /// Flag for visibility
        /// </summary>
        public bool IsVisible {
            get => _isVisible;
            set {
                _isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }

        /// <summary>
        /// Handle of the current foreground window
        /// </summary>
        public IntPtr ForegroundWindowHandle { get; set; }

        /// <summary>
        /// Title of foreground window
        /// </summary>
        public string ForegroundWindowTitle
        {
            get => _foregroundWindowTitle;
            set {
                _foregroundWindowTitle = value;
                OnPropertyChanged(nameof(ForegroundWindowTitle));
            }
        }

        /// <summary>
        /// Command to minimize windows
        /// </summary>
        public ICommand ExitCommand
            => _exitCommand ?? (_exitCommand = new CommandHandler(() => IsVisible = false, () => true));

        /// <summary>
        /// Command to save settings
        /// </summary>
        public ICommand SaveCommand
            => _saveCommand ?? (_saveCommand = new CommandHandler(() 
                => Model.Settings.SettingsManager.StoreSettings(), () => true));
    }
}

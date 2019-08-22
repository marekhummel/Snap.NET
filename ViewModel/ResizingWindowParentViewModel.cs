using SnapNET.Model;
using SnapNET.Model.Monitor;
using SnapNET.Model.Window;
using SnapNET.View;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace SnapNET.ViewModel
{
    internal class ResizingWindowParentViewModel : BaseViewModel
    {

        public Monitor Monitor { get; set; }
        public IntPtr ForegroundWindowHandle { get; set; }


        private bool _isVisible;
        private string _foregroundWindowTitle;
        private ICommand _exitCommand;

        public bool IsVisible
        {
            get => _isVisible;
            set {
                _isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }

        public string ForegroundWindowTitle
        {
            get => _foregroundWindowTitle;
            set {
                _foregroundWindowTitle = value;
                OnPropertyChanged(nameof(ForegroundWindowTitle));
            }
        }

      
        public ICommand ExitCommand
            => _exitCommand ?? (_exitCommand = new CommandHandler(() => { IsVisible = false; }, () => true));
    }
}

using System.Windows.Input;

namespace SnapNET.ViewModel
{
    internal class ResizingWindowViewModel : BaseViewModel
    {

        private bool _isVisible = true;
        public bool IsVisible
        {
            get => _isVisible;
            set {
                _isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }



        private ICommand _exitClickCommand;
        public ICommand ExitClickCommand
        {
            get {
                return _exitClickCommand ?? (_exitClickCommand = new CommandHandler(() => { IsVisible = false; }, () => true));
            }
        }
    }
}

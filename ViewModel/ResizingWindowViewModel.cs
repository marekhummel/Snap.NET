using System.Windows.Input;
using SnapNET.Model.Monitor;
using SnapNET.Model.Window;

namespace SnapNET.ViewModel
{
    internal class ResizingWindowViewModel : BaseViewModel
    {
        private ICommand _resizeCommand;

        public ResizingWindowSharedViewModel Shared { get; private set; }
        public Monitor Monitor { get; private set; }

        public ResizingWindowViewModel(ResizingWindowSharedViewModel par, Monitor mon)
        {
            Shared = par;
            Monitor = mon;
        }

        public ICommand ResizeCommand
            => _resizeCommand ?? (_resizeCommand = new CommandHandler(() => {
                var wa = Monitor.WorkingArea;
                WindowHelper.SetWindowSize(Shared.ForegroundWindowHandle, wa.Left, wa.Top, wa.Width / 2, wa.Height);
            }, () => true));
    }
}

using System.Windows.Input;
using SnapNET.Model.Monitor;
using SnapNET.Model.Settings;
using SnapNET.Model.Window;

namespace SnapNET.ViewModel
{
    internal class ResizingWindowViewModel : BaseViewModel
    {
        private ICommand _resizeCommand;
        private readonly Monitor _monitor;
        private readonly GridSettings _gridSettings;

        public ResizingWindowSharedViewModel Shared { get; private set; }


        internal ResizingWindowViewModel(ResizingWindowSharedViewModel par, Monitor mon, GridSettings gridSettings)
        {
            Shared = par;
            _monitor = mon;
            _gridSettings = gridSettings;
        }

        public ICommand ResizeCommand
            => _resizeCommand ?? (_resizeCommand = new CommandHandler(() => {
                var rect = _gridSettings.GetTileSpanAtIndices(_monitor, 0, 2, 0, 4);
                WindowHelper.SetWindowSize(Shared.ForegroundWindowHandle, rect.Left, rect.Top, rect.Width, rect.Height, true);
            }, () => true));
    }
}

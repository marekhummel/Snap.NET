using SnapNET.Model;
using SnapNET.Model.Monitor;
using SnapNET.Model.Window;
using SnapNET.View;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace SnapNET.ViewModel
{
    internal class ResizingWindowChildViewModel : BaseViewModel
    {
        private ICommand _resizeCommand;

        public ResizingWindowParentViewModel Parent { get; private set; }
        public Monitor Monitor { get; set; }

        public ResizingWindowChildViewModel(ResizingWindowParentViewModel par)
        {
            Parent = par;
        }

        public ICommand ResizeCommand 
            => _resizeCommand ?? (_resizeCommand = new CommandHandler(() => {
                    var wa = Monitor.WorkingArea;
                    WindowHelper.SetWindowSize(Parent.ForegroundWindowHandle, wa.Left, wa.Top, wa.Width / 2, wa.Height);
                }, () => true));
    }
}

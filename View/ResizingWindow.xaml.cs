using SnapNET.Model.Monitor;
using SnapNET.Model.Window;
using System.Windows;

namespace SnapNET.View
{
    /// <summary>
    /// Interaction logic for ResizingWindow.xaml
    /// </summary>
    public partial class ResizingWindow : Window
    {
        public Monitor Monitor { get; set; }


        public ResizingWindow()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            // ToDo: Doesn't close all windows
            Visibility = Visibility.Hidden;
        }

        private void SnapTest1Button_Click(object sender, RoutedEventArgs e)
        {
            var hwnd = WindowHelper.GetCurrentForegroundWindow().Handle;
            var wa = Monitor.WorkingArea;
            WindowHelper.SetWindowSize(hwnd, wa.Left, wa.Top, wa.Width / 2, wa.Height);
        }

        private void SnapTest2Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

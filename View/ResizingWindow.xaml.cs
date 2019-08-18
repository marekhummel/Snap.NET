﻿using System.Windows;

namespace SnapNET.View
{
    /// <summary>
    /// Interaction logic for ResizingWindow.xaml
    /// </summary>
    public partial class ResizingWindow : Window
    {
        public ResizingWindow()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            // ToDo: Doesn't close all windows
            this.Visibility = Visibility.Hidden;
        }
    }
}

using System.ComponentModel;

namespace SnapNET.ViewModel
{
    /// <summary>
    /// Base view model class for implementation of <see cref="INotifyPropertyChanged"/>
    /// </summary>
    internal class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Event fired when any property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Helper method to invoke event
        /// </summary>
        /// <param name="name">The name of the modified property</param>
        protected void OnPropertyChanged(string name) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

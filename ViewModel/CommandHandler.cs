using System;
using System.Windows.Input;

namespace SnapNET.ViewModel
{
    /// <summary>
    /// Command class for basic xaml commands
    /// </summary>
    internal class CommandHandler : ICommand
    {

        // ***** Private members *****

        private readonly Action _action;
        private readonly Func<bool> _canExecute;


        // ***** Public members *****

        /// <summary>
        /// Wires CanExecuteChanged event 
        /// </summary>
        public event EventHandler? CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        // ***** Constructor *****

        /// <summary>
        /// Creates instance of the command handler
        /// </summary>
        /// <param name="action">Action to be executed by the command</param>
        /// <param name="canExecute">A bolean property to containing current permissions to execute the command</param>
        public CommandHandler(Action action, Func<bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }


        // ***** Public methods *****

        /// <summary>
        /// Explicit check if execute is allowed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object? parameter)
            => _canExecute.Invoke();

        /// <summary>
        /// Executes action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object? parameter)
            => _action();
    }
}

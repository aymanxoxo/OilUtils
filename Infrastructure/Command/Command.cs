using System;
using System.Windows.Input;

namespace Infrastructure.Command
{
    public partial class Command<T> : ICommand
    {
        private readonly Action<T> _executeAction;

        private readonly Func<T, bool> _canExecuteAction;

        public Command(Action<T> executeAction)
        {
            _executeAction = executeAction;
        }

        public Command(Action<T> executeAction, Func<T, bool> canExecuteAction) : this(executeAction)
        {
            _canExecuteAction = canExecuteAction;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_canExecuteAction == null)
            {
                return true;
            }

            return _canExecuteAction.Invoke((T)parameter);
        }

        public void Execute(object parameter)
        {
            if (_executeAction != null)
            {
                _executeAction.Invoke((T)parameter);
            }
        }
    }
}

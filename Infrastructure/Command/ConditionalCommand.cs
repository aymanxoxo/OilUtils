using System;
using System.Windows.Input;

namespace Infrastructure.Command
{
    public partial class Command<T, B> : ICommand
    {
        private Action<T> _execute;
        private Func<B, bool> _canExecute;

        public Command(Action<T> execute, Func<B, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute.Invoke((B)parameter);
        }

        public void Execute(object parameter)
        {
            if (_execute != null)
            {
                _execute.Invoke((T)parameter);
            }
        }
    }
}

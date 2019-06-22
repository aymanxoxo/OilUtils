using System;
using System.Windows.Input;

namespace Infrastructure.Command
{
    public partial class Command : ICommand
    {
        private readonly Action _execute;

        public Command(Action execute)
        {
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            if (_execute != null)
            {
                _execute.Invoke();
            }
        }
    }
}

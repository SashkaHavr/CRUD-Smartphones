using System;
using System.Windows.Input;

namespace CRUD_proj.Infrastructure
{
    class RelayCommand : ICommand
    {
        Action<object> action;
        Predicate<object> predicate;

        public RelayCommand(Action<object> act, Predicate<object> pred = null)
        {
            action = act;
            predicate = pred;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter) => predicate == null ? true : predicate(parameter);

        public void Execute(object parameter) => action(parameter);
    }
}

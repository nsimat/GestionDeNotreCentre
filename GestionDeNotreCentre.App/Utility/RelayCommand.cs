using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GestionDeNotreCentre.App.Utility
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;
        private Action saveModule;
        private Func<object, bool> canSaveModule;

        public RelayCommand(Action<object> executeMethod, Predicate<object> canExecuteMethod)
        {
            execute = executeMethod;
            canExecute = canExecuteMethod;
        }

        public RelayCommand(Action saveModule, Func<object, bool> canSaveModule)
        {
            this.saveModule = saveModule;
            this.canSaveModule = canSaveModule;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            bool b = canExecute == null ? true : canExecute(parameter);
            return b;
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }

    public class RelayCommand<T> : ICommand
    {
        Action<T> execute;
        Func<T, bool> canExecute;

        public RelayCommand(Action<T> executeMethod)
        {
            execute = executeMethod;
        }

        public RelayCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            execute = executeMethod;
            canExecute = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        #region ICommand Members

        bool ICommand.CanExecute(object parameter)
        {
            if (canExecute != null)
            {
                T tparm = (T)parameter;
                return canExecute(tparm);
            }
            if (execute != null)
            {
                return true;
            }
            return false;
        }

        // Beware - should use weak references if command instance lifetime is longer than lifetime of UI objects that get hooked up to command
        // Prism commands solve this in their implementation
        public event EventHandler CanExecuteChanged = delegate { };

        void ICommand.Execute(object parameter)
        {
            if (execute != null)
            {
                execute((T)parameter);
            }
        }
        #endregion
    }
}

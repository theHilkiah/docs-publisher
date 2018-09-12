using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyCSApp.Program.Core
{
    abstract class CMDCore : ICommand
    {
        public Predicate<object> canExecute;
        public Action<object> execute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null ? true : this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            if (this.canExecute != null) this.execute(parameter);
            else return;
        }

        public CMDCore(Predicate<object> CanExecute = null, Action<object> Execute = null)
        {
            if (Execute != null) this.execute = Execute;
            if (CanExecute != null) this.canExecute = CanExecute;
        }
    }
}

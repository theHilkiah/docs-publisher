using Sgml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace DocsPublisher.Program.Core
{
    class MainCore : INotifyPropertyChanged, INotifyDataErrorInfo, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = delegate { };
        public Predicate<object> canExecute;
        public Action<object> execute;

        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            NotifyPropertyChanged(propertyName);
            ValidateProperty(propertyName, value);
            return true;
        }

        private void ValidateProperty<T>(string propertyName, T value)
        {
            var results = new List<ValidationResult>();

            if (results.Any())
            {
               // _errors[propertyName] = results.Select(c => c.ErrorContent.ToString).ToList(); 
            }
            else
            {
                _errors.Remove(propertyName);
            }
            ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }
     

        public bool HasErrors
        {
            get { return _errors.Count > 0; }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public MainCore(Predicate<object> CanExecute = null, Action<object> Execute = null)
        {
            if (Execute != null) this.execute = Execute;
            if (CanExecute != null) this.canExecute = CanExecute;
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

        public IEnumerable GetErrors(string propertyName)
        {
            return (_errors.ContainsKey(propertyName))? _errors[propertyName]: null;
        }
    }
}

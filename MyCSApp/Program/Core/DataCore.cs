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

namespace MyCSApp.Program.Core
{
    abstract class DataCore : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = delegate { };

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
                //_errors?.Remove(propertyName);
            }
            ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }
     

        public bool HasErrors
        {
            get { return _errors.Count > 0; }
        }

        
        public IEnumerable GetErrors(string propertyName)
        {
            return (_errors.ContainsKey(propertyName))? _errors[propertyName]: null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSApp.Program.App.DataObjects
{
    class AppStarted: DataObject
    {
        private dynamic _inputDoc;
        public dynamic InputDoc
        {
            get { return _inputDoc; }
            set { SetField(ref _inputDoc, value); }
        }

        private dynamic _inputStyle;
        public dynamic InputStyle
        {
            get { return _inputStyle; }
            set { SetField(ref _inputStyle, value); }
        }

        private dynamic _outputDoc;
        public dynamic OutputDoc
        {
            get { return _outputDoc; }
            set { SetField(ref _outputDoc, value); }
        }

        public AppStarted()
        {

        }
    }
}

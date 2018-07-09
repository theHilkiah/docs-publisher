using DocsPublisher.Program.App.DataObjects;
using DocsPublisher.UIView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace DocsPublisher.Program.App.ViewObjects
{
    class RunDocsPublisher: ViewObject
    {
        public dynamic Data { get; }
        public dynamic CMD { get; }
        public UserControl UI;

        private ICommand _runApp;

        public ICommand RunApp
        {
            get { return _runApp ?? (_runApp = runCMD(f => canRunApp(f), f => doRunApp(f))); }
        }

        private bool canRunApp(object param)
        {
            //throw new NotImplementedException();
            return true;
        }

        private void doRunApp(object param)
        {
            //throw new NotImplementedException();
            
        }

        public RunDocsPublisher()
        {
            this.UI = new DocsCollectorUI();
        }
    }
}

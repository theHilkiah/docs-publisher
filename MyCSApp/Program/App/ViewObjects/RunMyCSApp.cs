using MyCSApp.Program.App.DataObjects;
using MyCSApp.UIView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyCSApp.Program.App.ViewObjects
{
    class RunMyCSApp: ViewObject
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

        public RunMyCSApp()
        {
            this.UI = new AppStarterUI();
        }
    }
}

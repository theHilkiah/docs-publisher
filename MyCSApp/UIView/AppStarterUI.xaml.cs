using MyCSApp.Program.App.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyCSApp.UIView
{
    /// <summary>
    /// Interaction logic for AppStarterUI.xaml
    /// </summary>
    public partial class AppStarterUI : UserControl
    {
        public AppStarterUI()
        {
            InitializeComponent();
            this.DataContext = new AppStarted();
        }
    }
}

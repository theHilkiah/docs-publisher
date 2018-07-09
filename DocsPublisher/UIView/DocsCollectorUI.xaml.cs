using DocsPublisher.Program.App.DataObjects;
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

namespace DocsPublisher.UIView
{
    /// <summary>
    /// Interaction logic for DocsCollectorUI.xaml
    /// </summary>
    public partial class DocsCollectorUI : UserControl
    {
        public DocsCollectorUI()
        {
            InitializeComponent();
            this.DataContext = new DocsCollected();
        }
    }
}

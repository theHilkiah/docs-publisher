﻿using DocsPublisher.Program.App.DataObjects;
using DocsPublisher.Program.App.ViewObjects;
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
    /// Interaction logic for DocsCollector.xaml
    /// </summary>
    public partial class DocsCollector : UserControl
    {
        public DocsCollector()
        {
            InitializeComponent();
            this.DataContext = new DocsCollected();
        }
    }
}
using DocsPublisher.Program.Core;
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

namespace DocsPublisher.Program.App.DataObjects
{
    class DataObject : DataCore
    {

        private dynamic _data;
        public dynamic DATA
        {
            get { return _data; }
            set { SetField(ref _data, value); }
        }

        //private ContentControl _ui;
        //public ContentControl UI
        //{
        //    get { return _ui; }
        //    set { SetField(ref _ui, value); }
        //}

        private string _logTitle;
        public string AppLogTitle
        {
            get { return _logTitle; }
            set { SetField(ref _logTitle, value); }
        }

        private string _logEntry;

        public string AppLogEntry
        {
            get { return _logEntry; }
            set { SetField(ref _logEntry, value); }
        }

        private int _progress;
        public int AppProgress
        {
            get { return _progress; }
            set { SetField(ref _progress, value); }
        }



        public void AddLogTitle(string logTitle)
        {
            AppLogTitle = logTitle;
        }

        public void ShowProgress(int progress)
        {
            AppProgress = progress;
        }

        public void AddLogEntry(string logEntry, bool? concat)
        {

            if (concat == true)
                AppLogEntry += logEntry + Environment.NewLine;
            else
                AppLogEntry = logEntry;
        }

    }
}

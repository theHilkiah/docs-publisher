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
using WinForms = System.Windows.Forms;

namespace DocsPublisher.Program.Core
{
    class AppsCore : MainCore
    {
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

        private ICommand _goPrev;
        public ICommand GoPrev
        {
            get { return _goPrev ?? (_goPrev = new MainCore(p => CanGoPrev(p), p => GoPrevAction(p))); }
        }

        public virtual void GoPrevAction(object param) { return; }

        public virtual bool CanGoPrev(object param) { return false; }

        private ICommand _goNext;
        public ICommand GoNext
        {
            get { return _goNext ?? (_goNext = new MainCore(p => CanGoNext(p), p => GoNextAction(p))); }
        }

        public virtual void GoNextAction(object param) { return; }

        public virtual bool CanGoNext(object param) { return false; }

        private ICommand _browseFolder;

        public ICommand BrowseFolder
        {
            get { return _browseFolder ?? (_browseFolder = new MainCore(p => true, p => BrowseFolderAction(p))); ; }
        }

        private void BrowseFolderAction(dynamic param)
        {
            WinForms.FolderBrowserDialog FolderBrowser = new WinForms.FolderBrowserDialog();

            if (param is TextBox) FolderBrowser.SelectedPath = (param as TextBox).Text;
            if (FolderBrowser.ShowDialog() != WinForms.DialogResult.OK) return;
            if (param is TextBox) (param as TextBox).Text = FolderBrowser.SelectedPath;
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

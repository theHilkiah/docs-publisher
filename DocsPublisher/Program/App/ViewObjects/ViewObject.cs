using DocsPublisher.Program.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WinForms = System.Windows.Forms;

namespace DocsPublisher.Program.App.ViewObjects
{
    class ViewObject : CMDCore
    {

        public ViewObject(Predicate<object> CanExecute = null, Action<object> Execute = null) : base(CanExecute, Execute)
        {

        }

        public ICommand runCMD(Predicate<object> canExec = null, Action<object> execThis = null)
        {
            return new ViewObject(p => canExec(p), p => execThis(p));
        }

        private ICommand _goPrev;
        public ICommand GoPrev
        {
            get { return _goPrev ?? (_goPrev = runCMD(p => CanGoPrev(p), p => GoPrevAction(p))); }
        }

        public virtual void GoPrevAction(object param) { return; }

        public virtual bool CanGoPrev(object param) { return false; }

        private ICommand _goNext;
        public ICommand GoNext
        {
            get { return _goNext ?? (_goNext = runCMD(p => CanGoNext(p), p => GoNextAction(p))); }
        }

        public virtual void GoNextAction(object param) { return; }

        public virtual bool CanGoNext(object param) { return false; }

        private ICommand _browseFolder;

        public ICommand BrowseFolder
        {
            get { return _browseFolder ?? (_browseFolder = runCMD(p => true, p => BrowseFolderAction(p))); ; }
        }

        private void BrowseFolderAction(dynamic param)
        {
            WinForms.FolderBrowserDialog FolderBrowser = new WinForms.FolderBrowserDialog();

            if (param is TextBox) FolderBrowser.SelectedPath = (param as TextBox).Text;
            if (FolderBrowser.ShowDialog() != WinForms.DialogResult.OK) return;
            if (param is TextBox) (param as TextBox).Text = FolderBrowser.SelectedPath;
        }
    }
}

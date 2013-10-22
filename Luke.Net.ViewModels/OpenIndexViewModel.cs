using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luke.Net.Lib;
using System.IO;

namespace Luke.Net.ViewModels
{
    public class OpenIndexViewModel : Screen
    {
        private readonly ShellViewModel _root;
        private readonly IFolderBrowser _browser;
        private readonly IMessageBox _messageBox;

        private string _pathToIndex;
        private bool _openReadOnly;
        private bool _forceUnlock;

        public event EventHandler Cancel;
        public event EventHandler Okay;

        public OpenIndexViewModel(ShellViewModel root, IFolderBrowser folderBrowser, IMessageBox messageBox)
        {
            _root = root;
            _browser = folderBrowser;
            _messageBox = messageBox;

            this.DisplayName = "Open Lucene Index";
        }

        public string PathToIndex
        {
            get { return _pathToIndex; }
            set
            {
                _pathToIndex = value;
                NotifyOfPropertyChange(() => PathToIndex);
            }
        }

        public bool OpenReadOnly
        {
            get { return _openReadOnly; }
            set
            {
                _openReadOnly = value;
                NotifyOfPropertyChange(() => OpenReadOnly);
            }
        }

        public bool ForceUnlock
        {
            get { return _forceUnlock; }
            set
            {
                _forceUnlock = value;
                NotifyOfPropertyChange(() => ForceUnlock);
            }
        }

        public void Browse()
        {
            var result = _browser.ShowDialog();

            if (result)
            {
                PathToIndex = _browser.SelectedPath;
            }
        }

        public void OkayButton()
        {
            if (string.IsNullOrEmpty(_pathToIndex) || !Directory.Exists(_pathToIndex))
            {
                _messageBox.ShowOkay("You must select a valid index directory.", "Invalid Path");
                return;
            }

            var e = Okay;

            if (e != null)
                e(this, EventArgs.Empty);
        }

        public void CancelButton()
        {
            var e = Cancel;

            if (e != null)
                e(this, EventArgs.Empty);
        }

        internal void LoadFailed()
        {
            _messageBox.ShowOkay("That path is not a valid Lucene index.", "Invalid index path");
        }
    }
}

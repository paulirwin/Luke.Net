using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Luke.Net.Lib.UI
{
    public class FolderBrowser : IFolderBrowser
    {
        private readonly FolderBrowserDialog _dialog;

        public FolderBrowser()
        {
            _dialog = new FolderBrowserDialog();
        }

        public bool ShowDialog()
        {
            var result = _dialog.ShowDialog();

            return result == DialogResult.OK;
        }

        public string SelectedPath
        {
            get { return _dialog.SelectedPath; }
        }
    }
}

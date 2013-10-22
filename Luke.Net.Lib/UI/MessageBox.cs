using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Luke.Net.Lib.UI
{
    public class MessageBox : IMessageBox
    {
        public bool ShowOkayCancel(string text, string title)
        {
            var result = System.Windows.MessageBox.Show(text, title, MessageBoxButton.OKCancel);
            return result == MessageBoxResult.OK;
        }

        public bool ShowOkay(string text, string title)
        {
            var result = System.Windows.MessageBox.Show(text, title, MessageBoxButton.OK);
            return result == MessageBoxResult.OK;
        }
    }
}

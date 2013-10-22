using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luke.Net.Lib
{
    public interface IMessageBox
    {
        bool ShowOkayCancel(string text, string title);

        bool ShowOkay(string text, string title);
    }
}

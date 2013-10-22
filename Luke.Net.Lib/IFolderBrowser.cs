using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luke.Net.Lib
{
    public interface IFolderBrowser
    {
        bool ShowDialog();

        string SelectedPath { get; }
    }
}

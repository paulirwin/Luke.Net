using Luke.Net.Lib.Index;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luke.Net.Lib
{
    public interface IIndexService
    {
        bool IsLoaded { get; }

        bool TryLoad(string path, IndexLoadOptions options);

        string IndexPath { get; }

        int MaxDoc { get; }

        int NumDeletedDocs { get; }

        int NumberOfFields { get; }

        long NumberOfTerms { get; }

        bool IsOptimized { get; }

        DateTime? LastModified { get; }
    }
}

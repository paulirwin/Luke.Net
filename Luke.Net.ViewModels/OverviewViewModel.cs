using Caliburn.Micro;
using Luke.Net.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luke.Net.ViewModels
{
    public class OverviewViewModel
    {
        private readonly IIndexService _indexService;

        public OverviewViewModel(IIndexService indexService)
        {
            _indexService = indexService;
        }

        public string IndexPath
        {
            get { return _indexService.IndexPath; }
        }

        public int NumberOfFields
        {
            get { return _indexService.NumberOfFields; }
        }

        public int NumberOfDocuments
        {
            get { return _indexService.MaxDoc - _indexService.NumDeletedDocs; }
        }

        public int NumDeletedDocs
        {
            get { return _indexService.NumDeletedDocs; }
        }

        public string HasDeletionsString
        {
            get { return _indexService.NumDeletedDocs > 0 ? "Yes (" + _indexService.NumDeletedDocs + ")" : "No"; }
        }

        public string IsOptimizedString
        {
            get { return _indexService.IsOptimized ? "Yes" : "No"; }
        }

        public long NumberOfTerms
        {
            get { return _indexService.NumberOfTerms; }
        }

        public string LastModifiedString
        {
            get { return _indexService.LastModified.HasValue ? _indexService.LastModified.Value.ToLongDateString() + " " + _indexService.LastModified.Value.ToLongTimeString() + " UTC" : ""; }
        }
    }
}

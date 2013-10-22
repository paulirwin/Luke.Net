using Lucene.Net.Index;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luke.Net.Lib.Index
{
    public class IndexService : IIndexService
    {
        private bool _loaded;

        private FSDirectory _directory;
        private IndexReader _reader;
        private int _numFields;
        private long _numTerms;
        private DateTime? _lastModified;

        private IDictionary<string, FieldTermCount> _termCounts;

        public bool IsLoaded
        {
            get { return _loaded; }
        }
        
        public bool TryLoad(string path, IndexLoadOptions options)
        {
            try
            {
                _directory = FSDirectory.Open(path);
                _reader = IndexReader.Open(_directory, options.ReadOnly);

                if (options.ForceUnlock && _directory.FileExists(IndexWriter.WRITE_LOCK_NAME))
                {
                    _directory.ClearLock(IndexWriter.WRITE_LOCK_NAME);
                }

                var fields = _reader.GetFieldNames(IndexReader.FieldOption.ALL);
                _numFields = fields.Count;

                CountTerms();

                foreach (var file in _directory.ListAll())
                {
                    try
                    {
                        string fpath = Path.Combine(_directory.Directory.ToString(), file);

                        if (_lastModified == null)
                            _lastModified = File.GetLastWriteTimeUtc(fpath);
                        else
                        {
                            var mod = File.GetLastWriteTimeUtc(fpath);
                            if (mod > _lastModified.Value)
                                _lastModified = mod;
                        }
                    }
                    catch
                    {
                        // ignore
                    }
                }

                _loaded = true;
                return true;
            }
            catch
            {
                _directory = null;
                _reader = null;
                _numFields = 0;
                _numTerms = 0;
                _loaded = false;
                _lastModified = null;
                return false;
            }
        }

        private void CountTerms()
        {
            _termCounts = new Dictionary<string, FieldTermCount>();

            _numTerms = 0;
            
            // TODO: add in this functionality from Lucene.net 4.x
        }

        public int MaxDoc
        {
            get { return _reader != null ? _reader.MaxDoc : 0; }
        }
        
        public int NumDeletedDocs
        {
            get { return _reader != null ? _reader.NumDeletedDocs : 0; }
        }
        
        public string IndexPath
        {
            get { return _directory != null ? _directory.Directory.ToString() : string.Empty; }
        }
        
        public int NumberOfFields
        {
            get { return _numFields; }
        }

        public bool IsOptimized
        {
            get { return _reader != null ? _reader.IsOptimized() : false; }
        }
        
        public long NumberOfTerms
        {
            get { return _numTerms; }
        }
        
        public DateTime? LastModified
        {
            get { return _lastModified; }
        }
    }
}

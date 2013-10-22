using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luke.Net.Lib.Index
{
    public class FieldTermCount : IComparable<FieldTermCount>
    {
        public string fieldname;
        public long termCount;

        public int CompareTo(FieldTermCount f2)
        {
            if (termCount > f2.termCount)
            {
                return -1;
            }
            else if (termCount < f2.termCount)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}

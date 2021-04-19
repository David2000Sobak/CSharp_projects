using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelFileUnion
{
    class Worksheet
    {
        public WhHeader Headers { get; set; }
        public List<ChannelSpan> Data { get; set; }
    }
}

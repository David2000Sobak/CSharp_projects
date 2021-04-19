using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelFileUnion
{
    class ChannelSpan
    {
        //Подтаблицы нахоящиеся в листах
        public string Name { get; set; }
        public List<Equipment> Span { get; set; }
    }
}

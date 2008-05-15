using System;
using System.Collections.Generic;

using System.Text;

namespace Tools.Common.DataTables
{
    public struct DataContextEntry<T, CT>
    {
        public string Name { get; set; }
        public T Value { get; set; }
        public CT Data { get; set; }
    }
}

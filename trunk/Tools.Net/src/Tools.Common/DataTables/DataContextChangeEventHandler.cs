using System;
using System.Collections.Generic;

using System.Text;
using System.Collections;

namespace Tools.Common.DataTables
{
    public class DataContextChangeEventArgs<T, CT>
    {
        IDictionary<string, DataContextEntry<T, CT>> contextEntries;


        public DataContextChangeEventArgs(IDictionary<string, DataContextEntry<T, CT>> contextEntries)
        {
            this.contextEntries = contextEntries;
        }

    }
}

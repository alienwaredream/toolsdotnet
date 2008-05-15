using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Tools.Common.DataTables
{
    public interface IDataTableTransformer
    {
        DataTable Transform(DataTable table);
    }
}

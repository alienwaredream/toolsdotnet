using System;
using System.Collections.Generic;

using System.Text;
using System.Data.SqlClient;

namespace Tools.Common.DataTables
{
    public class QueryDetails
    {
        public string CommandName { get; set; }
        public List<string> Parameters { get; set; }
    }
}

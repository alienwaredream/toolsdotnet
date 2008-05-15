using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools.Common.DataTables
{
    public class TransposeDefinition
    {
        public string KeyColumnName { get; set; }
        public string SourceNameColumnName { get; set; }
        public string SourceValueColumnName { get; set; }
        /// <summary>
        /// If set to the non-empty value, arranges the transpositions sums to be
        /// targeted by the filter based values.
        /// </summary>
        public string FilterName { get; set; }

    }
}

using System;
using System.Collections.Generic;

using System.Text;
using System.Globalization;
using System.Data;
using System.Collections;

namespace Tools.Common.Utils
{
    public static class SortUtility
    {
        /// <summary>
        /// Toggles the order of the sort given the current sort order.
        /// </summary>
        /// <param name="order">Current sort order</param>
        /// <returns>Toggled sort order</returns>
        /// <remarks>This default implementation toggles from none to asc then desc, and then
        /// toggles only between asc and desc. Add more methods if anything else is required.</remarks>
        public static SortOrder ToggleOrder(SortOrder order)
        {
            switch (order)
            {
                case SortOrder.None: return SortOrder.Asc;
                case SortOrder.Asc: return SortOrder.Desc;
                case SortOrder.Desc: return SortOrder.Asc;
            }
            throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, 
                "Unexpected SortOrder order argument of value {0}, review the implementation!", order.ToString()));
        }
        /// <summary>
        /// Sorts the <see cref="System.Data.DataTable"/>.
        /// </summary>
        /// <param name="sourceTable">The source table.</param>
        /// <param name="sortColumn">The column to sort by.</param>
        /// <param name="order">The <see cref="Tools.Common.SortOrder"/>.</param>
        /// <param name="copyIfNoSort">Should only be true when one table owner is assumed
        /// other time and space.</param>
        /// <returns></returns>
        public static DataTable SortDataTable(DataTable sourceTable, string sortColumn, SortOrder order, bool copyIfNoSort)
        {
            if (String.IsNullOrEmpty(sortColumn))
            {
                // copyIfNoSort make the semantic more stable, even if there is no sort required
                // the copy is returned, so the caller may assume same level of independence.
                if (copyIfNoSort) return sourceTable.Copy();
                return sourceTable;
            }

            DataTable targetTable = sourceTable.Clone();

            DataRow[] sourceRows = sourceTable.Select(null, 
                ("[" + sortColumn + "] " + order.ToString()).TrimEnd(' '));

            for (int i = 0; i < sourceRows.Length; i++)
            {
                DataRow newRow = targetTable.NewRow();

                newRow.ItemArray = sourceRows[i].ItemArray;

                targetTable.Rows.Add(newRow);
            }
            return targetTable;
        }
    }
}

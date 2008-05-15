using System;
using System.Collections.Generic;

using System.Text;
using Tools.Common.DataAccess;
using System.Data;
using System.Collections;
using Tools.Common.Utils;

namespace Tools.Common.DataTables
{
    //TODO: (SD) resolve when column names will be dups because of the transposed column values
    public class TransposeDataTableTransformer : IDataTableTransformer
    {
        private TransposeDefinition transposeDefinition;

        protected TransposeDefinition TransposeDefinition
        {
            get { return transposeDefinition; }
        }

        public TransposeDataTableTransformer(TransposeDefinition transposeDefinition)
        {
            this.transposeDefinition = transposeDefinition;
        }

        #region IDataTableTransformer Members

        public virtual System.Data.DataTable Transform(DataTable sourceTable)
        {
            DataTable retTable = sourceTable.Clone(); // only clones the structure

            SortedList<string, string> transposeNames = new SortedList<string, string>();

            PrepareColumns(sourceTable, retTable, transposeNames);

            SortedList<string, DataRow> keyRows = new SortedList<string, DataRow>();

            for (int i = 0; i < sourceTable.Rows.Count; i++)
            {
                DataRow sourceRow = sourceTable.Rows[i];

                // PrepareTransposeKeyKeyColumns(sourceTable, retTable, transposeNames);

                // Null values are normalized to be an empty string

                string trKeyName = String.Empty; 
                object sourceNameObject = sourceRow[transposeDefinition.SourceNameColumnName];

                if (sourceNameObject != null)
                {
                    trKeyName = sourceNameObject.ToString();
                }

                string dataKeyName = String.Empty;
                object dataKeyNameObject = sourceRow[transposeDefinition.KeyColumnName];
                
                if (dataKeyNameObject != null)
                {
                    dataKeyName = dataKeyNameObject.ToString();
                }

                //**

                if (!keyRows.ContainsKey(dataKeyName))
                {
                    DataRow dr = retTable.NewRow();

                    keyRows.Add(dataKeyName, dr);

                    retTable.Rows.Add(dr);

                    FillRow(sourceTable, sourceRow, dr, trKeyName);
                }
                else
                {
                    FillRow(sourceTable, sourceRow, keyRows[dataKeyName], trKeyName);
                }
            }

            ReshuffleColumns(retTable, transposeNames);

            return retTable;


        }
        

        protected void ReshuffleColumns(DataTable targetTable, IDictionary<string, string> transposeNames)
        {
            
        }

        protected virtual void PrepareColumns(DataTable sourceTable, DataTable targetTable,
            IDictionary<string, string> transposeNames)
        {
            // remove the columns that should be transposed
            for (int i = 0; i < targetTable.Columns.Count; i++)
            {

                if (targetTable.Columns[i].ColumnName == transposeDefinition.SourceNameColumnName
                     || targetTable.Columns[i].ColumnName == transposeDefinition.SourceValueColumnName)
                {
                    targetTable.Columns.RemoveAt(i);
                }
            }
            PrepareTransposeKeyKeyColumns(sourceTable, targetTable, transposeNames);

        }

        protected void FillRow(DataTable sourceTable, DataRow sourceRow, DataRow targetRow, string trKeyName)
        {
            for (int j = 0; j < sourceTable.Columns.Count; j++)
            {

                if (
                    sourceTable.Columns[j].ColumnName == transposeDefinition.SourceValueColumnName)
                {
                    HandleSourceValueColumn(sourceRow, targetRow, trKeyName, sourceRow[j]);
                    continue;
                }
                if (sourceTable.Columns[j].ColumnName == transposeDefinition.SourceNameColumnName)
                    continue;

                targetRow[sourceTable.Columns[j].ColumnName] = sourceRow[sourceTable.Columns[j].ColumnName];
            }
        }
        // that was refactored to do one more travserse, until SetOrdinal was found on the Column.
        // leaving for a moment here in case SetOrdinal idea would not work as expected (SD)
        private void PrepareTransposeKeyKeyColumns(
            DataTable sourceTable, DataTable targetTable, IDictionary<string, string> transposeNames)
        {

            for (int i = 0; i < sourceTable.Rows.Count; i++)
            {
                string trKeyName = sourceTable.Rows[i][transposeDefinition.SourceNameColumnName] as string 
                    ?? String.Empty;

                if (!transposeNames.ContainsKey(trKeyName))
                {
                    //TODO: (SD) refactor types of columns to be configurable 
                    targetTable.Columns.Add(
                        new DataColumn(trKeyName, typeof(long)));
                    transposeNames.Add(trKeyName, trKeyName);
                }
            }
        }


        protected virtual void HandleSourceValueColumn(DataRow sourceRow, DataRow targetRow, string trKeyName, object sourceValue)
        {
            targetRow[trKeyName] = ConvertSafelyToInt64(sourceValue, 0) +
                ConvertSafelyToInt64(targetRow[trKeyName], 0);
        }
        /// <summary>
        /// Provides safe conversion either to the underlying value or to the default fallback
        /// value if object value is DBNull.Value
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        protected Int64 ConvertSafelyToInt64(object val, Int64 fallbackValue)
        {
            return ConversionUtility.ConvertDBValueToInt64(val, fallbackValue);
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Web.Script.Serialization;

namespace Tools.Common.DataTables
{
    public class JavaScriptDataTableConverter : JavaScriptConverter
    {
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException("Deserialize is not implemented.");
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            DataTable dt = obj as DataTable;
            Dictionary<string, object> result = new Dictionary<string, object>();

            if (dt != null && dt.Rows.Count > 0)
            {
                // List for row values
                List<object> rowValues = new List<object>();
                List<object> colDefinitions = new List<object>();

                foreach (DataColumn dc in dt.Columns)
                {
                    Dictionary<string, object> colDefinition = new Dictionary<string, object>();
                    colDefinition.Add("name", dc.ColumnName); // col name
                    //colDefinition.Add("dataType", "String"); // TODO: (SD) have some resolver here
                    //colDefinition.Add("defaultValue", (dc.DefaultValue); // col name
                    colDefinitions.Add(colDefinition);
                }
                result["columns"] = colDefinitions;
                

                foreach (DataRow dr in dt.Rows)
                {
                    // Dictionary for col name / col value
                    Dictionary<string, object> colValues = new Dictionary<string, object>();

                    foreach (DataColumn dc in dt.Columns)
                    {
                        colValues.Add(dc.ColumnName, // col name
                         (string.Empty == dr[dc].ToString()) ? null : dr[dc]); // col value
                    }

                    // Add values to row
                    rowValues.Add(colValues);
                    //result.Add("row", colValues);
                }

                //// Add rows to serialized object
                result["rows"] = rowValues;
            }

            return result;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            //Define the DataTable as a supported type.
            get
            {
                return new System.Collections.ObjectModel.ReadOnlyCollection<Type>(
                 new List<Type>(
                  new Type[] { typeof(DataTable) }
                 )
                );
            }
        }

        public static string SerializeDataTable2Json(DataTable table)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            //DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(DataTable));
            serializer.RegisterConverters(new JavaScriptConverter[] {
                new JavaScriptDataTableConverter()});
            return serializer.Serialize(table);
        }
    }
}

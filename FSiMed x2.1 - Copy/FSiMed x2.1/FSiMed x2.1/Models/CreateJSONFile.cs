using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;

namespace FSiMed_x2._1.Models
{
    public static class CreateJSONFile
    {
        public static bool WriteJason(DataTable dt, string path)
        {
            try
            {

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, string>> rows = new List<Dictionary<string, string>>();
                Dictionary<string, string> row = null;

                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, string>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName.Trim().ToString(), Convert.ToString(dr[col]));
                    }
                    rows.Add(row);
                }
                string jsonstring = serializer.Serialize(rows);

                using (var file = new StreamWriter(path, false))
                {
                    file.Write(jsonstring);
                    file.Close();
                    file.Dispose();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
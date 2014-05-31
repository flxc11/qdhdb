using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CNVP.Framework.Helper;

namespace CNVP.UI
{
    public class Column
    {
        public static string GetCloumnName(string columnId)
        {
            string rslt = string.Empty;
            using (DataTable dataTable = DbHelper.ExecuteTable("select ColumnName,ColumnID from T_Column where ColumnID=" + columnId))
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    rslt = dataTable.Rows[0]["ColumnName"].ToString();
                }
            }
            return rslt;
        }
        
    }
}

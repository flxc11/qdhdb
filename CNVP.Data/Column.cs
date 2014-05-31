using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNVP.Data
{
    using System.Data;
    using System.Data.SqlClient;

    using CNVP.Framework.Helper;

    /// <summary>
    /// 栏目帮助类
    /// </summary>
    public class Column
    {
        private string StrSql = "";

        /// <summary>
        /// 获取授权内子类序号(返回值：,1,2,3,4)
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public string GetChildColumnID(int ParentID)
        {
            string Str = "";
            StrSql = "Select ColumnID From T_Column Where ParentID=@ParentID";
            IDataParameter[] Param = { 
                DbHelper.MakeParam("@ParentID", ParentID)
            };
            DataTable Dt = DbHelper.ExecuteTable(StrSql, Param);
            foreach (DataRow Row in Dt.Rows)
            {
                Str += "," + Row["ColumnID"].ToString();
                GetChildColumnID(Convert.ToInt32(Row["ColumnID"].ToString()));
            }
            return Str;
        }
    }
}

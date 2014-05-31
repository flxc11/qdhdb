using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CNVP.Framework.Helper;
using CNVP.Model;

namespace CNVP.Data
{
    public class NewsSourceData
    {
        #region 增加资源
        /// <summary>
        /// 增加资源
        /// </summary>
        /// <param name="model"></param>
        public int AddSource(NewsSourcesModel model)
        {
            string StrSql = "Insert Into T_NewsSources (FileName,FilePath,FileExtension,PostTime,AdminID) Values (@FileName,@FilePath,@FileExtension,@PostTime,@AdminID)";
            IDataParameter[] Param = new IDataParameter[]{
                DbHelper.MakeParam("@FileName",model.FileName),
                DbHelper.MakeParam("@FilePath",model.FilePath),
                DbHelper.MakeParam("@FileExtension",model.FileExtension),
                DbHelper.MakeParam("@PostTime",model.PostTime),
                DbHelper.MakeParam("@AdminID",model.AdminID)
            };
            return DbHelper.ExecuteSqlGetMaxID(StrSql, Param);
        }
        #endregion
    }
}

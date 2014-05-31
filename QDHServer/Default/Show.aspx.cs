using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QDHServer.Default
{
    using CNVP.Framework.Utils;

    /// <summary>
    /// 新闻显示页 列表页
    /// </summary>
    public partial class Show : System.Web.UI.Page
    {
        public string ColumnId = string.Empty;
        /// <summary>
        /// 默认 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ColumnId = Public.FilterSql(Request.Params["ClassID"]);
                if (string.IsNullOrEmpty(ColumnId) || !Public.IsNumber(ColumnId))
                {
                    ColumnId = "1";
                }
            }
        }
    }
}
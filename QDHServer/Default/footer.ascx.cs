using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CNVP.Framework.Helper;

namespace QDHServer.Default
{
    public partial class footer : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rptnotice.DataSource =
                    DbHelper.ExecuteTable(
                        "select top 5 NewsID,NewsTitle from T_NewsInfo where ColumnID=6 and NewsState=1 order by OrderID desc");
                rptnotice.DataBind();
            }
        }
    }
}
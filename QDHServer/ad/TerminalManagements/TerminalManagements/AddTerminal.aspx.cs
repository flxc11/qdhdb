using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QDHServer.ad.TerminalManagements.TerminalManagements
{
    public partial class AddTerminal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //返回
        protected void IbtnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("TerminalManagement.aspx");
        }
    }
}
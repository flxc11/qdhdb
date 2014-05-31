using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QDHServer.AppCode;

namespace QDHServer
{
    public partial class Self_Top : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            sysPublic.IsOnLine();
        }

        protected void imgExit_Click(object sender, ImageClickEventArgs e)
        {
            ResultData RetData = new ResultData();
            SQLHelper sh = new SQLHelper();
            //RetData = sh.LoginOut();
            if (RetData.IsErr)
            {
                sysPublic.ShowMsgBox("无法正常退出！");
                return;
            }
            Response.Write("<script language=\"javascript\" type=\"text/javascript\">window.parent.location=\"/login.aspx\"; </script>");
        }
    }
}

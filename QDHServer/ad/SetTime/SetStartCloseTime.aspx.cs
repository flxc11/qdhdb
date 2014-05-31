using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QDHServer.AppCode;
using System.Data;

namespace QDHServer.ad.SetTime
{
    public partial class SetStartCloseTime : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initTerminal();
            }
        }
        Common common = new Common();

        //初始化加载终端信息
        private void initTerminal()
        {
            try
            {
                string txtTerminal = "select TERMINALID,CLOSETIME from ADUSERTERMINAL";
                DataSet dsTerminal = common.GetGeneralTable("", "", txtTerminal, 2);
                dlTerminal.DataSource = dsTerminal;
                dlTerminal.DataBind();
            }
            catch (Exception)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertAgainError()</script>");
            }
        }

        //返回
        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("SetTime.aspx");
        }
    }
}
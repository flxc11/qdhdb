using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QDHConfig;
using System.Data.SqlClient;
using QDHServer.AppCode;
using System.Data;

namespace QDHServer.ad.TerminalManagements.GroupManagements
{
    public partial class UpdateTerminal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initUpdateTerminal();
            }
        }

        Common common = new Common();
        //返回
        protected void IbtnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("GroupManagement.aspx");
        }

        //初始化修改终端信息
        private void initUpdateTerminal()
        {
            try
            {
                string id = Request.QueryString["id"];
                /*显示在gridview中*/
                string adTerminal = "select * from T_TerminalType where TerminalTypeID=" + id + "";
                DataSet dsADTerminal = common.GetGeneralTable("", "", adTerminal, 2);
                txtGroupName.Text = dsADTerminal.Tables[0].Rows[0]["TerminalTypeName"].ToString();
            }
            catch (Exception)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertWarningNet()</script>");
            }
        }

        //保存
        protected void IbtnSave_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string id = Request.QueryString["id"];
                string connectString = BaseConfig.DataConnectString();
                SqlConnection myConnection = new SqlConnection(connectString);
                SqlCommand myCommand = myConnection.CreateCommand();
                myCommand.CommandText = "Update T_TerminalType set Updatetime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',TerminalTypeName='" + txtGroupName.Text + "' where TerminalTypeID=" + id + "";
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertUpdateSuccess()</script>");
            }
            catch (Exception)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertWarningNet()</script>");
            }
        }
    }
}
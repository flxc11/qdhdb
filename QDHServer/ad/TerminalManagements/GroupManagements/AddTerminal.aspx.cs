using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using QDHConfig;

namespace QDHServer.ad.TerminalManagements.GroupManagements
{
    public partial class AddTerminal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        //保存
        protected void IbtnSave_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtGroupName.Text == "")
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertGroupName()</script>");
                }
                else
                {
                    string connectString = BaseConfig.DataConnectString();
                    SqlConnection myConnection = new SqlConnection(connectString);
                    SqlCommand myCommand = myConnection.CreateCommand();
                    myCommand.CommandText = "insert into T_TerminalType(TerminalTypeName,TerminalTypeArea,OrderId,Createtime,Updatetime) values('" + txtGroupName.Text + "','','' ,'" + DateTime.Now + "','" + DateTime.Now + "')";
                    myConnection.Open();
                    myCommand.ExecuteNonQuery();
                    myConnection.Close();
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertAddSuccess()</script>");
                }
            }
            catch (Exception)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertWarningNet()</script>");
            }
        }

        //返回
        protected void IbtnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("GroupManagement.aspx");
        }
    }
}
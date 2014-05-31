using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using QDHServer.AppCode;
using System.Data.SqlClient;

namespace QDHServer.ad
{
    public partial class TermialLooks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //sysPublic.IsOnLine();  //判断是否登录
                //if (!sysPublic.IsLimit("t38"))
                //{
                //    Response.Redirect("~/NoLimit.htm");
                //    return;
                //}
                initTermialLook();
                initTermialAddLook();
            }
        }

        Common common = new Common();
        //初始化加载终端广告审核信息
        private void initTermialLook()
        {
            try
            {
                string txtTermial = "select distinct t.terminalid,t.picsize from ADtannounce t where status='0'";
                DataSet dsTermial = common.GetGeneralTable("", "", txtTermial, 2);
                dlTerminal.DataSource = dsTermial;
                dlTerminal.DataBind();
                lblTermial.Text = dsTermial.Tables[0].Rows.Count.ToString();
            }
            catch (Exception)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertErrorgDeletePic()</script>");
            }
        }

        //初始化新增
        private void initTermialAddLook()
        {
            try
            {
                string txtInsertTermial = "select distinct t.terminalid,t.picsize from adinsert t where updatestatus='3'";
                DataSet dsInsertTermial = common.GetGeneralTable("", "", txtInsertTermial, 2);
                dlTerminalAdd.DataSource = dsInsertTermial;
                dlTerminalAdd.DataBind();
                lblTerminalAdd.Text = dsInsertTermial.Tables[0].Rows.Count.ToString();
            }
            catch (Exception)
            {
                // Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请稍后再试')</script>");
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertErrorgDeletePic()</script>");
            }
        }

        //审核
        protected void IbtnLook_Click(object sender, ImageClickEventArgs e)
        {
            int counts = 0;
            try
            {
                for (int i = 0; i < dlTerminal.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)dlTerminal.Items[i].FindControl("ckbTermial");
                    HiddenField HidID = (HiddenField)dlTerminal.Items[i].FindControl("HidID");
                    if (cb.Checked)
                    {
                        string id = HidID.Value;
                        string connectString = ConfigurationManager.AppSettings["DataConnectString"];
                        SqlConnection myConnection = new SqlConnection(connectString);
                        SqlCommand myCommand = myConnection.CreateCommand();
                        myCommand.CommandText = "update ADTANNOUNCE set status='1' where terminalid='" + id + "' and status='0'";
                        myConnection.Open();
                        myCommand.ExecuteNonQuery();
                        myConnection.Close();
                        counts++;
                    }
                }
                initTermialLook();
            }
            catch (Exception)
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请稍后再试')</script>");
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertErrorgDeletePic()</script>");
            }
            if (counts == 0)
            {
                // Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请选择数据！')</script>");
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertWarningInputTerminal()</script>");
            }
            else
            {
                //  Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('审核成功！')</script>");
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertWarningSuccess()</script>");
            }
        }

        //新增  已发布终端审核
        protected void IbtnLookAdd_Click(object sender, ImageClickEventArgs e)
        {
            int counts = 0;
            try
            {
                //图片上传
                for (int i = 0; i < dlTerminalAdd.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)dlTerminalAdd.Items[i].FindControl("ckbTermialAdd");
                    HiddenField HidID = (HiddenField)dlTerminalAdd.Items[i].FindControl("HidIDAdd");
                    if (cb.Checked)
                    {
                        string id = HidID.Value;
                        string connectString = ConfigurationManager.AppSettings["DataConnectString"];
                        SqlConnection myConnection = new SqlConnection(connectString);
                        SqlCommand myCommand = myConnection.CreateCommand();
                        myCommand.CommandText = "update adinsert set updatestatus='4' where terminalid='" + id + "' and updatestatus='3'";
                        myConnection.Open();
                        myCommand.ExecuteNonQuery();
                        myConnection.Close();
                        counts++;
                    }
                }
                initTermialAddLook();
            }
            catch (Exception)
            {
                // Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请稍后再试')</script>");
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertErrorgDeletePic()</script>");
            }
            if (counts == 0)
            {
                // Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请选择数据！')</script>");
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertWarningInputTerminal()</script>");
            }
            else
            {
                // Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('审核成功！')</script>");
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertWarningSuccess()</script>");
            }
        }

        //返回
        protected void IbtnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/ad/PicShow.aspx");
        }
    }
}
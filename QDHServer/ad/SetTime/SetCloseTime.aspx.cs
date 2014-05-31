using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QDHServer.AppCode;
using System.Data.SqlClient;

namespace QDHServer.ad.SetTime
{
    public partial class SetCloseTime : System.Web.UI.Page
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
                string txtTerminal = "select TERMINALID from ADUSERTERMINAL";
                DataSet dsTerminal = common.GetGeneralTable("", "", txtTerminal, 2);
                PagedDataSource pds = new PagedDataSource();
                pds.AllowPaging = true;
                pds.PageSize = AspNetPager1.PageSize;
                pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
                pds.DataSource = dsTerminal.Tables[0].DefaultView;
                AspNetPager1.RecordCount = pds.DataSourceCount;
                dlTerminal.DataSource = pds;
                dlTerminal.DataBind();
            }
            catch (Exception)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertAgainError()</script>");
            }
        }

        //关机
        protected void lnkClose_Click(object sender, EventArgs e)
        {
            try
            {
                int counts = 0;
                for (int i = 0; i < dlTerminal.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)dlTerminal.Items[i].FindControl("ckbTerminal");
                    HiddenField HidTermial = (HiddenField)dlTerminal.Items[i].FindControl("HidID");
                    if (cb.Checked)
                    {
                        if (txtTime.Value == "")
                        {
                            string updateStatus = "update ADUSERTERMINAL set CLOSETIME='" + DateTime.Now.ToString("HH:mm:ss") + "',CLOSESTATUS='1' where TERMINALID='" + HidTermial.Value + "'";
                            SqlConnection conn = new SqlConnection(Common.connectionString);
                            conn.Open();
                            SqlCommand myCommand = conn.CreateCommand();
                            myCommand.CommandText = "" + updateStatus + "";
                            myCommand.ExecuteNonQuery();
                            counts++;
                        }
                        else
                        {
                            string updateStatus = "update ADUSERTERMINAL set CLOSETIME='" + txtTime.Value + "',CLOSESTATUS='1' where TERMINALID='" + HidTermial.Value + "'";
                            SqlConnection conn = new SqlConnection(Common.connectionString);
                            conn.Open();
                            SqlCommand myCommand = conn.CreateCommand();
                            myCommand.CommandText = "" + updateStatus + "";
                            myCommand.ExecuteNonQuery();
                            counts++;
                        }
                    }
                }
                if (counts == 0)    //没有选择1条记录
                {
                    //   Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请选择终端')</script>");
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertWarningLastPic()</script>");
                }
                else   //选择了记录
                {
                    // Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('文件发布成功');location.href='Default.aspx'</script>");
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>closeTime()</script>");
                }
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

        //取消关机时间
        protected void lnkCancleClose_Click(object sender, EventArgs e)
        {
            try
            {
                int counts = 0;
                for (int i = 0; i < dlTerminal.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)dlTerminal.Items[i].FindControl("ckbTerminal");
                    HiddenField HidTermial = (HiddenField)dlTerminal.Items[i].FindControl("HidID");
                    if (cb.Checked)
                    {
                        string updateStatus = "update ADUSERTERMINAL set CLOSETIME='未设置',CLOSESTATUS='0' where TERMINALID='" + HidTermial.Value + "'";
                        SqlConnection conn = new SqlConnection(Common.connectionString);
                        conn.Open();
                        SqlCommand myCommand = conn.CreateCommand();
                        myCommand.CommandText = "" + updateStatus + "";
                        myCommand.ExecuteNonQuery();
                        counts++;
                    }
                }
                if (counts == 0)    //没有选择1条记录
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertWarningLastPic()</script>");
                }
                else   //选择了记录
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>waiting()</script>");
                }
            }
            catch (Exception)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertAgainError()</script>");
            }
        }

        //搜索
        protected void lnkShowTerminal_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTerminal.Text == "")
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertInputTerminal()</script>");
                    initTerminal();
                }
                else
                {
                    string txtSQL = "select * from ADUSERTERMINAL  where TerminalID='" + txtTerminal.Text + "'";
                    DataSet dsTerminalID = common.GetGeneralTable("", "", txtSQL, 2);
                    if (dsTerminalID.Tables[0].Rows.Count > 0)
                    {
                        PagedDataSource pds = new PagedDataSource();
                        pds.AllowPaging = true;
                        pds.PageSize = AspNetPager1.PageSize;
                        pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
                        pds.DataSource = dsTerminalID.Tables[0].DefaultView;
                        AspNetPager1.RecordCount = pds.DataSourceCount;
                        dlTerminal.DataSource = pds;
                        dlTerminal.DataBind();
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertError()</script>");
                    }
                }
            }
            catch (Exception)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertAgainError()</script>");
            }
        }

        //分页改变
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            initTerminal();
        }
    }
}
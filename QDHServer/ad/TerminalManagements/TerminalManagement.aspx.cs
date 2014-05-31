using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using QDHServer.AppCode;
using QDHConfig;

namespace QDHServer.ad.TerminalManagement
{
    public partial class TerminalManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initTerminal();
            }
        }
        Common common = new Common();

        /*初始化加载终端信息*/
        private void initTerminal()
        {
            try
            {
                /*显示在lblCount中*/
                string terminalCount = "select * from T_Terminal";
                DataSet dsTerminal = common.GetGeneralTable("", "", terminalCount, 2);
                lblCount.Text = dsTerminal.Tables[0].Rows.Count.ToString();


                if (Request.QueryString["id"] == null)
                {
                    /*显示在gridview中*/
                    string adTerminal = "select TerminalID,TerminalName,TerminalNo,TerminalMac,TerminalIP,TerminalAddress,CreateTime,TerminalState from T_Terminal";
                    DataSet dsAccount = common.GetGeneralTable("", "", adTerminal, 2);
                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;                                       //AspNetPager控件
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
                    pds.DataSource = dsAccount.Tables[0].DefaultView;
                    AspNetPager1.AlwaysShow = true;
                    AspNetPager1.RecordCount = pds.DataSourceCount;
                    gvTerminalManagement.DataSource = pds;
                    gvTerminalManagement.DataBind();
                }
                else    //传值过来的情况
                {
                    /*显示在gridview中*/
                    string adTerminal = "select tt.TerminalID,tt.TERMINALNAME,tt.TerminalState,tt.TERMINALADDRESS,tt.CreateTime,tt.TerminalIP,tt.TerminalMac from T_TerminalType tg,T_Terminal tt where tg.TerminalTypeID='" + Request.QueryString["id"] + "' and tt.TerminalNo='" + Request.QueryString["id"] + "'";
                    DataSet dsAccount = common.GetGeneralTable("", "", adTerminal, 2);
                    PagedDataSource pds = new PagedDataSource();
                    pds.AllowPaging = true;                                       //AspNetPager控件
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
                    pds.DataSource = dsAccount.Tables[0].DefaultView;
                    AspNetPager1.AlwaysShow = true;
                    AspNetPager1.RecordCount = pds.DataSourceCount;
                    gvTerminalManagement.DataSource = pds;
                    gvTerminalManagement.DataBind();
                }
            }
            catch (Exception)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertWarningNet()</script>");
            }
        }

        //搜索
        protected void lnkShowTerminal_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlStatus.Text == "正常")
                {
                    ddlStatus.DataSource = "../images/normal.jpg";
                }
                else
                {
                    ddlStatus.DataSource = "../images/stop.jpg";
                }
                if (txtTerminalName.Text == "")
                {
                    if (ddlStatus.Text == "节目单状态")
                    {
                        string adTerminal = "select TerminalID,TerminalName,TerminalNo,TerminalMac,TerminalIP,TerminalAddress,CreateTime,TerminalState from T_Terminal";
                        DataSet dsAccount = common.GetGeneralTable("", "", adTerminal, 2);
                        PagedDataSource pds = new PagedDataSource();
                        pds.AllowPaging = true;                                       //AspNetPager控件
                        pds.PageSize = AspNetPager1.PageSize;
                        pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
                        pds.DataSource = dsAccount.Tables[0].DefaultView;
                        AspNetPager1.AlwaysShow = true;
                        AspNetPager1.RecordCount = pds.DataSourceCount;
                        gvTerminalManagement.DataSource = pds;
                        gvTerminalManagement.DataBind();
                    }
                    else
                    {
                        string adTerminal = "select TerminalID,TerminalName,TerminalNo,TerminalMac,TerminalIP,TerminalAddress,CreateTime,TerminalState from T_Terminal where TerminalState='" + ddlStatus.DataSource + "'";
                        DataSet dsAccount = common.GetGeneralTable("", "", adTerminal, 2);
                        PagedDataSource pds = new PagedDataSource();
                        pds.AllowPaging = true;                                       //AspNetPager控件
                        pds.PageSize = AspNetPager1.PageSize;
                        pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
                        pds.DataSource = dsAccount.Tables[0].DefaultView;
                        AspNetPager1.AlwaysShow = true;
                        AspNetPager1.RecordCount = pds.DataSourceCount;
                        gvTerminalManagement.DataSource = pds;
                        gvTerminalManagement.DataBind();
                    }
                }
                else
                {
                    if (ddlStatus.Text == "节目单状态")
                    {
                        string adTerminal = "select TerminalID,TerminalName,TerminalNo,TerminalMac,TerminalIP,TerminalAddress,CreateTime,TerminalState from T_Terminal where TerminalName like '%" + txtTerminalName.Text + "%'";
                        DataSet dsAccount = common.GetGeneralTable("", "", adTerminal, 2);
                        PagedDataSource pds = new PagedDataSource();
                        pds.AllowPaging = true;                                       //AspNetPager控件
                        pds.PageSize = AspNetPager1.PageSize;
                        pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
                        pds.DataSource = dsAccount.Tables[0].DefaultView;
                        AspNetPager1.AlwaysShow = true;
                        AspNetPager1.RecordCount = pds.DataSourceCount;
                        gvTerminalManagement.DataSource = pds;
                        gvTerminalManagement.DataBind();
                    }
                    else
                    {
                        string adTerminal = "select TerminalID,TerminalName,TerminalNo,TerminalMac,TerminalIP,TerminalAddress,CreateTime,TerminalState from T_Terminal where TERMINALNAME like '%" + txtTerminalName.Text + "%' and TerminalState='" + ddlStatus.DataSource + "'";
                        DataSet dsAccount = common.GetGeneralTable("", "", adTerminal, 2);
                        PagedDataSource pds = new PagedDataSource();
                        pds.AllowPaging = true;                                       //AspNetPager控件
                        pds.PageSize = AspNetPager1.PageSize;
                        pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
                        pds.DataSource = dsAccount.Tables[0].DefaultView;
                        AspNetPager1.AlwaysShow = true;
                        AspNetPager1.RecordCount = pds.DataSourceCount;
                        gvTerminalManagement.DataSource = pds;
                        gvTerminalManagement.DataBind();
                    }
                }
            }
            catch (Exception)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertWarningNet()</script>");
            }
        }

        //分页改变
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            initTerminal();
        }

        //删除
        protected void gvTerminalManagement_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int deleteid = int.Parse(gvTerminalManagement.Rows[e.RowIndex].Cells[0].Text.ToString());
                string connectString = BaseConfig.DataConnectString();
                SqlConnection myConnection = new SqlConnection(connectString);
                SqlCommand myCommand = myConnection.CreateCommand();
                myCommand.CommandText = "Delete from T_Terminal where TerminalID='" + deleteid + "'";
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertDeleteSuccess()</script>");
                initTerminal();
            }
            catch (Exception)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertWarningNet()</script>");
            }
        }

        //修改
        protected void gvTerminalManagement_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int updateid = int.Parse(gvTerminalManagement.Rows[e.RowIndex].Cells[0].Text.ToString());
                Response.Redirect("updateTerminal.aspx?id='" + updateid + "'");
            }
            catch (Exception)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertWarningNet()</script>");
            }
        }

        //添加
        protected void IbtnAdd_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("AddTerminal.aspx");
        }

        //颜色变化
        protected void gvTerminalManagement_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string tempFlowId = DataBinder.Eval(e.Row.DataItem, "TerminalState").ToString();
                if (tempFlowId == "../images/stop.jpg")
                    e.Row.Cells[6].Attributes.Add("style", "color:Red");
                e.Row.Cells[6].Attributes.Add("bordercolor", "black");
            }
        }
    }
}
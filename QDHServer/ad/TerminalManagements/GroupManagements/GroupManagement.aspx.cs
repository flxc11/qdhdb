using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QDHServer.AppCode;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace QDHServer.ad.TerminalManagements.GroupManagements
{
    public partial class GroupManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initGroupManagement();
            }
        }

        Common common = new Common();
        //初始化分组信息
        private void initGroupManagement()
        {
            try
            {
                /*显示在gridview中*/
                string adTerminal = "select * from T_TerminalType";
                DataSet dsAccount = common.GetGeneralTable("", "", adTerminal, 2);
                PagedDataSource pds = new PagedDataSource();
                pds.AllowPaging = true;                                       //AspNetPager控件
                pds.PageSize = AspNetPager1.PageSize;
                pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
                pds.DataSource = dsAccount.Tables[0].DefaultView;
                AspNetPager1.AlwaysShow = true;
                AspNetPager1.RecordCount = pds.DataSourceCount;
                gvGroupManagement.DataSource = pds;
                gvGroupManagement.DataBind();
            }
            catch (Exception)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertWarningNet()</script>");
            }
        }

        //分页改变
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            initGroupManagement();
        }

        //删除
        protected void gvGroupManagement_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int deleteid = int.Parse(gvGroupManagement.Rows[e.RowIndex].Cells[0].Text.ToString());
                string connectString = ConfigurationManager.AppSettings["DataConnectString"];
                SqlConnection myConnection = new SqlConnection(connectString);
                SqlCommand myCommand = myConnection.CreateCommand();
                myCommand.CommandText = "Delete from T_TerminalType where TerminalTypeID='" + deleteid + "'";
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertDeleteSuccess()</script>");
                initGroupManagement();
            }
            catch (Exception)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertWarningNet()</script>");
            }
        }

        //修改
        protected void gvGroupManagement_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int updateid = int.Parse(gvGroupManagement.Rows[e.RowIndex].Cells[0].Text.ToString());
                Response.Redirect("UpdateTerminal.aspx?id='" + updateid + "'");
            }
            catch (Exception)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertWarningNet()</script>");
            }
        }

        //魔棒选择
        protected void gvGroupManagement_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //鼠标经过时，行背景色变 
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#00A9FF'");
                //鼠标移出时，行背景色变 
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");
            }
        }

        //添加
        protected void IbtnAdd_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("AddTerminal.aspx");
        }
    }
}
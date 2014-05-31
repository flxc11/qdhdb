using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using QDHServer.AppCode;
using System.Data.SqlClient;
using System.Data;

namespace QDHServer
{
    public partial class RealTime : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initTerminal();
            }
        }

        downFiles dw = new downFiles();
        Common common = new Common();
        string AD_Address_Details = ConfigurationManager.AppSettings["AD_Address_Details"];

        //初始化终端信息
        private void initTerminal()
        {
            string txtSQL = "select * from ADUSERTERMINAL";
            DataSet dsTerminalID = common.GetGeneralTable("", "", txtSQL, 2);
            PagedDataSource pds = new PagedDataSource();
            pds.AllowPaging = true;
            pds.PageSize = AspNetPager1.PageSize;
            pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
            pds.DataSource = dsTerminalID.Tables[0].DefaultView;
            AspNetPager1.RecordCount = pds.DataSourceCount;
            dlTerminal.DataSource = pds;
            dlTerminal.DataBind();
        }
  
        //终端监控查看
        protected void lnkChakan_Click(object sender, EventArgs e)
        {
            ////try
            ////{
            ////    if (txtTerminal.Text == "")
            ////    {
            ////        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertWarningLastPic()</script>");
            ////    }
            ////    else if (txtTerminal.Text != "")
            ////    {
            ////        string txtSQL = "select * from ADUSERTERMINAL  where TerminalID='" + txtTerminal.Text + "'";
            ////        DataSet dsTerminalID = common.GetGeneralTable("", "", txtSQL, 2);
            ////        if (dsTerminalID.Tables[0].Rows.Count > 0)
            ////        {
            ////            try
            ////            {
            ////                string updateStatus = "update ADUSERTERMINAL set CONTROLSTATUS='1' where TERMINALID='" + txtTerminal.Text + "'";
            ////                SqlConnection conn = new SqlConnection(Common.connectionString);
            ////                conn.Open();
            ////                SqlCommand myCommand = conn.CreateCommand();
            ////                myCommand.CommandText = "" + updateStatus + "";
            ////                myCommand.ExecuteNonQuery();
            ////            }
            ////            catch (Exception)
            ////            {
            ////                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertWarningDeletePic()</script>");
            ////            }


            ////            //if (dw.GetUrlError("" + AD_Address_Details + "/" + txtTerminal.Text + ".jpg" + "").Equals(200))
            ////            //{
            ////            //    // lnkChakan.Attributes.Add("onclick", "javascript:window.showModalDialog(\"RealTimeDetails.aspx?terminal=" + txtTerminal.Text + "\",null,\"dialogWidth=530px,dialogHeight=530px\");void 0;");
            ////            //    Response.Redirect("RealTimeDetails.aspx?terminal=" + txtTerminal.Text + "",false);
                           
            ////            //   // this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>waiting()</script>");
            ////            //}
            ////            //else
            ////            //{
            ////            //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>waiting()</script>");
            ////            //}
            ////        }
            ////        else
            ////        {
            ////            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertError()</script>");
            ////        }
            ////    }
            ////}
            ////catch (Exception)
            ////{
            ////    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertWarningDeletePic()</script>");
            ////}
        }

        //分页 页数改变
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            initTerminal();
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
    }
}
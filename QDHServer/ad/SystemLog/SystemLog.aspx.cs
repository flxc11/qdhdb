using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace QDHServer.ad.SystemLog
{
    public partial class SystemLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initSystemLog();
            }
        }

        //初始化加载系统日志
        private void initSystemLog()
        {
            try
            {
                string strDirectory = HttpContext.Current.Server.MapPath("~") + "\\logs\\广告\\";
                DataTable dt = new DataTable();
                dt.Columns.Add("终端名称");
                dt.Columns.Add("类型");
                dt.Columns.Add("状态");
                dt.Columns.Add("完成时间");
                string date = DateTime.Now.ToString("yyyyMMdd");
                FileStream fs = new FileStream(strDirectory + "" + date + ".txt", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8);
                string strLine = sr.ReadLine();
                while (strLine != null)
                {
                    lblLog.Visible = false;
                    dvLog.Visible = true;
                    string[] strArray = new string[3];
                    strArray = strLine.Split(',');
                    DataRow dr = dt.NewRow();
                    dr[0] = strArray[0];
                    dr[1] = strArray[1];
                    dr[2] = strArray[2];
                    dr[3] = strArray[3];
                    dt.Rows.Add(dr);
                    strLine = sr.ReadLine();
                }
                sr.Close();
                fs.Close();

                DataView dv = dt.DefaultView;
                dvLog.DataSource = dv;
                dvLog.DataBind();
            }
            catch (Exception)
            {
                lblLog.Visible = true;
                lblLog.Text = "暂无日志";
                dvLog.Visible = false;
            }
        }

        //搜索
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string strDirectory = HttpContext.Current.Server.MapPath("~") + "logs\\广告\\";
            DataTable dt = new DataTable();
            dt.Columns.Add("终端名称");
            dt.Columns.Add("类型");
            dt.Columns.Add("状态");
            dt.Columns.Add("用户名");
            dt.Columns.Add("完成时间");
            string date = txtTime.Value.ToString();
            try
            {
                FileStream fs = new FileStream(strDirectory + "" + date + ".txt", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8);
                string strLine = sr.ReadLine();
                while (strLine != null)
                {
                    lblLog.Visible = false;
                    dvLog.Visible = true;
                    string[] strArray = new string[4];
                    strArray = strLine.Split(',');
                    DataRow dr = dt.NewRow();
                    dr[0] = strArray[0];
                    dr[1] = strArray[1];
                    dr[2] = strArray[2];
                    dr[3] = strArray[3];
                    dr[4] = strArray[4];
                    dt.Rows.Add(dr);
                    strLine = sr.ReadLine();
                }
                sr.Close();
                fs.Close();

                DataView dv = dt.DefaultView;
                if (dv.Count == 0)
                {
                    lblLog.Visible = true;
                    lblLog.Text = "暂无日志";
                    dvLog.Visible = false;
                }
                else
                {
                    dvLog.DataSource = dv;
                    dvLog.DataBind();
                }
            }
            catch (Exception)
            {
                lblLog.Visible = true;
                lblLog.Text = "暂无日志";
                dvLog.Visible = false;
            }
        }

        //返回
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ad/PicShow.aspx");
        }
    }
}
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QDHServer.AppCode;
using System.Text;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;

namespace QDHServer
{
    public partial class RealTimeDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            initDetailShow();
        }
        downFiles dw = new downFiles();
        Common common = new Common();

        [DllImport("winmm.dll")]
        public static extern int mciSendString(string m_strCmd, string m_strReceive, int m_v1, int m_v2);

        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        static extern Int32 GetShortPathName(String path, StringBuilder shortPath, Int32 shortPathLength);
        string connectString = ConfigurationManager.AppSettings["DataConnectString"];
        string AD_Address_Details = ConfigurationManager.AppSettings["AD_Address_Details"];
        /// <summary>
        /// 使用mciSendString播放音乐
        /// </summary>
        /// <param name="name">文件名</param>
        /// <param name="command">命令</param>
        private static void mciMusic(string name, string command)
        {
            StringBuilder shortpath = new StringBuilder();
            int result = GetShortPathName(name, shortpath, shortpath.Capacity);
            name = shortpath.ToString();
            string buf = string.Empty;
            mciSendString(command + " " + name, buf, buf.Length, 0); //播放 
        }

        //显示视频详细信息
        private void initDetailShow()
        {
            try
            {
                string terminals = Request.QueryString["terminal"].ToString();
                string txtSQL = "select * from ADUSERTERMINAL  where TerminalID='" + terminals + "'";
                DataSet dsTerminalID = common.GetGeneralTable("", "", txtSQL, 2);
                if (dsTerminalID.Tables[0].Rows.Count > 0)
                {
                    try
                    {
                        string updateStatus = "update ADUSERTERMINAL set CONTROLSTATUS='1' where TERMINALID='" + terminals + "'";
                        SqlConnection conn = new SqlConnection(Common.connectionString);
                        conn.Open();
                        SqlCommand myCommand = conn.CreateCommand();
                        myCommand.CommandText = "" + updateStatus + "";
                        myCommand.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertWarningDeletePic()</script>");
                    }
                }

                if (dw.GetUrlError("" + AD_Address_Details + "/" + terminals + ".jpg" + "").Equals(200))
                {
                    this.pic_content.InnerHtml = PlayMedia.Play(@"" + AD_Address_Details + "/" + terminals + ".jpg", 510, 510);
                }
                else
                {
                  //  this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>waiting()</script>");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请过30秒之后再试！！');location.href='RealTime.aspx'</script>");
                }
            }
            catch (Exception)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertWarningDeletePic()</script>");
            }
        }
    }
}
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QDHServer.AppCode;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Text;

namespace QDHServer.ad.TermialLook
{
    public partial class DetailShowVideo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            initDetailShow();
        }
        downFiles file = new downFiles();
        public static string AD_Address_Images = ConfigurationManager.AppSettings["AD_Address_Images"];
        [DllImport("winmm.dll")]
        public static extern int mciSendString(string m_strCmd, string m_strReceive, int m_v1, int m_v2);

        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        static extern Int32 GetShortPathName(String path, StringBuilder shortPath, Int32 shortPathLength);

        string connectString = ConfigurationManager.AppSettings["DataConnectString"];

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
                string videoName = Request.QueryString["videoName"].ToString();
                //   System.IO.Directory.CreateDirectory(BaseConfig.Local_Dir());  //创建本地文件夹 用来存放下载文件
                //  file.downFile(BaseConfig.AD_Address_Images() + "/" + videoName, BaseConfig.Local_Dir() + "/" + videoName);
                this.video_content.InnerHtml = PlayMedia.Play(@"" + AD_Address_Images + "/" + videoName + "", 510, 510);
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('网络异常，请稍后再试！')</script>");
            }
        }
    }
}
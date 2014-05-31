using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.Services;
using QDHServer.AppCode;

namespace QDHServer
{
    /// <summary>
    /// ytSelfHelpSvr 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class ytSelfHelpSvr : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        #region 广告  sgr
        /// <summary>
        /// 终端号读出数据库Status值  (监控)
        /// </summary>
        /// <param name="TerminalId">终端编号</param>
        [WebMethod(Description = "获取数据库Status值")]
        public string controlStatus(string TerminalId)
        {
            SQLHelper help = new SQLHelper();
            string txt = help.StatusInfo(TerminalId);
            return txt;
        }

        /// <summary>
        ///  查询对应终端上的更新时间
        /// </summary>
        /// <param name="TerminalId">终端编号</param>
        [WebMethod(Description = "获取对应终端的更新时间")]
        public string timeInfo(string TerminalId)
        {
            SQLHelper help = new SQLHelper();
            string time = help.timeInfo(TerminalId);
            return time;
        }

        /// <summary>
        /// 读取最大时间的图片  用于替换ini文件中的图片名称
        /// </summary>
        /// <param name="TerminalId">终端编号</param>
        [WebMethod(Description = "读取终端上更换的图片")]
        public string picInfo(string TerminalId)
        {
            SQLHelper help = new SQLHelper();
            string pic = help.picInfo(TerminalId);
            return pic;
        }

        /// <summary>
        /// 读取数据库上的添加图片(添加)
        /// </summary>
        /// <param name="TerminalId">终端编号</param>
        /// <param name="DownloadFilePath">下载文件路径</param>
        [WebMethod(Description = "读取数据库上的添加图片")]
        public string getInsertPicName(string TerminalId)
        {
            SQLHelper help = new SQLHelper();
            string pic = help.getInsertPicName(TerminalId);
            return pic;
        }

        /// <summary>
        /// 读取数据库上的添加视频(添加)
        /// </summary>
        /// <param name="TerminalId">终端编号</param>
        /// <param name="DownloadFilePath">下载文件路径</param>
        [WebMethod(Description = "读取数据库上的添加视频")]
        public string getInsertVideoName(string TerminalId)
        {
            SQLHelper help = new SQLHelper();
            string video = help.getInsertVideoName(TerminalId);
            return video;
        }

        /// <summary>
        /// 删除添加终端号广告
        /// </summary>
        /// <param name="TerminalId">终端号</param>
        [WebMethod(Description = "删除添加终端号广告")]
        public void getDelInsert(string TerminalId)
        {
            SQLHelper help = new SQLHelper();
            help.getDelInsert(TerminalId);
        }

        /// <summary>
        /// 删除终端号图片
        /// </summary>
        /// <param name="TerminalId">终端号</param>
        /// <param name="DownloadFilePath">下载文件路径</param>
        [WebMethod(Description = "删除终端号图片")]
        public string getDelPic(string TerminalId)
        {
            SQLHelper help = new SQLHelper();
            string pic = help.getDelPic(TerminalId);
            return pic;
        }

        /// <summary>
        /// 删除终端号视频
        /// </summary>
        /// <param name="TerminalId">终端号</param>
        /// <param name="DownloadFilePath">下载文件路径</param>
        [WebMethod(Description = "删除终端号视频")]
        public string getDelVideo(string TerminalId)
        {
            SQLHelper help = new SQLHelper();
            string pic = help.getDelVideo(TerminalId);
            return pic;
        }

        /// <summary>
        /// 删除终端号相关信息
        /// </summary>
        /// <param name="TerminalId">终端号</param>
        [WebMethod(Description = "删除终端号相关信息")]
        public void getDelete(string TerminalId)
        {
            SQLHelper help = new SQLHelper();
            help.getDelete(TerminalId);
        }

        /// <summary>
        /// 得到要覆盖的图片
        /// </summary>
        /// <param name="TerminalId">终端号</param>
        [WebMethod(Description = "得到要覆盖的图片")]
        public string getAnnouncePic(string TerminalId)
        {
            SQLHelper help = new SQLHelper();
            return help.getAnnouncePic(TerminalId);
        }

        /// <summary>
        /// 得到要覆盖的视频
        /// </summary>
        /// <param name="TerminalId">终端号</param>
        [WebMethod(Description = "得到要覆盖的视频")]
        public string getAnnounceVideo(string TerminalId)
        {
            SQLHelper help = new SQLHelper();
            return help.getAnnounceVideo(TerminalId);
        }

        /// <summary>
        /// 更新  监控状态(把1改成0)
        /// </summary>
        /// <param name="TerminalId">终端号</param>
        [WebMethod(Description = "更新  监控状态(把1改成0)")]
        public void updateStatus(string TerminalId)
        {
            SQLHelper help = new SQLHelper();
            help.updateStatus(TerminalId);
        }

        /// <summary>
        /// 终端监控 获取状态(重启)
        /// </summary>
        /// <param name="TerminalId">终端编号</param>
        [WebMethod(Description = "重启时间状态")]
        public string reStartStatus(string TerminalId)
        {
            SQLHelper help = new SQLHelper();
            return help.ReStartInfo(TerminalId);
        }

        /// <summary>
        /// 重启时间
        /// </summary>
        /// <param name="TerminalId">终端编号</param>
        [WebMethod(Description = "重启时间")]
        public void RestartTime(string TerminalId)
        {
            SQLHelper help = new SQLHelper();
            help.RestartTime(TerminalId);
        }


        /// <summary>
        /// 关机时间(得到关机状态  1为关机  0为不关机)
        /// </summary>
        /// <param name="TerminalId">终端编号</param>
        [WebMethod(Description = "关机时间状态")]
        public string closeTimeStatus(string TerminalId)
        {
            SQLHelper help = new SQLHelper();
            return help.closeTimeStatus(TerminalId);
        }

        /// <summary>
        /// 关机时间
        /// </summary>
        /// <param name="TerminalId">终端编号</param>
        /// <returns></returns>
        [WebMethod(Description = "关机时间")]
        public string closeTime(string TerminalId)
        {
            SQLHelper help = new SQLHelper();
            return help.closeTime(TerminalId);
        }

        #endregion 广告
    }
}

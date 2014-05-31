using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using QDHServer.AppCode;
using System.Data;
using System.Data.SqlClient;

namespace QDHServer.AppCode
{
    public class SQLHelper
    {
        #region 广告
        Common common = new Common();
        /// <summary>
        ///  查询对应终端上的更新时间
        /// </summary>
        /// <param name="TerminalId">终端编号</param>
        public string timeInfo(string TerminalId)
        {
            try
            {
                string selectTime = "select top 1 Time from ADTANNOUNCE where terminalID='" + TerminalId + "'  and Status=1";
                DataSet dsTime = common.GetGeneralTable("", "", selectTime, 2);
                return dsTime.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception)
            {
                return "9999";
            }
        }

        /// <summary>
        /// 终端监控 获取状态
        /// </summary>
        /// <param name="TerminalId">终端编号</param>
        public string StatusInfo(string TerminalId)
        {
            try
            {
                string selectTxt = "select CONTROLSTATUS from ADUSERTERMINAL where terminalID='" + TerminalId + "'";
                DataSet dsTxt = common.GetGeneralTable("", "", selectTxt, 2);
                return dsTxt.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception)
            {
                return "9999";
            }
        }

        /// <summary>
        /// 读取最大时间的图片  用于替换ini文件中的图片名称
        /// </summary>
        /// <param name="TerminalId">终端编号</param>
        public string picInfo(string TerminalId)
        {
            try
            {
                string selectPicName = "select PicName,Time from ADTANNOUNCE where TerminalId='" + TerminalId + "' and TXTTYPE='图片' and Status=1";
                DataSet dsPic = common.GetGeneralTable("", "", selectPicName, 2);
                return dsPic.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception)
            {
                return "9999";
            }
        }

        /// <summary>
        /// 读取数据库上的添加图片(添加)
        /// </summary>
        /// <param name="TerminalId">终端编号</param>
        public string getInsertPicName(string TerminalId)
        {
            try
            {
                string pic = "";
                Common common = new Common();
                string picName = "select PicName from adinsert where TERMINALID='" + TerminalId + "' and UpdateStatus='4'";
                SqlConnection conn = new SqlConnection(Common.connectionString);
                SqlCommand cmd = new SqlCommand("" + picName + "", conn);
                conn.Open();
                SqlDataReader odrInsertPic = cmd.ExecuteReader();
                DataSet dsInsert = Common.Query(picName);
                if (dsInsert.Tables[0].Rows.Count > 0)      //添加的情况
                {
                    while (odrInsertPic.Read())
                    {
                        string PicName = odrInsertPic["PicName"].ToString();
                        pic += PicName + (',');
                    }
                    return pic;
                }
                else
                {
                    return "9999";
                }
            }
            catch (Exception)
            {
                return "9999";
            }
        }

        /// <summary>
        /// 读取数据库上的添加视频(添加)
        /// </summary>
        /// <param name="TerminalId">终端编号</param>
        public string getInsertVideoName(string TerminalId)
        {
            try
            {
                string video = "";
                SqlConnection conn = new SqlConnection(Common.connectionString);
                conn.Open();
                string videoName = "select VideoName from adinsert where TERMINALID='" + TerminalId + "' and UpdateStatus='4'";
                SqlCommand cmdVideo = new SqlCommand("" + videoName + "", conn);
                SqlDataReader odrInsertVideo = cmdVideo.ExecuteReader();
                DataSet dsInsert = Common.Query(videoName);
                if (dsInsert.Tables[0].Rows.Count > 0)      //添加的情况
                {
                    while (odrInsertVideo.Read())
                    {
                        string VideoName = odrInsertVideo["VideoName"].ToString();
                        video += VideoName + (',');
                    }
                    return video;
                }
                else
                {
                    return "9999";
                }
            }
            catch (Exception)
            {
                return "9999";
            }
        }

        /// <summary>
        /// 删除添加终端号广告
        /// </summary>
        /// <param name="TerminalId">终端号</param>
        public void getDelInsert(string TerminalId)
        {
            try
            {
                string delInsert = "Delete from adinsert where TERMINALID='" + TerminalId + "'";
                SqlConnection conn = new SqlConnection(Common.connectionString);
                conn.Open();
                SqlCommand myCommand = conn.CreateCommand();
                myCommand.CommandText = "" + delInsert + "";
                myCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 删除终端号图片
        /// </summary>
        /// <param name="TerminalId">终端号</param>
        public string getDelPic(string TerminalId)
        {
            try
            {
                string pic = "";
                SqlConnection conn = new SqlConnection(Common.connectionString);
                conn.Open();
                string picDelName = "select PicName from addelete where TERMINALID='" + TerminalId + "' and UpdateStatus='2'";
                SqlCommand cmdDel = new SqlCommand("" + picDelName + "", conn);
                SqlDataReader odrDelPic = cmdDel.ExecuteReader();
                DataSet dsDel = Common.Query(picDelName);
                if (dsDel.Tables[0].Rows.Count > 0)      //删除的情况
                {
                    while (odrDelPic.Read())
                    {
                        string PicName = odrDelPic["PicName"].ToString();
                        pic += PicName + (',');
                    }
                    return pic;
                }
                else
                {
                    return "9999";
                }
            }
            catch (Exception)
            {
                return "9999";
            }
        }

        /// <summary>
        /// 删除终端号视频
        /// </summary>
        /// <param name="TerminalId">终端号</param>
        /// <param name="DownloadFilePath">下载文件路径</param>
        public string getDelVideo(string TerminalId)
        {
            try
            {
                string video = "";
                string videoName = "select VideoName from addelete where TERMINALID='" + TerminalId + "' and UpdateStatus='2'";
                SqlConnection conn = new SqlConnection(Common.connectionString);
                conn.Open();
                SqlCommand cmdVideo = new SqlCommand("" + videoName + "", conn);
                SqlDataReader odrDelVideo = cmdVideo.ExecuteReader();
                DataSet dsDel = Common.Query(videoName);
                if (dsDel.Tables[0].Rows.Count > 0)      //删除的情况
                {
                    while (odrDelVideo.Read())
                    {
                        string VideoName = odrDelVideo["VideoName"].ToString();
                        video += VideoName + (',');
                    }
                    return video;
                }
                else
                {
                    return "9999";
                }
            }
            catch (Exception)
            {
                return "9999";
            }
        }

        /// <summary>
        /// 删除终端号相关信息
        /// </summary>
        /// <param name="TerminalId">终端号</param>
        public void getDelete(string TerminalId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Common.connectionString);
                conn.Open();
                string delDelete = "Delete from addelete where TERMINALID='" + TerminalId + "'";
                SqlCommand myCommand = conn.CreateCommand();
                myCommand.CommandText = "" + delDelete + "";
                myCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 得到要覆盖的图片
        /// </summary>
        /// <param name="TerminalId">终端号</param>
        public string getAnnouncePic(string TerminalId)
        {
            try
            {
                string picName = "";
                downFiles down = new downFiles();
                string insertMsg = "select * from adinsert where TERMINALID='" + TerminalId + "'";
                DataSet dsInsert = Common.Query(insertMsg);
                string DelMsg = "select * from adDelete where TERMINALID='" + TerminalId + "'";
                DataSet dsDelete = Common.Query(DelMsg);
                if (dsInsert.Tables[0].Rows.Count == 0 && dsDelete.Tables[0].Rows.Count == 0)
                {
                    string picAnnounce = "select PicName from ADTANNOUNCE where TXTTYPE='图片' and TERMINALID='" + TerminalId + "'";
                    SqlConnection conn = new SqlConnection(Common.connectionString);
                    conn.Open();
                    SqlCommand cmdAnnounce = new SqlCommand("" + picAnnounce + "", conn);
                    SqlDataReader ordAnnouncePic = cmdAnnounce.ExecuteReader();
                    DataSet dsAnnouncePic = Common.Query(picAnnounce);
                    if (dsAnnouncePic.Tables[0].Rows.Count > 0)
                    {
                        while (ordAnnouncePic.Read())
                        {
                            string picAnnounceName = ordAnnouncePic["PicName"].ToString();
                            picName += picAnnounceName + (',');
                        }
                        return picName;
                    }
                    return "9999";
                }
                return "9999";
            }
            catch (Exception)
            {
                return "9999";
            }
        }

        /// <summary>
        /// 得到要覆盖的视频
        /// </summary>
        /// <param name="TerminalId">终端号</param>
        public string getAnnounceVideo(string TerminalId)
        {
            try
            {
                string video = "";
                string insertMsg = "select * from adinsert where TERMINALID='" + TerminalId + "'";
                DataSet dsInsert = Common.Query(insertMsg);
                string DelMsg = "select * from adDelete where TERMINALID='" + TerminalId + "'";
                DataSet dsDelete = Common.Query(DelMsg);
                if (dsInsert.Tables[0].Rows.Count == 0 && dsDelete.Tables[0].Rows.Count == 0)
                {
                    string videoAnnounce = "select VideoName from ADTANNOUNCE where TXTTYPE='视频' and TERMINALID='" + TerminalId + "'";
                    SqlConnection conn = new SqlConnection(Common.connectionString);
                    conn.Open();
                    SqlCommand cmdAnnounceVideo = new SqlCommand("" + videoAnnounce + "", conn);
                    SqlDataReader ordAnnounceVideo = cmdAnnounceVideo.ExecuteReader();
                    DataSet dsAnnouncePic = Common.Query(videoAnnounce);
                    if (dsAnnouncePic.Tables[0].Rows.Count > 0)
                    {
                        while (ordAnnounceVideo.Read())
                        {
                            string videoAnnounceName = ordAnnounceVideo["videoName"].ToString();
                            video += videoAnnounceName + (',');
                        }
                        return video;
                    }
                    return "9999";
                }
                return "9999";
            }
            catch (Exception)
            {
                return "9999";
            }
        }

        /// <summary>
        /// 更新  监控状态(把1改成0)
        /// </summary>
        /// <param name="TerminalId">终端号</param>
        public void updateStatus(string TerminalId)
        {
            try
            {
                string updateStatus = "update ADUSERTERMINAL set CONTROLSTATUS='0' where TERMINALID='" + TerminalId + "'";
                SqlConnection conn = new SqlConnection(Common.connectionString);
                conn.Open();
                SqlCommand myCommand = conn.CreateCommand();
                myCommand.CommandText = "" + updateStatus + "";
                myCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 重启时间(得到重启状态  1为重启  0为不重启)
        /// </summary>
        /// <param name="TerminalId">终端编号</param>
        public string ReStartInfo(string TerminalId)
        {
            try
            {
                string selectTxt = "select RESTARTSTATUS from ADUSERTERMINAL where terminalID='" + TerminalId + "'";
                DataSet dsTxt = common.GetGeneralTable("", "", selectTxt, 2);
                return dsTxt.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception)
            {
                return "9999";
            }
        }

        /// <summary>
        /// 重启时间(把状态1改成0)
        /// </summary>
        /// <param name="TerminalId">终端编号</param>
        public void RestartTime(string TerminalId)
        {
            try
            {
                string updateStatus = "update ADUSERTERMINAL set RESTARTSTATUS='0' where TERMINALID='" + TerminalId + "'";
                SqlConnection conn = new SqlConnection(Common.connectionString);
                conn.Open();
                SqlCommand myCommand = conn.CreateCommand();
                myCommand.CommandText = "" + updateStatus + "";
                myCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 关机时间(得到关机状态  1为关机  0为不关机)
        /// </summary>
        /// <param name="TerminalId">终端编号</param>
        public string closeTimeStatus(string TerminalId)
        {
            try
            {
                string selectTxt = "select CLOSESTATUS from ADUSERTERMINAL where terminalID='" + TerminalId + "'";
                DataSet dsTxt = common.GetGeneralTable("", "", selectTxt, 2);
                return dsTxt.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception)
            {
                return "9999";
            }
        }

        /// <summary>
        /// 关机时间
        /// </summary>
        /// <param name="TerminalId">终端编号</param>
        public string closeTime(string TerminalId)
        {
            try
            {
                string closeTime = "select CLOSETIME from ADUSERTERMINAL where TerminalId='" + TerminalId + "'";
                DataSet dsPic = common.GetGeneralTable("", "", closeTime, 2);
                return dsPic.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception)
            {
                return "9999";
            }
        }
        #endregion 广告
    }
}
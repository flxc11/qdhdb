using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QDHServer.AppCode;

namespace QDHServer.ad.TermialLook
{
    public partial class DetailTermial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initDetailPic();
                initDetailVideo();
            }
        }
        Common common = new Common();

        //初始化图片信息
        private void initDetailPic()
        {
            try
            {
                string termialId = Request.QueryString["termialId"].ToString();
                string Detailpic = "select PICNAME from ADTANNOUNCE where TerminalID='" + termialId + "'  and TxtType='图片'";
                DataSet dsPic = common.GetGeneralTable("", "", Detailpic, 2);
                dlPic.DataSource = dsPic;
                dlPic.DataBind();
            }
            catch (Exception)
            {
            }
        }

        //初始化视频信息
        private void initDetailVideo()
        {
            try
            {
                string termialId = Request.QueryString["termialId"].ToString();
                string DetailVideo = "select VideoName from ADTANNOUNCE where TerminalID='" + termialId + "'  and TxtType='视频'";
                DataSet dsVideo = common.GetGeneralTable("", "", DetailVideo, 2);
                dlVideo.DataSource = dsVideo;
                dlVideo.DataBind();
            }
            catch (Exception)
            {

            }
        }

        ////初始化字幕信息
        //private void initDetailWord()
        //{
        //    try
        //    {
        //        string termialId = Request.QueryString["termialId"].ToString();
        //        string DetailVideo = "select TXTName from ADTANNOUNCE where TerminalID='" + termialId + "'";
        //        DataSet dsVideo = common.GetGeneralTable("", "", DetailVideo, 2);
        //        //lblWord.Text = dsVideo.Tables[0].Rows[0][0].ToString();
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //返回
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("TermialLooks.aspx");
        }
    }
}
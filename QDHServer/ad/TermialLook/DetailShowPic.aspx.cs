using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using QDHServer.AppCode;

namespace QDHServer.ad.TermialLook
{
    public partial class DetailShowPic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            initDetailShow();
        }

        string connectString = ConfigurationManager.AppSettings["DataConnectString"];
        string AD_Address_Images = ConfigurationManager.AppSettings["AD_Address_Images"];
        downFiles file = new downFiles();
        //显示图片详细信息
        private void initDetailShow()
        {
            try
            {
                string picName = Request.QueryString["picName"].ToString();
                this.video_content.InnerHtml = PlayMedia.Play(@"" + AD_Address_Images + "/" + picName + "", 510, 510);
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('网络异常，请稍后再试！')</script>");
            }
        }
    }
}
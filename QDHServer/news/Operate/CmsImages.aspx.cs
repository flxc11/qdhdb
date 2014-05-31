using System;
using CNVP.Framework.Helper;
using CNVP.Framework.Utils;

namespace QDHServer.news.Operate
{
    public partial class CmsImages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string PageNo = Request.Params["PageNo"];
                if (string.IsNullOrEmpty(PageNo))
                {
                    PageNo = "1";
                }
                if (!Public.IsNumber(PageNo))
                {
                    MessageUtils.ShowPre("页码参数必须为数字！");
                }
                int PageSize = 10;
                int RecordCount, PageCount;
                string StrWhere = "T_NewsSources Where 1=1 And FileExtension In ('jpg','jpeg','png','gif','bmp')";
                Repeater1.DataSource = DbHelper.ExecutePage("*", StrWhere, "SourceID", "Order By SourceID Desc", Convert.ToInt32(PageNo), PageSize, out RecordCount, out PageCount);
                Repeater1.DataBind();
                LitPager.Text = DbHelper.GetPageNormal(RecordCount, PageCount, PageSize, Convert.ToInt32(PageNo));
            }
        }

        /// <summary>
        /// 插入原图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            string originalImagePath = Request.Params["ImagesURL"];
            Response.Write("<script>top.setTBConfig('retVal','" + originalImagePath + "');top.returnValue ='" + originalImagePath + "';top.removeTB();</script>");
        }
        /// <summary>
        /// 插入缩略图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button2_Click(object sender, EventArgs e)
        {
            string originalImagePath = Request.Params["ImagesURL"];
            string FileExtension = Request.Params["HidFileExtension"];
            string thumbnailPath = originalImagePath.Replace("." + FileExtension, "") + "_" + Rand.Number(4) + "." + FileExtension;
            string width = Request.Params["width"];
            string height = Request.Params["height"];
            string mode = Request.Params["mode"];
            if (string.IsNullOrEmpty(width) || (!Public.IsNumber(width)))
            {
                width = "100";
            }
            if (string.IsNullOrEmpty(height) || (!Public.IsNumber(height)))
            {
                height = "100";
            }
            if (!string.IsNullOrEmpty(originalImagePath))
            {
                //CNVP.Framework.Utils.ImageUtils.MakeThumbnail(Server.MapPath(originalImagePath), Server.MapPath(thumbnailPath), Convert.ToInt32(width), Convert.ToInt32(height), mode);
            }
            Response.Write("<script>top.setTBConfig('retVal','" + thumbnailPath + "');top.returnValue ='" + thumbnailPath + "';top.removeTB();</script>");
        }
        public string GetFileName(string FileName)
        {
            if (FileName.Length > 20)
            {
                FileName = FileName.Substring(0, 20) + "...";
            }
            return FileName;
        }
    }
}
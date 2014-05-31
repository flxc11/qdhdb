using System ;
using System.Globalization ;
using System.Xml ;
using System.Web ;
using System.Web.UI.WebControls;
using CNVP.Framework.Utils;
using CNVP.Framework.Helper;

namespace FredCK.FCKeditorV2.FileBrowser
{
	public class Browser : FileWorkerBase
	{
		protected override void OnLoad(EventArgs e)
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            Response.AddHeader("pragma", "no-cache");
            Response.AddHeader("cache-control", "");
            Response.CacheControl = "no-cache";

            this.Config.LoadConfig();
            if (!Config.Enabled)
            {
                this.SendFileUploadResponse(1, true, "", "", "登录状态已丢失，请先登录后台。");
                return;
            }
            string sResourceType = Request.QueryString["Type"];
            if (sResourceType == null)
            {
                this.SendFileUploadResponse(1, true, "", "", "系统不支持浏览该类型的文件。");
                return;
            }
            //绑定数据源
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
            string StrWhere = "T_NewsSources Where 1=1";
            string _Query = string.Empty;
            foreach (string a in Config.TypeConfig[sResourceType].AllowedExtensions)
            {
                _Query += "'" + a + "',";
            }
            if (!string.IsNullOrEmpty(_Query))
            {
                StrWhere += string.Format(" And FileExtension In ({0})",_Query.Substring(0,_Query.Length-1));
            }
            Repeater Repeater1 =(Repeater)FindControl("Repeater1");
            Literal LitPager = (Literal)FindControl("LitPager");
            Repeater1.DataSource = DbHelper.ExecutePage("*", StrWhere, "SourceID", "Order By SourceID Desc", Convert.ToInt32(PageNo), PageSize, out RecordCount, out PageCount);
            Repeater1.DataBind();
            LitPager.Text = DbHelper.GetPageNormal(RecordCount, PageCount, PageSize, Convert.ToInt32(PageNo));
            Panel Panel1 = (Panel)FindControl("Panel1");
            if (RecordCount > 0) { Panel1.Visible = true; }
		}
	}
}

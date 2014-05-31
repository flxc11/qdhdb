using System;
using CNVP.Data;
using CNVP.Framework.Utils;
using CNVP.Model;

namespace QDHServer.news.NewsInfo
{
    public partial class Ajax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Action = Request.Params["Action"];
                switch (Action)
                {
                    case "ISAudit":
                        ISAudit();
                        break;
                    default: DeleteOne();
                        break;
                }
            }
        }

        private void ISAudit()
        {
            string _ID = Request.Params["ID"];
            string _ISAudit = Request.Params["flg"];
            NewsInfoData bll = new NewsInfoData();
            NewsInfoModel model = new NewsInfoModel();
            model.NewsState = Convert.ToInt32(_ISAudit);
            model.NewsID = Convert.ToInt32(_ID);
            bll.ISAudit(model);
            Response.Write("{returnval:\"审核操作成功！\"}");
            Response.End();
        }
        private void DeleteOne()
        {
            string ID = Request.Params["ID"];
            foreach (string a in ID.Split(','))
            {
                if ((!string.IsNullOrEmpty(a)) && (Public.IsNumber(a)))
                {
                    NewsInfoData bll = new NewsInfoData();
                    NewsInfoModel model = new NewsInfoModel();
                    model.NewsID = Convert.ToInt32(a);
                    bll.DeleteNewsInfo(model);
                }
            }
            Response.Write("{msgCode:'1',msgStr:'删除成功。',goUrl:'NewsInfoList.aspx'}");
            Response.End();
        }
    }
}
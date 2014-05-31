using System;
using System.Globalization;
using System.Xml;
using System.Web;
using System.Web.UI.WebControls;
using CNVP.Framework.UploadFile;
using CNVP.Framework.Utils;
using CNVP.Data;
using CNVP.Model;
namespace FredCK.FCKeditorV2.FileBrowser
{
	public class BigUploader : FileWorkerBase
	{
		protected override void OnLoad(EventArgs e)
		{
			this.Config.LoadConfig();
			if (!Config.Enabled)
			{
				this.SendFileUploadResponse( 1, true, "", "", "登录状态已丢失，请先登录后台。");
				return;
			}
			string sResourceType = Request.QueryString["Type"];
			if (sResourceType == null)
			{
				this.SendFileUploadResponse(1,true,"","","系统不支持上传该类型的文件。");
				return;
			}

			if (!Config.CheckIsTypeAllowed(sResourceType))
			{
                this.SendFileUploadResponse(1,true,"","","系统不支持上传该类型的文件。");
				return;
			}
		}
        protected void Button1_Click(object sender, EventArgs e)
        {
            string _Folder = DateTime.Now.ToString("yyyyMM") + "/" + DateTime.Now.ToString("yyyyMMddhhmmssfff");
            Upload Upload1 = (Upload)FindControl("Upload1");
            TextBox TxtFileName = (TextBox)FindControl("TxtFileName");
            Upload1.Save();
            string _FilePath = "/" + Upload1.FullPath;
            if (!string.IsNullOrEmpty(Upload1.FullPath))
            {
                string _FileName = TxtFileName.Text.Trim();
                if (string.IsNullOrEmpty(_FileName))
                {
                    //Edit By Apollo/2010-07-02
                    //_FileName = Upload1.FullPath;
                    _FileName = Upload1.FullPath.Substring(Upload1.FullPath.LastIndexOf("/") + 1);
                }
                //写入数据库
                NewsSourceData bll = new CNVP.Data.NewsSourceData();
                NewsSourcesModel model = new NewsSourcesModel();
                model.FileName = _FileName;
                model.FilePath = "/UploadFile/" + _Folder + "/" + _FileName;
                model.FileExtension = Upload1.Ext.Replace(".", "");
                //bll.FileName = _FileName;
                //Edit By Apollo/2010-0-02
                //bll.FilePath = _FilePath;
                //bll.FilePath = Public.GetInstallDir(_FilePath);
                //bll.FileExtension = Upload1.Ext.Replace(".","");
                //JCms1.4.0
                //bll.AdminID = CNVP.Framework.UI.AdminPage.AdminID;
                //model.AdminID = new AdminPage().AdminID;
                model.AdminID = 0;
                model.PostTime = DateTime.Now.ToString();
                bll.AddSource(model);

                Response.Write("<script language=\"javascript\" type=\"text/javascript\">parent.SetUrl('" + _FilePath + "','" + _FileName + "');</script>");
            }
        }
	}
}
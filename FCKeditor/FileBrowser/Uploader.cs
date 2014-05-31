using System ;
using System.Globalization ;
using System.Xml ;
using System.Web ;

namespace FredCK.FCKeditorV2.FileBrowser
{
	public class Uploader : FileWorkerBase
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
			this.FileUpload(sResourceType,"/",true);
		}
	}
}

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
				this.SendFileUploadResponse( 1, true, "", "", "��¼״̬�Ѷ�ʧ�����ȵ�¼��̨��");
				return;
			}
			string sResourceType = Request.QueryString["Type"];
			if (sResourceType == null)
			{
				this.SendFileUploadResponse(1,true,"","","ϵͳ��֧���ϴ������͵��ļ���");
				return;
			}

			if (!Config.CheckIsTypeAllowed(sResourceType))
			{
                this.SendFileUploadResponse(1,true,"","","ϵͳ��֧���ϴ������͵��ļ���");
				return;
			}
			this.FileUpload(sResourceType,"/",true);
		}
	}
}

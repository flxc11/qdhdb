using System;
using System.Text;
using System.Web;

namespace FredCK.FCKeditorV2.FileBrowser
{
	public class Config : System.Web.UI.UserControl
	{
		private const string DEFAULT_USER_FILES_PATH = "/UploadFile/";

		private string sUserFilesDirectory;

		public bool Enabled;
		public string UserFilesPath;
		public string UserFilesAbsolutePath;
		public bool ForceSingleExtension;
        public bool FileRename;
		public string[] AllowedTypes;
		public string[] HtmlExtensions;
		public TypeConfigList TypeConfig;

		public Config()
		{
        }

		private void DefaultSettings() 
		{
			Enabled = false;
			UserFilesPath = "/UploadFile/";
			UserFilesAbsolutePath = "";
			ForceSingleExtension = true;
            FileRename = true;
			AllowedTypes = new string[] {"File","Image","Flash","Video"};
			HtmlExtensions = new string[] {"html","htm","xml","xsd","txt","js"};

            //获取上传相关设置
			TypeConfig = new TypeConfigList((FileWorkerBase)this.Page);

            //上传附件相关设置
            TypeConfig["File"].AllowedExtensions = new string[] { "doc", "docx", "xls", "xlsx", "ppt", "pptx", "rar", "zip", "pdf" };
			TypeConfig["File"].DeniedExtensions = new string[] { };
			TypeConfig["File"].FilesPath = "%UserFilesPath%File/";
			TypeConfig["File"].FilesAbsolutePath = ( UserFilesAbsolutePath == "" ? "" : "%UserFilesAbsolutePath%File/" );
			TypeConfig["File"].QuickUploadPath = "%UserFilesPath%";
			TypeConfig["File"].QuickUploadAbsolutePath = "%UserFilesAbsolutePath%";

            //上传图片相关设置
			TypeConfig["Image"].AllowedExtensions = new string[] {"bmp","gif","jpeg","jpg","png"};
			TypeConfig["Image"].DeniedExtensions = new string[] {};
			TypeConfig["Image"].FilesPath = "%UserFilesPath%Image/";
			TypeConfig["Image"].FilesAbsolutePath = (UserFilesAbsolutePath ==""?"":"%UserFilesAbsolutePath%Image/");
			TypeConfig["Image"].QuickUploadPath = "%UserFilesPath%";
			TypeConfig["Image"].QuickUploadAbsolutePath = "%UserFilesAbsolutePath%";

            //上传动画相关设置
			TypeConfig["Flash"].AllowedExtensions = new string[] {"swf"};
			TypeConfig["Flash"].DeniedExtensions = new string[] {};
			TypeConfig["Flash"].FilesPath = "%UserFilesPath%Flash/";
			TypeConfig["Flash"].FilesAbsolutePath = ( UserFilesAbsolutePath==""?"":"%UserFilesAbsolutePath%Flash/");
			TypeConfig["Flash"].QuickUploadPath = "%UserFilesPath%";
			TypeConfig["Flash"].QuickUploadAbsolutePath = "%UserFilesAbsolutePath%";

            //上传视频相关设置
            TypeConfig["Video"].AllowedExtensions = new string[] { "flv" };
			TypeConfig["Video"].DeniedExtensions = new string[] {};
			TypeConfig["Video"].FilesPath = "%UserFilesPath%Video/";
            TypeConfig["Video"].FilesAbsolutePath = (UserFilesAbsolutePath == "" ? "" : "%UserFilesAbsolutePath%Video/");
			TypeConfig["Video"].QuickUploadPath = "%UserFilesPath%";
			TypeConfig["Video"].QuickUploadAbsolutePath = "%UserFilesAbsolutePath%";
		}

		internal void LoadConfig()
		{
			DefaultSettings();
			SetConfig();
			string userFilesPath = Session["FCKeditor:UserFilesPath"] as string;
			if (userFilesPath == null || userFilesPath.Length == 0)
				userFilesPath = Application[ "FCKeditor:UserFilesPath"] as string;
			if (userFilesPath == null || userFilesPath.Length == 0)
				userFilesPath = System.Configuration.ConfigurationSettings.AppSettings[ "FCKeditor:UserFilesPath" ];
			if (userFilesPath == null || userFilesPath.Length == 0)
				userFilesPath = this.UserFilesPath;
			if (userFilesPath == null || userFilesPath.Length == 0)
				userFilesPath = DEFAULT_USER_FILES_PATH;
			if (!userFilesPath.EndsWith("/"))
				userFilesPath += "/";
			userFilesPath = this.ResolveUrl(userFilesPath);
			this.UserFilesPath = userFilesPath;
		}

		/// <summary>
		/// The absolution path (server side) of the user files directory. It 
		/// is based on the <see cref="FileWorkerBase.UserFilesPath"/>.
		/// </summary>
		internal string UserFilesDirectory
		{
			get
			{
				if ( sUserFilesDirectory == null )
				{
					if ( this.UserFilesAbsolutePath.Length > 0 )
					{
						sUserFilesDirectory = this.UserFilesAbsolutePath;
						sUserFilesDirectory = sUserFilesDirectory.TrimEnd( '\\', '/' ) + '/';
					}
					else
					{
						// Get the local (server) directory path translation.
						sUserFilesDirectory = Server.MapPath( this.UserFilesPath );
					}
				}
				return sUserFilesDirectory;
			}
		}

		public virtual void SetConfig()
		{ }

		internal bool CheckIsTypeAllowed( string typeName )
		{
			return ( System.Array.IndexOf( this.AllowedTypes, typeName ) >= 0 );
		}

		internal bool CheckIsNonHtmlExtension( string extension )
		{
			return ( this.HtmlExtensions.Length == 0 || !Util.ArrayContains( this.HtmlExtensions, extension, System.Collections.CaseInsensitiveComparer.DefaultInvariant ) );
		}
	}
}

using System;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using CNVP.Config;
using CNVP.Framework.Utils;
using CNVP.Data;
using CNVP.Model;

namespace FredCK.FCKeditorV2.FileBrowser
{
	public abstract class FileWorkerBase : System.Web.UI.Page
	{
		public Config Config;
		protected void FileUpload(string resourceType, string currentFolder, bool isQuickUpload)
		{
			HttpPostedFile oFile = Request.Files["NewFile"];

            string sFileName = "";
            //判断是否存在上传文件
			if (oFile == null)
			{
				this.SendFileUploadResponse(202,isQuickUpload);
				return;
			}
            //返回服务器端保存路径
			string sServerDir = this.ServerMapFolder(resourceType,currentFolder,isQuickUpload);

            //上传的文件名
            sFileName = System.IO.Path.GetFileName(oFile.FileName);
            //文件名安全过滤
			sFileName = this.SanitizeFileName(sFileName);
            //获取文件后缀
			string sExtension = System.IO.Path.GetExtension(oFile.FileName);
			sExtension = sExtension.TrimStart('.');

            //文件后缀安全检查
			if (!this.Config.TypeConfig[resourceType].CheckIsAllowedExtension(sExtension))
			{
                this.SendFileUploadResponse(202,isQuickUpload);
				return;
			}
			if (this.Config.CheckIsNonHtmlExtension(sExtension) && !this.CheckNonHtmlFile(oFile))
			{
                this.SendFileUploadResponse(202,isQuickUpload);
				return;
			}
            string _FileName = Request.Form["txtFileName"];
            if (string.IsNullOrEmpty(_FileName))
            {
                _FileName = sFileName.Replace("." + sExtension, "");
            }

            //创建文件夹
            string _FloderName = DateTime.Now.ToString("yyyyMM");
            if (!Directory.Exists(sServerDir + _FloderName))
            {
                Directory.CreateDirectory(sServerDir + _FloderName);
            }

            //保存文件
            if (this.Config.FileRename)
            {
                sFileName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + "." + sExtension.ToLower();
            }
            else
            {
                int iCounter = 0;
                sFileName = _FileName + "." + sExtension.ToLower();

                while (true)
                {
                    string sTempPath = System.IO.Path.Combine(sServerDir, _FloderName + "/" + sFileName);
                    if (System.IO.File.Exists(sTempPath))
                    {
                        iCounter++;
                        sFileName = _FileName + "(" + iCounter + ")." + sExtension.ToLower();
                    }
                    else
                    {
                        break;
                    }
                }
            }
            #region "保存原图"
            //保存文件
            sFileName = _FloderName + "/" + sFileName;
            string sFilePath = System.IO.Path.Combine(sServerDir, sFileName);
            oFile.SaveAs(sFilePath);
            //返回路径
            TypeConfig typeConfig = this.Config.TypeConfig[resourceType];
            string sFileUrl = isQuickUpload ? typeConfig.GetQuickUploadPath() : typeConfig.GetFilesPath();
            //sFileUrl += sFileName;
            string sResourceType = Request.QueryString["Type"];
            #endregion
            #region "自动缩略"
            string AutoThumbnail = Request.Form["ChkAutoThumbnail"];
            if (string.IsNullOrEmpty(AutoThumbnail))
            {
                AutoThumbnail = "0";
            }
            if (!Public.IsNumber(AutoThumbnail))
            {
                AutoThumbnail = UIConfig.AutoThumbnail;
            }

            string originalImagePath = sFileName;
            string thumbnailPath = originalImagePath.Replace("." + sExtension.ToLower(), "") + "_" + Rand.Number(4) + "." + sExtension.ToLower();
            string thumbnailPath1 = originalImagePath.Replace("." + sExtension.ToLower(), "") + "_" + Rand.Number(6) + "." + sExtension.ToLower();
            if (sResourceType == "Image" && AutoThumbnail=="1")
            {
                string width = UIConfig.ThumbnailX;
                string height = UIConfig.ThumbnailY;
                string mode = UIConfig.ThumbnailStyle;
                if (string.IsNullOrEmpty(width) || (!Public.IsNumber(width)))
                {
                    width = "100";
                }
                if (string.IsNullOrEmpty(height) || (!Public.IsNumber(height)))
                {
                    height = "100";
                }
                if (System.IO.File.Exists(System.IO.Path.Combine(sServerDir, originalImagePath)))
                {
                    ImageUtils.MakeThumbnail(System.IO.Path.Combine(sServerDir, originalImagePath), System.IO.Path.Combine(sServerDir, thumbnailPath), Convert.ToInt32(width), Convert.ToInt32(height), mode);
                    //删除原图
                    FileUtils.DeleteFile(System.IO.Path.Combine(sServerDir, originalImagePath));
                    originalImagePath = thumbnailPath;
                }
                //sFileUrl = isQuickUpload ? typeConfig.GetQuickUploadPath() : typeConfig.GetFilesPath();
            }
            #endregion
            #region "自动水印"
            string AutoWatermark = Request.Form["ChkAutoWatermark"];
            if (string.IsNullOrEmpty(AutoWatermark))
            {
                AutoWatermark = "0";
            }
            if (!Public.IsNumber(AutoWatermark))
            {
                AutoWatermark = UIConfig.AutoWatermark;
            }

            if (sResourceType == "Image" && AutoWatermark == "1")
            {
                if (!System.IO.File.Exists(System.IO.Path.Combine(sServerDir, thumbnailPath)))
                {
                    thumbnailPath = originalImagePath;
                }
                ImageUtils.AddImageSignPic(System.IO.Path.Combine(sServerDir, thumbnailPath), System.IO.Path.Combine(sServerDir, thumbnailPath1), Server.MapPath(UIConfig.InstallDir + UIConfig.WatermarkSrc), Convert.ToInt32(UIConfig.WatermarkStatus), 100, 100);
                //删除缩略图
                FileUtils.DeleteFile(System.IO.Path.Combine(sServerDir, thumbnailPath));
                originalImagePath = thumbnailPath1;
            }
            sFileUrl += originalImagePath;
            #endregion
            #region "保存入库"
            string _Folder = DateTime.Now.ToString("yyyyMM") + "/" + DateTime.Now.ToString("yyyyMMddhhmmssfff");
            NewsSourceData bll = new CNVP.Data.NewsSourceData();
            NewsSourcesModel model = new NewsSourcesModel();
            model.FileName = _FileName;
            model.FilePath = "/UploadFile/" + _Folder + "/" + _FileName;
            model.FileExtension = sExtension.ToLower();
            //model.AdminID = new AdminPage().AdminID;
            model.AdminID = 0;
            model.PostTime = DateTime.Now.ToString();
            bll.AddSource(model);

            #endregion
            //输出返回值
            this.SendFileUploadResponse(0, isQuickUpload, sFileUrl, _FileName);
		}

		private void SendFileUploadResponse( int errorNumber, bool isQuickUpload )
		{
            this.SendFileUploadResponse(errorNumber, isQuickUpload, "", "", "");
		}

		private void SendFileUploadResponse( int errorNumber, bool isQuickUpload, string fileUrl, string fileName )
		{
			this.SendFileUploadResponse( errorNumber, isQuickUpload, fileUrl, fileName, "" );
		}

		protected void SendFileUploadResponse( int errorNumber, bool isQuickUpload, string fileUrl, string fileName, string customMsg )
		{
			Response.Clear();
			Response.Write( "<script type=\"text/javascript\">");
            //Response.Write(@"(function(){var d=document.domain;while (true){try{var A=window.top.opener.document.domain;break;}catch(e) {};d=d.replace(/.*?(?:\.|$)/,'');if (d.length==0) break;try{document.domain=d;}catch (e){break;}}})();");
            Response.Write(@"(function(){var d=document.domain;while (true){try{var A=window.top.opener.document.domain;break;}catch(e) {};d=d.replace(/.*?(?:\.|$)/,'');if (d.length==0) break;}})();");
            if (isQuickUpload)
            {
                Response.Write("window.parent.OnUploadCompleted(" + errorNumber + ",'" + fileUrl.Replace("'", "\\'") + "','" + fileName.Replace("'", "\\'") + "','" + customMsg.Replace("'", "\\'") + "') ;");
            }
            else
            {
                Response.Write("window.parent.frames['frmUpload'].OnUploadCompleted(" + errorNumber + ",'" + fileName.Replace("'", "\\'") + "') ;");
            }
			Response.Write("</script>");
			Response.End();
		}

		protected string ServerMapFolder( string resourceType, string folderPath, bool isQuickUpload )
		{
			TypeConfig typeConfig = this.Config.TypeConfig[ resourceType ];

			// Get the resource type directory.
			string sResourceTypePath = isQuickUpload ? typeConfig.GetQuickUploadDirectory() : typeConfig.GetFilesDirectory();

			// Ensure that the directory exists.
			Util.CreateDirectory( sResourceTypePath );

			// Return the resource type directory combined with the required path.
			return System.IO.Path.Combine( sResourceTypePath, folderPath.TrimStart( '/' ) );
		}


		// Do a cleanup of the folder name to avoid possible problems
		protected string SanitizeFolderName( string folderName )
		{
			// Remove . \ / | : ? * " < >
			return Regex.Replace( folderName, @"[.\\/|:?*""<>\p{C}]", "_", RegexOptions.None );
		}
        /// <summary>
        /// 文件名称安全过滤
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
		private string SanitizeFileName(string fileName)
		{
			// Replace dots in the name with underscores (only one dot can be there... security issue).
			if (Config.ForceSingleExtension)
				fileName = Regex.Replace(fileName, @"\.(?![^.]*$)", "_", RegexOptions.None);
			// Remove \ / | : ? * " < >
			return Regex.Replace(fileName, @"[\\/|:?*""<>\p{C}]", "_", RegexOptions.None);
		}

		private bool CheckNonHtmlFile( HttpPostedFile file )
		{
			byte[] buffer = new byte[ 1024 ];
			file.InputStream.Read( buffer, 0, 1024 );

			string firstKB = System.Text.ASCIIEncoding.ASCII.GetString( buffer );

			if ( Regex.IsMatch( firstKB, @"<!DOCTYPE\W*X?HTML", RegexOptions.IgnoreCase | RegexOptions.Singleline ) )
				return false;

			if ( Regex.IsMatch( firstKB, @"<(?:body|head|html|img|pre|script|table|title)", RegexOptions.IgnoreCase | RegexOptions.Singleline ) )
				return false;

			//type = javascript
			if ( Regex.IsMatch( firstKB, @"type\s*=\s*[\'""]?\s*(?:\w*/)?(?:ecma|java)", RegexOptions.IgnoreCase | RegexOptions.Singleline ) )
				return false;

			//href = javascript
			//src = javascript
			//data = javascript
			if ( Regex.IsMatch( firstKB, @"(?:href|src|data)\s*=\s*[\'""]?\s*(?:ecma|java)script:", RegexOptions.IgnoreCase | RegexOptions.Singleline ) )
				return false;

			//url(javascript
			if ( Regex.IsMatch( firstKB, @"url\s*\(\s*[\'""]?\s*(?:ecma|java)script:", RegexOptions.IgnoreCase | RegexOptions.Singleline ) )
				return false;

			return true;
		}
	}
}

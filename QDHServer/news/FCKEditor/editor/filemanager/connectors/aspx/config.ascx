<%@ Control Language="C#" EnableViewState="false" AutoEventWireup="false" Inherits="FredCK.FCKeditorV2.FileBrowser.Config" %>
<script runat="server">
    private bool CheckAuthentication()
    {
        //HttpCookie Cookie = Request.Cookies["JCms_Admin"];
        HttpCookie Cookie = Request.Cookies[CNVP.Config.UIConfig.AdminCookiesName];
        if (Cookie != null)
        {
            return true;
        }
        else
        {
            return false; 
        }
    }
    public override void SetConfig()
    {
	    Enabled = CheckAuthentication();
        //Edit By Apollo/2010-06-30
	    UserFilesPath = "/UploadFile/";
        //UserFilesPath = CNVP.Config.UIConfig.InstallDir + "UploadFile/";
	    UserFilesAbsolutePath = "";
	    ForceSingleExtension = true;
        FileRename = true;
        AllowedTypes = new string[] { "File", "Image", "Flash", "Video" };
	    HtmlExtensions = new string[] { "html", "htm", "xml", "xsd", "txt", "js" };

        //上传文件相关设置
        TypeConfig["File"].AllowedExtensions = new string[] { "doc", "docx", "xls", "xlsx", "ppt", "pptx", "rar", "zip", "pdf"};
        TypeConfig["File"].DeniedExtensions = new string[] { };
        TypeConfig["File"].FilesPath = "%UserFilesPath%File/";
        TypeConfig["File"].FilesAbsolutePath = (UserFilesAbsolutePath == "" ? "" : "%UserFilesAbsolutePath%File/");
        TypeConfig["File"].QuickUploadPath = "%UserFilesPath%";
        TypeConfig["File"].QuickUploadAbsolutePath = (UserFilesAbsolutePath == "" ? "" : "%UserFilesAbsolutePath%");

        //上传图片相关设置
        TypeConfig["Image"].AllowedExtensions = new string[] { "bmp", "gif", "jpeg", "jpg", "png" };
        TypeConfig["Image"].DeniedExtensions = new string[] { };
        TypeConfig["Image"].FilesPath = "%UserFilesPath%Image/";
        TypeConfig["Image"].FilesAbsolutePath = (UserFilesAbsolutePath == "" ? "" : "%UserFilesAbsolutePath%Image/");
        TypeConfig["Image"].QuickUploadPath = "%UserFilesPath%";
        TypeConfig["Image"].QuickUploadAbsolutePath = (UserFilesAbsolutePath == "" ? "" : "%UserFilesAbsolutePath%");

        //上传动画相关设置
        TypeConfig["Flash"].AllowedExtensions = new string[] { "swf" };
        TypeConfig["Flash"].DeniedExtensions = new string[] { };
        TypeConfig["Flash"].FilesPath = "%UserFilesPath%Flash/";
        TypeConfig["Flash"].FilesAbsolutePath = (UserFilesAbsolutePath == "" ? "" : "%UserFilesAbsolutePath%Flash/");
        TypeConfig["Flash"].QuickUploadPath = "%UserFilesPath%";
        TypeConfig["Flash"].QuickUploadAbsolutePath = (UserFilesAbsolutePath == "" ? "" : "%UserFilesAbsolutePath%");

        //上传视频相关设置
        TypeConfig["Video"].AllowedExtensions = new string[] { "flv", "asf", "wmv" };
        TypeConfig["Video"].DeniedExtensions = new string[] { };
        TypeConfig["Video"].FilesPath = "%UserFilesPath%Video/";
        TypeConfig["Video"].FilesAbsolutePath = (UserFilesAbsolutePath == "" ? "" : "%UserFilesAbsolutePath%Video/");
        TypeConfig["Video"].QuickUploadPath = "%UserFilesPath%";
        TypeConfig["Video"].QuickUploadAbsolutePath = (UserFilesAbsolutePath == "" ? "" : "%UserFilesAbsolutePath%");
    }
</script>
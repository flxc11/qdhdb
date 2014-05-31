using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Web;
using System.IO;
using System.Drawing;
using CNVP.Config;
using CNVP.Framework.Utils;
using System.Text;

namespace CNVP.Framework.UploadFile
{
    #region 是否使用
    /// <summary>
    /// 是否使用
    /// </summary>
    public enum Boolean
    {
        Yes,
        No
    }
    #endregion 
	#region 上传控件
	/// <summary>
	/// 上传控件
	/// </summary>
	[
	DefaultProperty("Text"), 
	ValidationProperty("Text"),
    ToolboxData("<{0}:Upload runat=server></{0}:Upload>"),
	ParseChildren(false), 
	Designer("CNVP.Framework.UploadFile.UpDesigner") 
	]
	public class Upload : System.Web.UI.WebControls.WebControl
	{
        /// <summary>
        /// 构造函数
        /// </summary>
        public Upload()
        {
            
        }
        #region 上传类型
        /// <summary>
        /// 获取上传类型
        /// </summary>
        public string UploadType
        {
            get
            {
                if (ViewState["UploadType"] != null)
                {
                    return Convert.ToString(ViewState["UploadType"]);
                }
                else
                {
                    return "Default";
                }
            }
            set
            {
                ViewState["UploadType"] = value;
            }
        }
        #endregion
        #region 上传后缀
        /// <summary>
        /// 获取上传后缀
        /// </summary>
        public string AllowExt
        {
            get
            {

                return "jpg,jpeg,png,bmp,gif";
            }
            set
            {
                ViewState["AllowExt"] = value;
            }
        }
        #endregion
        #region 上传大小
        /// <summary>
        /// 获取上传大小
        /// </summary>
        public int MaxSize
        {
            get
            {

                return Convert.ToInt32(UploadConfig.GetSingleConfig(UploadType).MaxSize);
            }
            set
            {
                ViewState["MaxSize"] = value;
            }
        }
        #endregion
        #region 上传路径

        private string filePath ;
        /// <summary>
        /// 获取保存路径
        /// </summary>
        public string FilePath
        {
            get
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    filePath = "/" + UploadConfig.GetSingleConfig(UploadType).FilePath + "/";
                }
                return filePath;
            }
            set
            {
                if (filePath != value)
                {
                    filePath = value;
                }
                ViewState["FilePath"] = value;                
            }
        }
        #endregion
        #region 文件名称
        /// <summary>
        /// 获取上传文件完整路径
        /// </summary>
        public string FullPath
        {
            get;set;
            //get 
            //{
                //object o = ViewState["FullPath"];
                //if (o != null)
                //{
                //    return (string)o;
                //}
                //return null;
            //}
            //set 
            //{
                //ViewState["FullPath"] = value;
            //}
        }

        private string fileName;
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        /// <summary>
        /// 按照原名字保存(Flase按原名字保存)
        /// </summary>
        public bool FileReName
        {
            get 
            {
                object o = ViewState["FileReName"];
                if (o != null)
                {
                    return (bool)o;
                }
                return true;
            }
            set
            {
                ViewState["FileReName"] = value;
            }
        }
        #endregion
        #region 文件属性
		/// <summary>
		/// 选择文件信息
		/// </summary>
		[ Browsable( false ) ]
		public string Text
		{
			get
			{
				try
				{
					foreach(Control c in this.Controls)
					{
						HtmlInputFile f = (HtmlInputFile)c;
						return f.PostedFile.FileName;
					}
					return null;
				}
				catch
				{
					return null;
				}
			}
			set
			{
				ViewState["Text"] = value;
			}
		}
		int fileSize = 0;
		/// <summary>
		/// 上传文件的大小
		/// </summary>
		[ Browsable( false ) ]
		public int FileSize
		{ 
			get 
			{ 
				return fileSize; 
			}
		}
		/// <summary>
		/// 上传文件的扩展名
		/// </summary>
		string ext = string.Empty;
		[ Browsable( false ) ]
		public string Ext
		{ 
			get 
			{ 
				return ext; 
			}
		} 
		#endregion
		#region 上传进度
		/// <summary>
		/// 指定是否显示进度
		/// </summary>
		[ Category( "进度条" ) ]
		[Description("指定是否显示进度")]
        [DefaultValue(false)]
		public Boolean ShowLoad
		{
			get {
                if (ViewState["ShowLoad"] == null)
                {
                    return Boolean.No;
                }
                else
                    return (Boolean)ViewState["ShowLoad"];
            }
			set {ViewState["ShowLoad"] = value;}
		}
		/// <summary>
		/// 填写提交按钮的ID"
		/// </summary>
		[ Category( "进度条" ) ]
		[Description("填写提交按钮的ID，留空则选择第一个runat=server的submit")]
		public string SubmitID
		{
			get {
                if (ViewState["SubmitID"] != null)
                {
                    return ViewState["SubmitID"].ToString();
                }
                else
                {
                    return null;
                }
				}
			set {ViewState["SubmitID"] = value;}
		}
		/// <summary>
		/// 刷新速度，单位ms
		/// </summary>
		[ Category( "进度条" ) ]
		[Description("刷新速度，单位ms")]
		[ DefaultValue( "" ) ]
		public long Refresh
		{
			get 
			{
				if(ViewState["Refresh"] != null)
				{
					return (long) ViewState["Refresh"];
				}
				else
				{
					return 500;
				}
			}
			set {ViewState["Refresh"] = value;}
		}

		/// <summary>
		/// 进度条信息样式
		/// </summary>
		[ Category( "进度条" ) ]
		[Description("进度条信息样式")]
		[ DefaultValue( "" ) ]
		public string progressInfoStyle
		{
			get 
			{
				if(ViewState["progressInfoStyle"] != null)
				{
					return  ViewState["progressInfoStyle"].ToString();
				}
				else
				{
					return null;
				}
			}
			set {ViewState["progressInfoStyle"] = value;}
		}
		/// <summary>
		/// 进度条样式
		/// </summary>
		[ Category( "进度条" ) ]
		[Description("进度条样式")]
		[ DefaultValue( "" ) ]
		public string progressBar
		{
			get 
			{
				if(ViewState["progressBar"] != null)
				{
					return  ViewState["progressBar"].ToString();
				}
				else
				{
					return null;
				}
			}
			set {ViewState["progressBar"] = value;}
		}
		/// <summary>
		/// 进度样式
		/// </summary>
		[ Category( "进度条" ) ]
		[Description("进度样式")]
		[ DefaultValue( "" ) ]
		public string progress
		{
			get 
			{
				
				if(ViewState["progress"] != null)
				{
					return  ViewState["progress"].ToString();
				}
				else
				{
					return null;
				}
			}
			set {ViewState["progress"] = value;}
		}
		/// <summary>
		/// 文件表单样式
		/// </summary>
		[ Category( "其它样式设置" ) ]
		[Description("文件表单样式")]
		[ DefaultValue( "" ) ]
		public string InputCss
		{
			get 
			{
				if(ViewState["InputCss"] != null)
				{
					return  ViewState["InputCss"].ToString();
				}
				else
				{
					return "CNVPUpload";
				}
			}
			set {ViewState["InputCss"] = value;}
		}
		#endregion
		#region 创建界面
		/// <summary> 
		/// 将此控件呈现给指定的输出参数。
		/// </summary>
		/// <param name="output"> 要写出到的 HTML 编写器 </param>
		protected override void Render(HtmlTextWriter output)
		{
			CreateChildControls();
			base.Render(output);
            StringBuilder Str = new StringBuilder();
            Str.Append("<script type=\"text/javascript\" language=\"javascript\">\r\n");
            Str.Append("function " + UploadType + "CheckFile(upload_field){\r\n");
            Str.Append("var filename = upload_field.value;\r\n");
            Str.Append(string.Format("var rgx=/({0})/i;\r\n",UploadConfig.GetSingleConfig(UploadType).AllowExt));
            Str.Append("var ext=filename.substring(filename.lastIndexOf('.')+1);\r\n");
            Str.Append("if(!rgx.test(ext))\r\n");
            Str.Append("{\r\n");
            Str.Append("alert('系统不支持上传('+ext+')后缀的文件。');\r\n");
            Str.Append("upload_field.outerHTML = upload_field.outerHTML;\r\n");
            Str.Append("}\r\n");
            Str.Append("}\r\n");
            Str.Append("</script>\r\n");
            Str.Append("<style type=\"text/css\">");
            Str.Append(".CNVPUpload{width:348px;height:25px;line-height:25px;border:1px solid #CCCCCC;}");
            Str.Append("</style>");
            output.Write(Str.ToString());
		}
		private void BuildControl()
		{
			HtmlInputFile f = new HtmlInputFile();
			f.ID = UniqueID + "_upFile";
            f.Attributes.Add("class", this.InputCss);
            f.Attributes.Add("onChange", "" + UploadType + "CheckFile(this);");
			Controls.Add(f);
            #region 设置进度条
            if (this.ShowLoad == Boolean.Yes)
            {
                ShowUpload upload = new ShowUpload();
                upload.ID = UniqueID + "_showload";
                if (SubmitID != null) upload.SubmitID = this.SubmitID;
                upload.progress = this.progress;
                upload.progressBar = this.progressBar;
                upload.progressInfoStyle = this.progressInfoStyle;
                upload.IsChild = Boolean.Yes;
                upload.Refresh = this.Refresh;
                Controls.Add(upload);
            }
            #endregion
		}
		protected override void CreateChildControls()
		{
			Controls.Clear();
			ClearChildViewState();
			BuildControl();
		}
		#endregion 
		#region 上传处理
		protected override void OnInit(EventArgs e)
		{
			base.OnInit (e);
		}
		public void Save()
		{
			if(Page.IsPostBack)
			{
                HtmlInputFile f = (HtmlInputFile)this.FindControl(UniqueID + "_upFile");
				if(f.PostedFile.ContentLength >0)
				{
                    string m_filename = f.PostedFile.FileName;
                    m_filename = m_filename.Substring(m_filename.LastIndexOf("\\") + 1);
                    this.ext = m_filename.Substring(m_filename.LastIndexOf("."));
                    if (!CheckType(this.ext))
                    {
                        HttpContext.Current.Response.Write("<script>alert('上传文件格式错误，请重新选择文件。');history.back();</script>");
                        HttpContext.Current.Response.End();
                    }
                    if (!CheckSize(f.PostedFile.ContentLength))
                    {
                        HttpContext.Current.Response.Write("<script>alert('上传文件不能大于" + MaxSize + "KB。');history.back();</script>");
                        HttpContext.Current.Response.End();
                    }
                    string temppath = GetSavePath();// this.GetSavePath();
					string m_fullsavePath = temppath +  this.GetFileName(m_filename);
                    FullPath = m_fullsavePath;
                    m_fullsavePath = System.Web.HttpContext.Current.Server.MapPath(m_fullsavePath);
                    f.PostedFile.SaveAs(m_fullsavePath);
				}
			}
		}
		#endregion
        #region 检测大小
        private bool CheckSize(int _size)
        {
            if (_size / 1024 > MaxSize)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
        #region 检测类型
        private bool CheckType(string _type)
        {
            string[] m_types;
            _type = _type.ToLower();
            if (!string.IsNullOrEmpty(AllowExt))
            {
                AllowExt = AllowExt.ToLower();
                m_types = this.AllowExt.Split('|');
                foreach (string m_type in m_types)
                {
                    if ("." + m_type == _type)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion
        #region 文件夹路径
        private string GetSavePath()
        {
            string m_savepath = FilePath;
            m_savepath += DateTime.Now.Year + DateTime.Now.Month + "/";
            string m_fullsavePath = HttpContext.Current.Request.PhysicalApplicationPath + "/" + m_savepath;
            if (!Directory.Exists(m_fullsavePath))
            {
                Directory.CreateDirectory(m_fullsavePath);
            }
            return m_savepath;
        }
        #endregion
        #region 取得文件名
        private string GetFileName(string _filename)
        {
            string tempName = _filename;
            if (FileReName)
            {
                tempName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + this.ext.ToLower();
            }
            if (!string.IsNullOrEmpty(fileName))
            {
                tempName = fileName;
            }
            else
            {
                fileName = tempName;
            }
            
            return tempName;
        }
        #region 同名不覆盖
        //private string GetFileName(string _filename)
        //{
        //    string _return = _filename;
        //    if (!FileNoReName)
        //    {
        //        _return = DateTime.Now.ToString("yyyyMMddhhmmssfff") + this.ext.ToLower();
        //    }
        //    string _returnTemp = _return;
        //    int iCounter = 0;
        //    while (true)
        //    {
        //        string _fullname = HttpContext.Current.Request.PhysicalApplicationPath + "/" + GetSavePath() + _return;
        //        if (System.IO.File.Exists(_fullname))
        //        {
        //            iCounter++;
        //            _return =
        //                System.IO.Path.GetFileNameWithoutExtension(_returnTemp) +
        //                "(" + iCounter + ")" +
        //                System.IO.Path.GetExtension(_returnTemp);
        //        }
        //        else
        //        {
        //            FullPath = GetSavePath() + _return;
        //            return _return;
        //        }
        //    }
        //}
        #endregion
        #endregion
    }
	#endregion
}
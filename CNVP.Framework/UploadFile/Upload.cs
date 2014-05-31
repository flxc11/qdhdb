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
    #region �Ƿ�ʹ��
    /// <summary>
    /// �Ƿ�ʹ��
    /// </summary>
    public enum Boolean
    {
        Yes,
        No
    }
    #endregion 
	#region �ϴ��ؼ�
	/// <summary>
	/// �ϴ��ؼ�
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
        /// ���캯��
        /// </summary>
        public Upload()
        {
            
        }
        #region �ϴ�����
        /// <summary>
        /// ��ȡ�ϴ�����
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
        #region �ϴ���׺
        /// <summary>
        /// ��ȡ�ϴ���׺
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
        #region �ϴ���С
        /// <summary>
        /// ��ȡ�ϴ���С
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
        #region �ϴ�·��

        private string filePath ;
        /// <summary>
        /// ��ȡ����·��
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
        #region �ļ�����
        /// <summary>
        /// ��ȡ�ϴ��ļ�����·��
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
        /// ����ԭ���ֱ���(Flase��ԭ���ֱ���)
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
        #region �ļ�����
		/// <summary>
		/// ѡ���ļ���Ϣ
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
		/// �ϴ��ļ��Ĵ�С
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
		/// �ϴ��ļ�����չ��
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
		#region �ϴ�����
		/// <summary>
		/// ָ���Ƿ���ʾ����
		/// </summary>
		[ Category( "������" ) ]
		[Description("ָ���Ƿ���ʾ����")]
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
		/// ��д�ύ��ť��ID"
		/// </summary>
		[ Category( "������" ) ]
		[Description("��д�ύ��ť��ID��������ѡ���һ��runat=server��submit")]
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
		/// ˢ���ٶȣ���λms
		/// </summary>
		[ Category( "������" ) ]
		[Description("ˢ���ٶȣ���λms")]
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
		/// ��������Ϣ��ʽ
		/// </summary>
		[ Category( "������" ) ]
		[Description("��������Ϣ��ʽ")]
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
		/// ��������ʽ
		/// </summary>
		[ Category( "������" ) ]
		[Description("��������ʽ")]
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
		/// ������ʽ
		/// </summary>
		[ Category( "������" ) ]
		[Description("������ʽ")]
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
		/// �ļ�����ʽ
		/// </summary>
		[ Category( "������ʽ����" ) ]
		[Description("�ļ�����ʽ")]
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
		#region ��������
		/// <summary> 
		/// ���˿ؼ����ָ�ָ�������������
		/// </summary>
		/// <param name="output"> Ҫд������ HTML ��д�� </param>
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
            Str.Append("alert('ϵͳ��֧���ϴ�('+ext+')��׺���ļ���');\r\n");
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
            #region ���ý�����
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
		#region �ϴ�����
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
                        HttpContext.Current.Response.Write("<script>alert('�ϴ��ļ���ʽ����������ѡ���ļ���');history.back();</script>");
                        HttpContext.Current.Response.End();
                    }
                    if (!CheckSize(f.PostedFile.ContentLength))
                    {
                        HttpContext.Current.Response.Write("<script>alert('�ϴ��ļ����ܴ���" + MaxSize + "KB��');history.back();</script>");
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
        #region ����С
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
        #region �������
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
        #region �ļ���·��
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
        #region ȡ���ļ���
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
        #region ͬ��������
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
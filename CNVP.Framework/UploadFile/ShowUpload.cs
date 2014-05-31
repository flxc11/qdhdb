using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Text;
using CNVP.Config;
using CNVP.Framework.Utils;

namespace CNVP.Framework.UploadFile
{
	/// <summary>
	/// ShowUpload 的摘要说明。
	/// </summary>
	[
	DefaultProperty("GUID"), 
	ValidationProperty("GUID"),
    ToolboxData("<{0}:ShowUpload runat=server></{0}:ShowUpload>"),
	ParseChildren(false),
    Designer("CNVP.Framework.UploadFile.ShowUpDesigner") 
	]
	public class ShowUpload : System.Web.UI.WebControls.WebControl
	{
		private string guid = string.Empty;


		/// <summary>
		/// 初使化
		/// </summary>
		public ShowUpload()
		{
			IsChild = Boolean.No;
		}
	
		[ Browsable( false ) ]
		[Category("进度条设置"), 
			DefaultValue(""), 
		Description("上传进度标识，不建议修改")]
		public string GUID 
		{
			get
			{
				
				return guid;
						
			}

			set
			{
				guid = value;
			}
		}

		/// <summary>
		/// 上传提交Button的ID,如果不填，则自动使用第一个Button值
		/// </summary>
		[Bindable(true)]
		[ Category( "进度条设置" ) ]
		[Description("上传提交Button的ID,如果不填，则自动使用第一个Button值")]
		[ DefaultValue( "" ) ]
		public string SubmitID
		{
			get {
				
				if(ViewState["SubmitID"] != null)
				{
					return  ViewState["SubmitID"].ToString();
				}
				else
				{
					return null;
				}
			}
			set {ViewState["SubmitID"] = value;}
		}

		/// <summary>
		/// 如果与提交Button同一页面则不要填写
		/// </summary>
		[ Category( "进度条设置" ) ]
		[Description("如果与提交Button同一页面则不要填写")]
		[ DefaultValue( "" ) ]
		public Boolean IsChild
		{
			get {return (Boolean) ViewState["IsChild"];}
			set {ViewState["IsChild"] = value;}
		}
		/// <summary>
		/// 刷新速度，单位ms
		/// </summary>
		[ Category( "进度条设置" ) ]
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
		[ Category( "进度条设置" ) ]
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
                    return "font-size:12px;font-family:Verdana;overflow:hidden;width:348px;border:solid 1px #cccccc;height:18px;line-height:18px;text-align:center;position:absolute;";
				}
			}
			set {ViewState["progressInfoStyle"] = value;}
		}

		/// <summary>
		/// 进度条样式
		/// </summary>
		[ Category( "进度条设置" ) ]
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
                    return "width:350px;height:18px;line-height:18px;";
				}
			}
			set {ViewState["progressBar"] = value;}
		}

		/// <summary>
		/// 进度样式
		/// </summary>
		[ Category( "进度条设置" ) ]
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
                    return "overflow:hidden;width:0%;height:17px;background:#FF0000;";
				}
			}
			set {ViewState["progress"] = value;}
		}
		/// <summary> 
		/// 将此控件呈现给指定的输出参数。
		/// </summary>
		/// <param name="output"> 要写出到的 HTML 编写器 </param>
		protected override void Render(HtmlTextWriter output)
		{
			string _uploadingid = HttpContext.Current.Request.QueryString["UploadID"];
			if(this.guid == string.Empty)
			{
				this.guid = Guid.NewGuid().ToString();
			}
			if(_uploadingid == null | _uploadingid == "" | _uploadingid == string.Empty)
			{
				string _url = HttpContext.Current.Request.RawUrl;

				if(_url.IndexOf('?')>=0)
				{

					_url +="&UploadID="+ this.guid;
				}
				else
				{
					_url +="?UploadID="+  this.guid;
				}
				HttpContext.Current.Response.Redirect(_url);
			}
			output.Write(Javascript().Replace("{uploadid}",_uploadingid));
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			int upNum = 0;
			if(this.IsChild == Boolean.Yes)
			{
				upNum =this.Parent.Parent.Controls.Count;
			}
			else
			{
				upNum =this.Parent.Controls.Count;
			}

			for(int i=0;i<upNum;i++)
			{
				Submit_Click(i);
			}
		}
		private void Submit_Click(int _i)
		{
			FindSubmit(_i);
		}
		private void FindSubmit(int _i)
		{
			try
			{
				Button _sb;
				if(this.IsChild == Boolean.Yes)
				{
					 _sb = (Button)this.Parent.Parent.Controls[_i];
				}
				else
				{
					_sb = (Button)this.Parent.Controls[_i];
				}
					if(SubmitID != null)
					{
                        if(_sb.ID == SubmitID)
						{
							AddEvent(_sb);								
						}
					}
					else
					{
						AddEvent(_sb);
					}
			}
			catch{
			}	
		}
		private void AddEvent(Button _b)
		{
            _b.Attributes.Add("onclick", "window.setTimeout('LoadProgressInfo()', 500);");
		}
		private string Javascript()
		{
			string temp = @"
<script>
function getDomDocumentPrefix() {
	if (getDomDocumentPrefix.prefix)
		return getDomDocumentPrefix.prefix;
	
	var prefixes = [""MSXML2"", ""Microsoft"", ""MSXML"", ""MSXML3""];
	var o;
	for (var i = 0; i < prefixes.length; i++) {
		try {

			o = new ActiveXObject(prefixes[i] + "".DomDocument"");
			return getDomDocumentPrefix.prefix = prefixes[i];
		}
		catch (ex) {};
	}
	
	throw new Error(""Could not find an installed XML parser"");
}

function getXmlHttpPrefix() {
	if (getXmlHttpPrefix.prefix)
		return getXmlHttpPrefix.prefix;
	
	var prefixes = [""MSXML2"", ""Microsoft"", ""MSXML"", ""MSXML3""];
	var o;
	for (var i = 0; i < prefixes.length; i++) {
		try {

			o = new ActiveXObject(prefixes[i] + "".XmlHttp"");
			return getXmlHttpPrefix.prefix = prefixes[i];
		}
		catch (ex) {};
	}
	
	throw new Error(""Could not find an installed XMLHttp object"");
}

function XmlHttp() {}

XmlHttp.create = function () {
	try {

		if (window.XMLHttpRequest) {
			var req = new XMLHttpRequest();

			if (req.readyState == null) {
				req.readyState = 1;
				req.addEventListener(""load"", function () {
					req.readyState = 4;
					if (typeof req.onreadystatechange == ""function"")
						req.onreadystatechange();
				}, false);
			}
			
			return req;
		}

		if (window.ActiveXObject) {
			return new ActiveXObject(getXmlHttpPrefix() + "".XmlHttp"");
		}
	}
	catch (ex) {}
	throw new Error(""Your browser does not support XmlHttp objects"");
};

function XmlDocument() {}

XmlDocument.create = function () {
	try {

		if (document.implementation && document.implementation.createDocument) {
			var doc = document.implementation.createDocument("""", """", null);

			if (doc.readyState == null) {
				doc.readyState = 1;
				doc.addEventListener(""load"", function () {
					doc.readyState = 4;
					if (typeof doc.onreadystatechange == ""function"")
						doc.onreadystatechange();
				}, false);
			}
			
			return doc;
		}
		if (window.ActiveXObject)
			return new ActiveXObject(getDomDocumentPrefix() + "".DomDocument"");
	}
	catch (ex) {}
	throw new Error(""Your browser does not support XmlDocument objects"");
};


if (window.DOMParser &&
	window.XMLSerializer &&
	window.Node && Node.prototype && Node.prototype.__defineGetter__) {
	Document.prototype.loadXML = function (s) {

		var doc2 = (new DOMParser()).parseFromString(s, ""text/xml"");

		while (this.hasChildNodes())
			this.removeChild(this.lastChild);
			
		for (var i = 0; i < doc2.childNodes.length; i++) {
			this.appendChild(this.importNode(doc2.childNodes[i], true));
		}
	};
	
	
	Document.prototype.__defineGetter__(""xml"", function () {
		return (new XMLSerializer()).serializeToString(this);
	});
}


var XmlHttpPoolArr = new Array();
var XmlHttpPoolSize = 100;
var XHPCurrentAvailableID = 0;

function XmlHttpPool() {}

XmlHttpPool.pick = function() {
	var pos = XHPCurrentAvailableID;
	XmlHttpPoolArr[pos] =  XmlHttp.create();
	
	XHPCurrentAvailableID >= (XmlHttpPoolSize-1) ? 0 : XHPCurrentAvailableID++
	
	return XmlHttpPoolArr[pos];
}
var s = ""上传完成,正在保存文件..."";
var t = ""上传失败"";
function progressBar()
{
this.totalSize = 100;
this.sizeCompleted = 0;
this.percentDone = ""0%"";
this.speed = 0;
this.setSize = function(totalSize,size,speed,leftHours,leftMinutes,leftSeconds)
{
	var oProgressInfo = document.getElementById(""progressInfo"");
	var oProgress = document.getElementById(""progress"");
	if (oProgress == null || oProgressInfo == null)
		return;

	if (totalSize <= 0)
		return;

	this.totalSize = totalSize;
	this.sizeCompleted = size;
	this.speed =  speed;

	if (size < 0)
		this.sizeCompleted = 0;
	else if (size > this.totalSize)
		this.sizeCompleted = this.totalSize;

	var sizeLeft = 0;
	var progressInfoText = """";
	sizeLeft = this.totalSize - this.sizeCompleted;

	this.percentDone = Math.round(size / this.totalSize * 100) + ""%"";
	oProgress.style.width = this.percentDone;

	if (sizeLeft > 0)
	{
        progressInfoText = ""{1}KB/{0}KB，{3}KB/秒，进度{2}，剩余时间{4}:{5}:{6}"";
		progressInfoText = progressInfoText.replace(""{0}"",this.totalSize);
        progressInfoText = progressInfoText.replace(""{1}"",this.sizeCompleted);
        progressInfoText = progressInfoText.replace(""{2}"",this.percentDone);
        progressInfoText = progressInfoText.replace(""{3}"",this.speed);
        progressInfoText = progressInfoText.replace(""{4}"",leftHours);
        progressInfoText = progressInfoText.replace(""{5}"",leftMinutes);
        progressInfoText = progressInfoText.replace(""{6}"",leftSeconds);
	}
	else
	{
		progressInfoText = s;
	}
    if (this.totalSize>{MaxSize})
    {
        alert(""上传文件不能大于{MaxSize}KB。"");
        UploadCancel();
    }
    else
    {
	    oProgressInfo.innerHTML = progressInfoText;
    }
}
this.UploadError = function()
{
	var oProgressInfo = document.getElementById(""progressInfo"");
	var oProgress = document.getElementById(""progress"");
	if (oProgressInfo != null)
		oProgressInfo.innerHTML = t;
	if (oProgress != null)

		oProgress.style.width = ""0"";
}
this.UploadComplete = function()
{
	var oProgressInfo = document.getElementById(""progressInfo"");
	var oProgress = document.getElementById(""progress"");
	if (oProgressInfo != null)
		oProgressInfo.innerHTML = s;
	if (oProgress != null)
		oProgress.style.width = ""100%"";
}
}
var iTimerID = null;
var xmlHttp = XmlHttpPool.pick();
var url = ""?uploadid={uploadid}&JCmsShowLoading=true"";
var pb = new progressBar();	
function LoadProgressInfo()
{
	try
	{
		xmlHttp.open(""GET"", url + ""&t="" + Math.random(), true);
		xmlHttp.send(null);
		xmlHttp.onreadystatechange = function()
		{
			LoadData(xmlHttp);
		}
	}
	catch(e)
	{
		alert(e)
	}
}

function LoadData(xmlhttp)
{
	if (xmlhttp.readyState == 4)
	{
		iTimerID = window.setTimeout(""LoadProgressInfo()"", " + this.Refresh.ToString()+ @"); 
		try{
			eval(xmlhttp.responseText);
		}
		catch(e)
		{
			alert(e)
		}
	}
}

function ClearTimer()
{
	if (iTimerID != null)
	{
		window.clearTimeout(iTimerID);
		iTimerID = null;
	}
}

function UploadCancel()
{
	location.href = location.href;
}
</script>
<style>
.progressBar{ " + this.progressBar + @"}
.progressInfo { " + this.progressInfoStyle + @"}
.progress {" + this.progress + @"}
</style>
<div class=""progressBar"" id=""progressBar"">
<div class=""progressInfo"" id=""progressInfo"">0%</div>
<div class=""progress"" id=""progress""></div>
</div>
			 ";
            string _Type = Public.FilterSql(HttpContext.Current.Request.Params["UploadType"]);
            return temp.Replace("{MaxSize}",UploadConfig.GetSingleConfig(_Type).MaxSize);
		}
	}
}
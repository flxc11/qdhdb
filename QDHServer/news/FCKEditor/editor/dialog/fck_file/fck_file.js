var dialog		= window.parent;
var oEditor		= dialog.InnerDialogLoaded();
var FCK			= oEditor.FCK;
var FCKLang		= oEditor.FCKLang;
var FCKConfig	= oEditor.FCKConfig;
var FCKRegexLib	= oEditor.FCKRegexLib;
var FCKTools	= oEditor.FCKTools;

dialog.AddTab('Upload',FCKLang.DlgFileUpload);
dialog.AddTab('BigUpload',FCKLang.DlgBigFileUpload);
dialog.AddTab('Network',oEditor.FCKLang.DlgFileNetwork);
if (FCKConfig.FileBrowser)
	dialog.AddTab('Browse',oEditor.FCKLang.DlgFileBrowse);

function OnDialogTabChange(tabCode)
{
	ShowE('divUpload',(tabCode=='Upload'));
	ShowE('divBigUpload',(tabCode=='BigUpload'));
	ShowE('divNetwork',(tabCode=='Network'));
	if (FCKConfig.FileBrowser)
		ShowE('divBrowse',(tabCode=='Browse'));
	dialog.SetAutoSize(true);
}

var oImage = dialog.Selection.GetSelectedElement();
if (oImage && oImage.tagName != 'IMG' && !(oImage.tagName == 'INPUT' && oImage.type == 'image'))
    oImage = null;
var oLink = dialog.Selection.GetSelection().MoveToAncestorNode('A');
if (oLink)
    FCK.Selection.SelectNode(oLink);

window.onload = function()
{
	oEditor.FCKLanguageManager.TranslatePage(document);
	LoadAnchorNamesAndIds();
	LoadSelection();
	if (FCKConfig.FileBrowser)
		GetE('tdBrowse').style.display='';
		GetE('frmBrowse').src = FCKConfig.FileBrowserURL;	
	if (FCKConfig.FileUpload)
		GetE('frmUpload').action = FCKConfig.FileUploadURL;
		GetE('frmBigUpload').src = FCKConfig.FileBigUploadURL;
	dialog.SetAutoSize(true);
	dialog.SetOkButton(true);
}

var bHasAnchors;
function LoadAnchorNamesAndIds()
{
	var aAnchors = new Array();
	var i;
	var oImages = oEditor.FCK.EditorDocument.getElementsByTagName('IMG');
	for(i=0;i<oImages.length ;i++)
	{
		if (oImages[i].getAttribute('_fckanchor'))
			aAnchors[aAnchors.length]=oEditor.FCK.GetRealElement(oImages[i]);
	}
	var oLinks=oEditor.FCK.EditorDocument.getElementsByTagName('A');
	for(i=0;i<oLinks.length;i++)
	{
		if (oLinks[i].name&&(oLinks[i].name.length>0))
			aAnchors[aAnchors.length]=oLinks[i];
	}
	var aIds=FCKTools.GetAllChildrenIds(oEditor.FCK.EditorDocument.body);
	bHasAnchors=(aAnchors.length>0||aIds.length>0);
	for (i=0;i<aAnchors.length;i++)
	{
		var sName=aAnchors[i].name;
		if (sName&&sName.length>0)
			FCKTools.AddSelectOption(GetE('cmbAnchorName'),sName,sName);
	}
	for (i=0;i<aIds.length;i++)
	{
		FCKTools.AddSelectOption(GetE('cmbAnchorId'),aIds[i],aIds[i]);
	}
}

function LoadSelection()
{
	if (!oLink) return;
	var sType='url';
	var sHRef=oLink.getAttribute('_fcksavedurl');
	if (sHRef==null)
		sHRef=oLink.getAttribute('href',2)||'';
	GetE('txtUrl').value    = sHRef;
	dialog.SetSelectedTab('Network');
}
function Ok()
{
	var sUri,sInnerHtml;
	oEditor.FCKUndo.SaveUndoStep();
	sUri=GetE('txtUrl').value;
	if (sUri.length==0)
	{
		alert(oEditor.FCKLang.DlgAlertUrl);
		return false ;
	}
	
	//Edit By Apollo/2010-09-07
	UpdateImage(sUri);
	
	var aLinks=oLink?[oLink]:oEditor.FCK.CreateLink(sUri,true);
	var aHasSelection=(aLinks.length>0);
	if (!aHasSelection)
	{
		if(GetE('txtFileName1').value!="")
		{
			sInnerHtml=GetE('txtFileName1').value
		}
		else
		{
			sInnerHtml=sUri;
		}
		aLinks = [oEditor.FCK.InsertElement('a')];
	}
	for (var i=0;i<aLinks.length;i++)
	{
		oLink=aLinks[i];
		if (aHasSelection)
			sInnerHtml=oLink.innerHTML;
		oLink.href=sUri;
		SetAttribute(oLink,'_fcksavedurl',sUri);
		oLink.innerHTML=sInnerHtml;
		SetAttribute(oLink,'target',null);
	}
	oEditor.FCKSelection.SelectNode(aLinks[0]);
	return true;
}
//Edit By Apollo/2010-09-07
function UpdateImage(filename) 
{
    oImage = FCK.EditorDocument.createElement('img');
    oImage.src = GetFilePic(filename);
    oImage.align = "absmiddle";
    oImage = FCK.InsertElement(oImage);
}
//Edit By Apollo/2010-09-07
function GetFilePic(filename) {
    var sExt;
    sExt = filename.substr(filename.lastIndexOf(".") + 1);
    sExt = sExt.toUpperCase();
    var sPicName;
    switch (sExt) {
        case "TXT":
            sPicName = "txt.gif";
            break;
        case "CHM":
        case "HLP":
            sPicName = "hlp.gif";
            break;
        case "DOC":
        case "DOCX":
            sPicName = "doc.gif";
            break;
        case "PDF":
            sPicName = "pdf.gif";
            break;
        case "MDB":
            sPicName = "mdb.gif";
            break;
        case "GIF":
            sPicName = "gif.gif";
            break;
        case "JPG":
            sPicName = "jpg.gif";
            break;
        case "BMP":
            sPicName = "bmp.gif";
            break;
        case "PNG":
            sPicName = "pic.gif";
            break;
        case "ASP":
        case "JSP":
        case "JS":
        case "PHP":
        case "PHP3":
        case "ASPX":
            sPicName = "code.gif";
            break;
        case "HTM":
        case "HTML":
        case "SHTML":
            sPicName = "htm.gif";
            break;
        case "ZIP":
            sPicName = "zip.gif";
            break;
        case "RAR":
            sPicName = "rar.gif";
            break;
        case "EXE":
            sPicName = "exe.gif";
            break;
        case "AVI":
            sPicName = "avi.gif";
            break;
        case "MPG":
        case "MPEG":
        case "ASF":
            sPicName = "mp.gif";
            break;
        case "RA":
        case "RM":
            sPicName = "rm.gif";
            break;
        case "MP3":
            sPicName = "mp3.gif";
            break;
        case "MID":
        case "MIDI":
            sPicName = "mid.gif";
            break;
        case "WAV":
            sPicName = "audio.gif";
            break;
        case "XLS":
        case "XLSX":
            sPicName = "xls.gif";
            break;
        case "PPT":
        case "PPTX":
        case "PPS":
            sPicName = "ppt.gif";
            break;
        case "SWF":
            sPicName = "swf.gif";
            break;
        default:
            sPicName = "unknow.gif";
            break;
    }
	var InstallDir=window.location.pathname.toLowerCase();
	InstallDir=InstallDir.substring(0,InstallDir.indexOf("fckeditor"));
	
    return InstallDir + "UploadFile/files/" + sPicName;
}
function BrowseServer()
{
	dialog.SetSelectedTab('Browse');
}
function SetUrl(url,filename,width,height)
{
    GetE('txtUrl').value = url;
    GetE('txtFileName1').value = filename;
	dialog.SetSelectedTab('Network');
}
function OnUploadCompleted(errorNumber,fileUrl,fileName,customMsg)
{
	window.parent.Throbber.Hide();
	GetE('divUpload').style.display = '';

	switch (errorNumber)
	{
		case 0 :
			alert('文件上传操作成功。');
			break ;
		case 1 :
			alert( customMsg ) ;
			return ;
		case 101 :	// Custom warning
			alert( customMsg ) ;
			break ;
		case 201 :
			alert('A file with the same name is already available. The uploaded file has been renamed to "' + fileName + '"' ) ;
			break ;
		case 202 :
			alert('系统不支持上传该文件类型。');
			return ;
		case 203 :
			alert("Security error. You probably don't have enough permissions to upload. Please check your server." ) ;
			return ;
		case 500 :
			alert( 'The connector is disabled' ) ;
			break ;
		default :
			alert( 'Error on file upload. Error number: ' + errorNumber ) ;
			return ;
	}
	//Edit By Apollo/2010-09-07
	SetUrl(fileUrl, fileName);
	GetE('frmUpload').reset() ;
}

var oUploadAllowedExtRegex	= new RegExp(FCKConfig.FileUploadAllowedExtensions,'i');
var oUploadDeniedExtRegex	= new RegExp(FCKConfig.FileUploadDeniedExtensions,'i');

function CheckUpload()
{
	var sFile = GetE('txtUploadFile').value;
	if (sFile.length==0)
	{
		alert('请选择一个附件再进行上传操作。');
		return false ;
	}
	if ((FCKConfig.FileUploadAllowedExtensions.length > 0 && !oUploadAllowedExtRegex.test(sFile)) ||
		(FCKConfig.FileUploadDeniedExtensions.length > 0 && oUploadDeniedExtRegex.test(sFile)))
	{
		OnUploadCompleted(202);
		return false ;
	}
	window.parent.Throbber.Show(100);
	GetE('divUpload').style.display  = 'none';
	return true;
}
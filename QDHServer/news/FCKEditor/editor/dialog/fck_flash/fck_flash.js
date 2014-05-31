var dialog		= window.parent;
var oEditor		= dialog.InnerDialogLoaded();
var FCK			= oEditor.FCK;
var FCKLang		= oEditor.FCKLang;
var FCKConfig	= oEditor.FCKConfig;
var FCKTools	= oEditor.FCKTools;

dialog.AddTab('Upload',FCKLang.DlgFlashUpload);
dialog.AddTab('Network',oEditor.FCKLang.DlgFlashNetwork);
if (FCKConfig.FlashBrowser)
	dialog.AddTab('Browse',oEditor.FCKLang.DlgFlashBrowse);

function OnDialogTabChange(tabCode)
{
	ShowE('divUpload',(tabCode=='Upload'));
	ShowE('divNetwork',(tabCode=='Network'));
	if (FCKConfig.FlashBrowser)
		ShowE('divBrowse',(tabCode=='Browse'));
}

var oFakeImage = dialog.Selection.GetSelectedElement() ;
var oEmbed ;

if (oFakeImage)
{
	if (oFakeImage.tagName == 'IMG' && oFakeImage.getAttribute('_fckflash') )
		oEmbed = FCK.GetRealElement( oFakeImage ) ;
	else
		oFakeImage = null ;
}
window.onload = function()
{
	oEditor.FCKLanguageManager.TranslatePage(document);
	LoadSelection();
	if (FCKConfig.FlashBrowser)
		GetE('tdBrowse').style.display='';
		GetE('frmBrowse').src = FCKConfig.FlashBrowserURL;	
	if (FCKConfig.FlashUpload)
		GetE('frmUpload').action = FCKConfig.FlashUploadURL;
	dialog.SetAutoSize(true);
	dialog.SetOkButton(true);
}
function LoadSelection()
{
	if (!oEmbed) return ;
	GetE('txtUrl').value    = GetAttribute(oEmbed,'src','');
	GetE('txtWidth').value  = GetAttribute(oEmbed,'width','') ;
	GetE('txtHeight').value = GetAttribute(oEmbed,'height','') ;
	UpdatePreview();
	dialog.SetSelectedTab('Network');
}
function Ok()
{
	if (GetE('txtUrl').value.length == 0)
	{
		dialog.SetSelectedTab('Network');
		GetE('txtUrl').focus();
		alert(FCKLang.DlgFlashAlertUrl);
		return false;
	}
	oEditor.FCKUndo.SaveUndoStep();
	if (!oEmbed)
	{
		oEmbed		= FCK.EditorDocument.createElement('EMBED');
		oFakeImage  = null;
	}
	UpdateEmbed(oEmbed);
	if (!oFakeImage)
	{
		oFakeImage	= oEditor.FCKDocumentProcessor_CreateFakeImage('FCK__Flash',oEmbed);
		oFakeImage.setAttribute('_fckflash','true',0);
		oFakeImage	= FCK.InsertElement(oFakeImage);
	}
	oEditor.FCKEmbedAndObjectProcessor.RefreshView(oFakeImage,oEmbed);
	return true;
}
function UpdateEmbed(e)
{
	SetAttribute(e,'type','application/x-shockwave-flash');
	SetAttribute(e,'pluginspage','http://www.macromedia.com/go/getflashplayer');
	SetAttribute(e,'src',GetE('txtUrl').value);
	SetAttribute(e,"width",GetE('txtWidth').value);
	SetAttribute(e,"height",GetE('txtHeight').value);
}

var ePreview;
function SetPreviewElement(previewEl)
{
	ePreview = previewEl ;

	if ( GetE('txtUrl').value.length > 0 )
		UpdatePreview() ;
}

function UpdatePreview()
{
	if (!ePreview)
		return ;
		
	while ( ePreview.firstChild )
		ePreview.removeChild( ePreview.firstChild ) ;
		
	if ( GetE('txtUrl').value.length == 0 )
		ePreview.innerHTML = '&nbsp;' ;
	else
	{
		var oDoc	= ePreview.ownerDocument || ePreview.document ;
		var e		= oDoc.createElement( 'EMBED' ) ;

		SetAttribute( e, 'src', GetE('txtUrl').value ) ;
		SetAttribute( e, 'type', 'application/x-shockwave-flash' ) ;
		SetAttribute( e, 'width', '100%' ) ;
		SetAttribute( e, 'height', '100%' ) ;

		ePreview.appendChild( e ) ;
	}
}
function BrowseServer()
{
	dialog.SetSelectedTab('Browse');
}
function SetUrl(url,width,height)
{
	GetE('txtUrl').value = url;
	if (width)
		GetE('txtWidth').value = width;
	if (height)
		GetE('txtHeight').value = height;
	UpdatePreview(); 
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

	SetUrl(fileUrl);
	GetE('frmUpload').reset() ;
}

var oUploadAllowedExtRegex	= new RegExp(FCKConfig.FlashUploadAllowedExtensions,'i');
var oUploadDeniedExtRegex	= new RegExp(FCKConfig.FlashUploadDeniedExtensions,'i');

function CheckUpload()
{
	var sFile = GetE('txtUploadFile').value ;

	if ( sFile.length == 0 )
	{
		alert('请选择一个Flash文件再进行上传操作。');
		return false ;
	}

	if ( ( FCKConfig.FlashUploadAllowedExtensions.length > 0 && !oUploadAllowedExtRegex.test( sFile ) ) ||
		( FCKConfig.FlashUploadDeniedExtensions.length > 0 && oUploadDeniedExtRegex.test( sFile ) ) )
	{
		OnUploadCompleted( 202 ) ;
		return false ;
	}

	window.parent.Throbber.Show( 100 ) ;
	GetE( 'divUpload' ).style.display  = 'none' ;

	return true ;
}

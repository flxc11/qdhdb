var dialog		= window.parent;
var oEditor		= dialog.InnerDialogLoaded();
var FCK			= oEditor.FCK;
var FCKLang		= oEditor.FCKLang;
var FCKConfig	= oEditor.FCKConfig;
var FCKTools	= oEditor.FCKTools;

dialog.AddTab('Upload', oEditor.FCKLang.DlgVideoUpload);
dialog.AddTab('Network', oEditor.FCKLang.DlgVideoNetwork);
dialog.AddTab('Browse', oEditor.FCKLang.DlgVideoBrowse);

function OnDialogTabChange(tabCode)
{
    ShowE('divUpload', (tabCode == 'Upload'));
    ShowE('divNetwork', (tabCode == 'Network'));
	ShowE('divBrowse', (tabCode == 'Browse'));
}

var oFakeImage = dialog.Selection.GetSelectedElement() ;
var oEmbed ;

if (oFakeImage)
{
	if (oFakeImage.tagName == 'IMG' && oFakeImage.getAttribute('_fckvideo') )
		oEmbed = FCK.GetRealElement(oFakeImage);
	else
		oFakeImage = null ;
}

window.onload = function()
{
	oEditor.FCKLanguageManager.TranslatePage(document) ;
	LoadSelection();
	if (FCKConfig.VideoBrowser)
		GetE('tdBrowse').style.display='';
		GetE('frmBrowse').src = FCKConfig.VideoBrowserURL;	
	if (FCKConfig.VideoUpload)
		GetE('frmUpload').src = FCKConfig.VideoUploadURL;
	dialog.SetAutoSize(true);
	dialog.SetOkButton(true);
}

function LoadSelection()
{
	if (!oEmbed) return;
	GetE('txtUrl').value    = GetAttribute(oEmbed,'src','');
	GetE('txtWidth').value  = GetAttribute(oEmbed,'width','');
	GetE('txtHeight').value = GetAttribute(oEmbed,'height','');
	dialog.SetSelectedTab('Network');
	UpdatePreview();
}
function Ok()
{
	if (GetE('txtUrl').value.length == 0)
	{
		dialog.SetSelectedTab('Network');
		alert("源文件地址不能为空。");
		return false;
	}
	oEditor.FCKUndo.SaveUndoStep();
	if (!oEmbed)
	{
		oEmbed		= FCK.EditorDocument.createElement('video');
		oFakeImage  = null;
	}
	UpdateEmbed(oEmbed);
	if (!oFakeImage)
	{
		oFakeImage	= oEditor.FCKDocumentProcessor_CreateFakeImage('FCK__Video',oEmbed);
		oFakeImage.setAttribute( '_fckvideo', 'true', 0 ) ;
		oFakeImage	= FCK.InsertElement(oFakeImage);
	}
	oEditor.FCKEmbedAndObjectProcessor.RefreshView(oFakeImage,oEmbed);
	return true;
}

function UpdateEmbed( e )
{
	SetAttribute( e, 'src', GetE('txtUrl').value);
	SetAttribute( e, "width" ,GetE('txtWidth').value);
	SetAttribute( e, "height" ,GetE('txtHeight').value);
	//SetAttribute( e, 'type'			,'application/x-shockwave-flash');
	//SetAttribute( e, 'pluginspage'	,'http://www.macromedia.com/go/getflashplayer');
	//SetAttribute( e, 'src',"/Images/Player.swf");
	// Advances Attributes
	/*SetAttribute( e, 'id'	, GetE('txtAttId').value ) ;
	SetAttribute( e, 'scale', GetE('cmbScale').value ) ;
	SetAttribute( e, 'play', GetE('chkAutoPlay').checked ? 'true' : 'false' ) ;
	SetAttribute( e, 'loop', GetE('chkLoop').checked ? 'true' : 'false' ) ;
	SetAttribute( e, 'menu', GetE('chkMenu').checked ? 'true' : 'false' ) ;
	SetAttribute( e, 'title'	, GetE('txtAttTitle').value ) ;
	if ( oEditor.FCKBrowserInfo.IsIE )
	{
		SetAttribute( e, 'className', GetE('txtAttClasses').value ) ;
		e.style.cssText = GetE('txtAttStyle').value ;
	}
	else
	{
		SetAttribute( e, 'class', GetE('txtAttClasses').value ) ;
		SetAttribute( e, 'style', GetE('txtAttStyle').value ) ;
	}*/
}

var ePreview ;
function SetPreviewElement( previewEl )
{
	ePreview = previewEl ;

	if ( GetE('txtUrl').value.length > 0 )
		UpdatePreview() ;
}

function UpdatePreview()
{
	if (!ePreview)
		return;
	while (ePreview.firstChild)
		ePreview.removeChild(ePreview.firstChild);
	/*if (GetE('txtUrl').value.length==0)
		ePreview.innerHTML = '&nbsp;';
	else
	{
		var oDoc	= ePreview.ownerDocument||ePreview.document;
		var e		= oDoc.createElement('EMBED');

		//SetAttribute(e,'src','flv_player.swf');
		//SetAttribute(e,'type','application/x-shockwave-flash');
		//SetAttribute(e,'FlashVars','vcastr_file='+GetE('txtUrl').value);
		SetAttribute(e,'src',GetE('txtUrl').value);
		SetAttribute(e,'type','application/x-oleobject');
		SetAttribute(e,'width','100%');
		SetAttribute(e,'height','100%');

		ePreview.appendChild(e);
	}*/
	if (GetE('txtUrl').value.length==0)
	{
		ePreview.innerHTML = '&nbsp;';
	}
	else
	{
		var oDoc	= ePreview.ownerDocument||ePreview.document;
		var e		= oDoc.createElement('EMBED');
		if(WinPlayer(GetE('txtUrl').value)!=null){ 
			SetAttribute(e,'src',GetE('txtUrl').value);
			SetAttribute(e,'type','application/x-oleobject');
			SetAttribute(e,'width','100%');
			SetAttribute(e,'height','100%');
			ePreview.appendChild(e);
		}
		if(FlvPlayer(GetE('txtUrl').value)!=null){ 
			SetAttribute(e,'src','flv_player.swf');
			SetAttribute(e,'type','application/x-shockwave-flash');
			SetAttribute(e,'FlashVars','vcastr_file='+GetE('txtUrl').value);
			SetAttribute(e,'width','100%');
			SetAttribute(e,'height','100%');
			ePreview.appendChild(e);
		}
	}
}
function WinPlayer(url){
	var r, re;
	re = /.(wmv|asf)$/i;
	r = url.match(re);
	return r;
}
function FlvPlayer(url){
	var r, re;
	re = /.flv$/i;
	r = url.match(re);
	return r;
} 
function BrowseServer()
{
	dialog.SetSelectedTab('Browse');
}
function SetUrl(url,width,height)
{
	GetE('txtUrl').value = url;
	UpdatePreview();
	dialog.SetSelectedTab('Network');
}
function OnUploadCompleted( errorNumber, fileUrl, fileName, customMsg )
{
	window.parent.Throbber.Hide() ;
	GetE('divUpload').style.display ='';

	switch ( errorNumber )
	{
		case 0 :	// No errors
			alert( 'Your file has been successfully uploaded' ) ;
			break ;
		case 1 :	// Custom error
			alert( customMsg ) ;
			return ;
		case 101 :	// Custom warning
			alert( customMsg ) ;
			break ;
		case 201 :
			alert( 'A file with the same name is already available. The uploaded file has been renamed to "' + fileName + '"' ) ;
			break ;
		case 202 :
			alert( 'Invalid file type' ) ;
			return ;
		case 203 :
			alert( "Security error. You probably don't have enough permissions to upload. Please check your server." ) ;
			return ;
		case 500 :
			alert( 'The connector is disabled' ) ;
			break ;
		default :
			alert( 'Error on file upload. Error number: ' + errorNumber ) ;
			return ;
	}
	SetUrl( fileUrl ) ;
	GetE('frmUpload').reset() ;
}

var oUploadAllowedExtRegex	= new RegExp( FCKConfig.FlashUploadAllowedExtensions, 'i' ) ;
var oUploadDeniedExtRegex	= new RegExp( FCKConfig.FlashUploadDeniedExtensions, 'i' ) ;

function CheckUpload()
{
	var sFile = GetE('txtUploadFile').value ;

	if ( sFile.length == 0 )
	{
		alert( 'Please select a file to upload' ) ;
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

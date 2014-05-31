var dialog		= window.parent ;
var oEditor		= dialog.InnerDialogLoaded();
var FCK			= oEditor.FCK;
var FCKLang		= oEditor.FCKLang;
var FCKConfig	= oEditor.FCKConfig;
var FCKDebug	= oEditor.FCKDebug;
var FCKTools	= oEditor.FCKTools;

var bImageButton = (document.location.search.length > 0 && document.location.search.substr(1)=='ImageButton');

dialog.AddTab('Upload',FCKLang.DlgImgUpload);
dialog.AddTab('Network',FCKLang.DlgImgNetwork);
if (FCKConfig.ImageBrowser)
	dialog.AddTab('Browse',FCKLang.DlgImgBrowse);

function OnDialogTabChange(tabCode)
{
	ShowE('divUpload',(tabCode == 'Upload'));
	ShowE('divNetwork',(tabCode == 'Network'));
	if (FCKConfig.ImageBrowser)
		ShowE('divBrowse',(tabCode == 'Browse'));
}
var oImage = dialog.Selection.GetSelectedElement();
if (oImage&&oImage.tagName!='IMG'&&!(oImage.tagName=='INPUT'&& oImage.type=='image'))
	oImage=null;
var oLink=dialog.Selection.GetSelection().MoveToAncestorNode('A');
var oImageOriginal;
function UpdateOriginal(resetSize)
{
	if (!eImgPreview)
		return;
	if (GetE('txtUrl').value.length == 0)
	{
		oImageOriginal = null;
		return;
	}
	oImageOriginal = document.createElement('IMG');
	if (resetSize)
	{
		oImageOriginal.onload = function()
		{
			this.onload = null ;
			ResetSizes();
		}
	}
	oImageOriginal.src = eImgPreview.src;
}

var bPreviewInitialized;
window.onload = function()
{
	oEditor.FCKLanguageManager.TranslatePage(document);
	GetE('btnLockSizes').title = FCKLang.DlgImgLockRatio;
	GetE('btnResetSize').title = FCKLang.DlgBtnResetSize;
	LoadSelection();
	UpdateOriginal();
	if (FCKConfig.ImageBrowser)
		GetE('tdBrowse').style.display='';
		GetE('frmBrowse').src = FCKConfig.ImageBrowserURL;
	if (FCKConfig.ImageUpload)
		GetE('frmUpload').action = FCKConfig.ImageUploadURL;
	dialog.SetAutoSize(true);
	dialog.SetOkButton(true);
}

function LoadSelection()
{
	if (!oImage) return; 
	var sUrl = oImage.getAttribute('_fcksavedurl');
	if (sUrl == null)
		sUrl = GetAttribute(oImage,'src','');
	GetE('txtUrl').value    = sUrl;
	GetE('txtAlt').value    = GetAttribute(oImage,'alt','');
	GetE('txtVSpace').value	= GetAttribute(oImage,'vspace','');
	GetE('txtHSpace').value	= GetAttribute(oImage,'hspace','');
	GetE('txtBorder').value	= GetAttribute(oImage,'border','');
	GetE('cmbAlign').value	= GetAttribute(oImage,'align','');

	var iWidth,iHeight;
	var regexSize = /^\s*(\d+)px\s*$/i;
	if (oImage.style.width)
	{
		var aMatchW  = oImage.style.width.match(regexSize);
		if (aMatchW)
		{
			iWidth = aMatchW[1];
			oImage.style.width ='';
			SetAttribute(oImage,'width',iWidth);
		}
	}
	if (oImage.style.height)
	{
		var aMatchH  = oImage.style.height.match(regexSize);
		if (aMatchH)
		{
			iHeight = aMatchH[1];
			oImage.style.height ='';
			SetAttribute(oImage,'height',iHeight);
		}
	}
	GetE('txtWidth').value	= iWidth ? iWidth : GetAttribute(oImage,"width",'');
	GetE('txtHeight').value	= iHeight ? iHeight : GetAttribute(oImage,"height",'');
	UpdatePreview();
	dialog.SetSelectedTab('Network');
}

function Ok()
{
	if (GetE('txtUrl').value.length ==0)
	{
		dialog.SetSelectedTab('Network');
		GetE('txtUrl').focus();
		alert(FCKLang.DlgImgAlertUrl);
		return false ;
	}
	var bHasImage=(oImage!=null);
	if (bHasImage&&bImageButton&&oImage.tagName=='IMG')
	{
		if (confirm( 'Do you want to transform the selected image on a image button?' ) )
			oImage = null ;
	}
	else if ( bHasImage && !bImageButton && oImage.tagName == 'INPUT' )
	{
		if ( confirm( 'Do you want to transform the selected image button on a simple image?' ) )
			oImage = null ;
	}
	oEditor.FCKUndo.SaveUndoStep();
	if (!bHasImage)
	{
		if (bImageButton)
		{
			oImage = FCK.EditorDocument.createElement('input');
			oImage.type = 'image' ;
			oImage = FCK.InsertElement( oImage ) ;
		}
		else
			oImage = FCK.InsertElement('img');
	}
	UpdateImage(oImage);
	return true;
}

function UpdateImage(e,skipId)
{
	e.src = GetE('txtUrl').value;
	SetAttribute(e,"_fcksavedurl",GetE('txtUrl').value);
	SetAttribute(e,"alt"   ,GetE('txtAlt').value);
	SetAttribute(e,"width" ,GetE('txtWidth').value);
	SetAttribute(e,"height",GetE('txtHeight').value);
	SetAttribute(e,"vspace",GetE('txtVSpace').value);
	SetAttribute(e,"hspace",GetE('txtHSpace').value);
	SetAttribute(e,"border",GetE('txtBorder').value);
	SetAttribute(e,"align" ,GetE('cmbAlign').value);
}

var eImgPreview ;
var eImgPreviewLink ;

function SetPreviewElements(imageElement,linkElement)
{
	eImgPreview = imageElement;
	eImgPreviewLink = linkElement;
	UpdatePreview();
	UpdateOriginal();
	bPreviewInitialized=true;
}

function UpdatePreview()
{
	if ( !eImgPreview || !eImgPreviewLink )
		return ;

	if ( GetE('txtUrl').value.length == 0 )
		eImgPreviewLink.style.display = 'none' ;
	else
	{
		UpdateImage( eImgPreview, true );
		eImgPreviewLink.style.display ='';
	}
}

var bLockRatio = true ;

function SwitchLock( lockButton )
{
	bLockRatio = !bLockRatio ;
	lockButton.className = bLockRatio ? 'BtnLocked' : 'BtnUnlocked' ;
	lockButton.title = bLockRatio ? 'Lock sizes' : 'Unlock sizes' ;

	if ( bLockRatio )
	{
		if ( GetE('txtWidth').value.length > 0 )
			OnSizeChanged( 'Width', GetE('txtWidth').value ) ;
		else
			OnSizeChanged( 'Height', GetE('txtHeight').value ) ;
	}
}

function OnSizeChanged( dimension, value )
{
	if ( oImageOriginal && bLockRatio )
	{
		var e = dimension == 'Width' ? GetE('txtHeight') : GetE('txtWidth') ;

		if ( value.length == 0 || isNaN( value ) )
		{
			e.value = '' ;
			return ;
		}

		if ( dimension == 'Width' )
			value = value == 0 ? 0 : Math.round( oImageOriginal.height * ( value  / oImageOriginal.width ) ) ;
		else
			value = value == 0 ? 0 : Math.round( oImageOriginal.width  * ( value / oImageOriginal.height ) ) ;

		if ( !isNaN( value ) )
			e.value = value ;
	}

	UpdatePreview() ;
}

function ResetSizes()
{
	if (!oImageOriginal) return;
	if (oEditor.FCKBrowserInfo.IsGecko && !oImageOriginal.complete)
	{
		setTimeout(ResetSizes,50);
		return;
	}
	GetE('txtWidth').value  = oImageOriginal.width;
	GetE('txtHeight').value = oImageOriginal.height;
	UpdatePreview();
}

function BrowseServer()
{
	dialog.SetSelectedTab('Browse');
}

var sActualBrowser ;
function SetUrl(url,width,height,alt)
{
	GetE('txtUrl').value=url ;
	GetE('txtWidth').value=width?width:'';
	GetE('txtHeight').value=height?height:'';
	if (alt)
		GetE('txtAlt').valu =alt;
	UpdatePreview();
	UpdateOriginal(true);
	dialog.SetSelectedTab('Network');
}

function OnUploadCompleted(errorNumber,fileUrl,fileName,customMsg)
{
	window.parent.Throbber.Hide() ;
	GetE('divUpload').style.display ='';
	
	switch (errorNumber)
	{
		case 0 :
			alert('上传文件操作成功。');
			break ;
		case 1 :	// Custom error
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
			alert( "Security error. You probably don't have enough permissions to upload. Please check your server." ) ;
			return ;
		case 500 :
			alert( 'The connector is disabled' ) ;
			break ;
		default :
			alert( 'Error on file upload. Error number: ' + errorNumber ) ;
			return ;
	}
	sActualBrowser='';
	SetUrl(fileUrl);
	GetE('frmUpload').reset();
}

var oUploadAllowedExtRegex	= new RegExp(FCKConfig.ImageUploadAllowedExtensions,'i');
var oUploadDeniedExtRegex	= new RegExp(FCKConfig.ImageUploadDeniedExtensions,'i');

function CheckUpload()
{
	var sFile=GetE('txtUploadFile').value;
	if (sFile.length==0)
	{
		alert('请选择一个图像文件再进行上传操作。');
		return false;
	}
	if ((FCKConfig.ImageUploadAllowedExtensions.length > 0 && !oUploadAllowedExtRegex.test(sFile)) ||
		(FCKConfig.ImageUploadDeniedExtensions.length > 0 && oUploadDeniedExtRegex.test(sFile)))
	{
		OnUploadCompleted(202);
		return false ;
	}
	window.parent.Throbber.Show(100);
	GetE('divUpload').style.display ='none';
	return true ;
}
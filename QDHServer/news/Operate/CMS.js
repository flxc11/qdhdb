var BasePath = "";
/*************************************************
描述：选择图片
作者：Apollo
时间：2010-04-09
*************************************************/
//function CmsSelectImages(obj) {
//    setTBConfig("attachEvent", function() {CmsImagesReturnValues(obj);});
//    showTB(BasePath + "CmsImages.aspx", 650, 400, "选择图片", 'parent');
//}
/*************************************************
描述：设置选择图片值
作者：Apollo
时间：2010-04-09
*************************************************/
function CmsImagesReturnValues(obj)
{
    var retVal = getTBConfig("retVal");
    if (retVal && retVal.length > 0) {
        $("#"+obj).attr("value", retVal);
    }
}
/*************************************************
描述：浏览图片
作者：Apollo
时间：2010-04-09
*************************************************/
function CmsViewImages(URL,FileExtension)
{
	$("#ImagesView").attr('src',URL);
	$("#ImagesURL").attr('value',URL);
	$("#HidFileExtension").attr('value',FileExtension);
}
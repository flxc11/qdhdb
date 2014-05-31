var BasePath = "";
/*************************************************
描述：选择图片
作者：East-Red
时间：2013-08-05
*************************************************/
function CmsSelectImages(obj) {
    setTBConfig("attachEvent", function () { CmsImagesReturnValues(obj); });
    showTB(BasePath + "Operate/CmsImages.aspx", 650, 400, "选择图片", 'self');
}
/*************************************************
描述：设置选择图片值
作者：Apollo
时间：2010-04-09
*************************************************/
function CmsImagesReturnValues(obj) {
    var retVal = getTBConfig("retVal");
    if (retVal && retVal.length > 0) {
        $("#" + obj).attr("value", retVal);
    }
}
/*************************************************
描述：选择图片
作者：Apollo
时间：2011-08-16
*************************************************/
function CmsSelectFiles() {
    setTBConfig("attachEvent", function () { CmsFilesReturnValues(); });
    showTB(BasePath + "Operate/CmsFiles.aspx", 650, 400, "选择附件", 'self');
}
/*************************************************
描述：设置选择图片值
作者：Apollo
时间：2011-08-16
*************************************************/
function CmsFilesReturnValues() {
    var retVal = getTBConfig("retVal");
    if (retVal && retVal.length > 0) {
        $("#TxtFiles").attr("value", retVal);
    }
}
/*************************************************
描述：浏览图片
作者：Apollo
时间：2010-04-09
*************************************************/
function CmsViewImages(URL, FileExtension) {
    $("#ImagesView").attr('src', URL);
    $("#ImagesURL").attr('value', URL);
    $("#HidFileExtension").attr('value', FileExtension);
}
/*************************************************
描述：文章排序
作者：Apollo
时间：2010-01-13
*************************************************/
function CmsOrder(ParentID) {
    if (ParentID == null | ParentID == "") {
        alert("请先选择栏目进行搜索后再进行排序！");
    } else {
        showTB(BasePath + "CmsOrder.aspx?ParentID=" + ParentID, 500, 300, "栏目排序", 'parent');
    }
    
}
/*************************************************
描述：向上移动到对应的位置
作者：Apollo
时间：2009-12-04
*************************************************/
function moveUp(obj, jto) {
    var selectedIndex = obj.selectedIndex;
    if (selectedIndex <= 0) {
        return false;
    }
    else if (selectedIndex == 1) {
        alert("所在位置已经在顶端了！");
        return false;
    }
    else if (parseInt(jto) >= selectedIndex) {
        alert("请输入小于所选位置的数值！");
        return false;
    }

    var temp = "";
    var len = 0;
    for (i = 0; i < obj.options.length; i++) {
        if (obj.options[i].selected) {
            len += 1;
            temp += temp == "" ? i : "," + i;
        }
    }
    var arr = temp.split(",");
    var tempindex = "";
    //如果没有输入指定的行号,则默认为上移一个位置
    if (jto * 1 == 0) {
        for (var i = 0; i < len; i++) {
            jto = jto + i;
            var sindex = arr[i] * 1;
            if (sindex == (jto * 1 + 1)) {
                continue;
            }
            tempindex += tempindex == "" ? (sindex - 1) : "," + (sindex - 1);

            for (var j = sindex; j > sindex - 1; j--) {
                var otext = obj.options[j - 1].text
                var ovalue = obj.options[j - 1].value
                obj.options[j - 1].text = obj.options[j].text
                obj.options[j - 1].value = obj.options[j].value
                obj.options[j].text = otext
                obj.options[j].value = ovalue
                obj.options[j].selected = false;
            }
        }
    }
    else //上移到指定的位置
    {
        var nspot = 1;
        for (var i = 0; i < len; i++) {
            nspot = i == 0 ? 0 : 1;
            jto = jto * 1 + nspot;
            tempindex += tempindex == "" ? jto : "," + jto;
            var sindex = arr[i] * 1;
            for (var j = sindex; j > jto; j--) {
                var otext = obj.options[j - 1].text
                var ovalue = obj.options[j - 1].value
                obj.options[j - 1].text = obj.options[j].text
                obj.options[j - 1].value = obj.options[j].value
                obj.options[j].text = otext
                obj.options[j].value = ovalue

            }
            obj.options[sindex].selected = false;
        }
    } //end else

    var selectArr = tempindex.split(",");
    if (selectArr.length > 0) {
        for (var i = 0; i < selectArr.length; i++) {
            if (selectArr[i] == "")
                continue;
            obj.options[selectArr[i]].selected = true;
        }
    }
}
/*************************************************
描述：向下移动到对应的位置
作者：Apollo
时间：2009-12-04
*************************************************/
function moveDown(obj, jto) {
    var temp = "";
    var len = 0;
    var lastsel = 0;
    for (i = 0; i < obj.options.length; i++) {
        if (obj.options[i].selected) {
            lastsel = i;
            len += 1;
            temp += temp == "" ? i : "," + i;
        }
    }

    var selectedIndex = obj.selectedIndex;
    if (selectedIndex <= 0) {
        return false;
    }
    else if (lastsel == obj.options.length - 1) {
        alert("所在位置已经在底端了！");
        return false;
    }
    else if (parseInt(jto) <= lastsel) {
        alert("请输入大于所选值的数值！");
        return false;
    }

    var arr = temp.split(",");
    var tempindex = "";

    //如果没有输入指定的行号,则默认为上移一个位置
    if (jto * 1 == 0) {
        for (var i = len - 1; i >= 0; i--) {
            var sindex = arr[i] * 1;
            if (sindex == (obj.options.length * 1 - i)) {
                continue;
            }
            tempindex += tempindex == "" ? (sindex + 1) : "," + (sindex + 1);
            for (var j = sindex; j < sindex + 1; j++) {
                var otext = obj.options[j + 1].text
                var ovalue = obj.options[j + 1].value
                obj.options[j + 1].text = obj.options[j].text
                obj.options[j + 1].value = obj.options[j].value
                obj.options[j].text = otext
                obj.options[j].value = ovalue
                obj.options[j].selected = false;
            }
        }
    }
    else //下移到指定的位置
    {
        var spot = 0;
        for (var i = len - 1; i >= 0; i--) {
            spot = (i == len - 1) ? 0 : 1;
            jto = jto * 1 - spot;
            tempindex += tempindex == "" ? jto : "," + jto;
            var sindex = arr[i] * 1;
            for (var j = sindex; j < jto; j++) {
                var otext = obj.options[j + 1].text
                var ovalue = obj.options[j + 1].value
                obj.options[j + 1].text = obj.options[j].text
                obj.options[j + 1].value = obj.options[j].value
                obj.options[j].text = otext
                obj.options[j].value = ovalue
            }

            spot++;
            obj.options[sindex].selected = false;
        }
    }

    var selectArr = tempindex.split(",");
    if (selectArr.length > 0) {
        for (var i = 0; i < selectArr.length; i++) {
            if (selectArr[i] == "")
                continue;
            obj.options[selectArr[i]].selected = true;
        }
    }
}
/*************************************************
描述：检查排序值是否在指定范围
作者：Apollo
时间：2009-12-04
*************************************************/
function checkInput(obj) {
    with (obj) {
        var intJumpto = parseInt(jumpto.value);
        var intMaxnum = parseInt(maxnum.value);

        if (intJumpto < 1 || intJumpto > intMaxnum) {
            jumpto.value = intMaxnum;
        }
    }
}
/*************************************************
描述：自定义检索栏目进行排序操作
作者：Apollo
时间：2009-12-04
*************************************************/
function CmsSearch(ParentID) {
    if (ParentID == null | ParentID == "") {
        ParentID = "0";
    }
    if ($("#TxtMin").val() == "") {
        alert("请输入正确的数字范围！");
        $("#TxtMin").focus();
        return false;
    }
    if (!isNumber($("#TxtMin").val())) {
        alert("请输入正确的数字范围！");
        $("#TxtMin").focus();
        return false;
    }
    var FirstRow = $("#TxtMin").val();
    if ($("#TxtMax").val() == "") {
        alert("请输入正确的数字范围！");
        $("#TxtMax").focus();
        return false;
    }
    if (!isNumber($("#TxtMax").val())) {
        alert("请输入正确的数字范围！");
        $("#TxtMin").focus();
        return false;
    }
    var EndRow = $("#TxtMax").val()
    if (($("#TxtMin").val() * 1) >= ($("#TxtMax").val() * 1)) {
        alert("请输入正确的数字范围！");
        return false;
    }
    window.location.href = "?ParentID=" + ParentID + "&FirstRow=" + FirstRow + "&EndRow=" + EndRow;
}
/*************************************************
描述：判断是否为数字
作者：Apollo
时间：2009-12-04
*************************************************/
function isNumber(Str) {
    return (!isNaN(parseInt(Str))) ? true : false;
}
/*************************************************
描述：仅能输入数字
作者：Apollo
时间：2009-12-04
*************************************************/
function funKeyDown(event) {
    if ((event.keyCode < 48 || event.keyCode > 57) && event.keyCode != 37
	&& event.keyCode != 39 && event.keyCode != 46 && event.keyCode != 8
	&& event.keyCode != 189 && event.keyCode != 109 && event.keyCode != 110 && event.keyCode != 9
	&& (event.keyCode < 96 || event.keyCode > 105)) {
        return false;
    }
    else {
        return true;
    }
}
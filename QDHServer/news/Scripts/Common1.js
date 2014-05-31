/*************************************************
描述:正则表达式,判断整数
作者:Vigour
时间:2011-09-09 
*************************************************/
function IsInteger(s) {
    var patrn = /^[-\+]?\d+$/;
    if (!patrn.exec(s)) return false;
    return true;
}
/*************************************************
描述:正则表达式,判断双浮点数
作者:Vigour
时间:2011-09-09 
*************************************************/
function IsDouble(s) {
    var patrn = /^[-\+]?(:?(:?\d+.\d+)|(:?\d+))$/;
    if (!patrn.exec(s)) return false;
    return true;
}
/*************************************************
描述:正则表达式,判断QQ
作者:Vigour
时间:2011-09-09 
*************************************************/
function IsValidQQ(s) {
    var patrn = /^\s*[.0-9]{5,10}\s*$/;
    if (!patrn.exec(s)) return false;
    return true;
}
/*************************************************
描述:正则表达式,判断域名,网址
作者:Vigour
时间:2011-09-09 
*************************************************/
function IsValidURL(s) {
    var patrn = /^http:\/\/[A-Za-z0-9]+\.[A-Za-z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^\"\"])*$/;
    if (!patrn.exec(s)) return false;
    return true;
}
/*************************************************
描述:正则表达式,判断Email(邮箱)
作者:Vigour
时间:2011-09-09 
*************************************************/
function IsValidEMail(s) {
    var patrn = /(\S)+[@]{1}(\S)+[.]{1}(\w)+/;
    if (!patrn.exec(s)) return false;
    return true;
}
/*************************************************
描述:正则表达式,判断移动电话
作者:Vigour
时间:2011-09-09 
*************************************************/
function IsValidMobilePhone(s) {
    var patrn = /^1[3|5|8]\d{9}$/;
    if (!patrn.exec(s)) return false;
    return true;
}
/*************************************************
描述:正则表达式,判断固定电话
作者:Vigour
时间:2011-10-20 
*************************************************/
function IsValidPhone(s) {
    var patrn = /^(([0\+]\d{2,3}-?)?(0\d{2,3})-?)?(\d{7,8})(-?(\d{3,}))?$/;
    if (!patrn.exec(s)) return false;
    return true;
}
/*************************************************
描述:正则表达式,判断固定电话，移动电话(排除86)
作者:Vigour
时间:2011-09-09 
*************************************************/
function IsValidPhoneAndMobilePhone(s) {
    var patrn = /(^(0[0-9]{2,3}\-?)?([2-9][0-9]{6,7})(\-?[0-9]{1,4})?$)|(^((\(\d{3}\))|(\d{3}\-?))?(1[358]\d{9})$)/;
    if (!patrn.exec(s)) return false;
    return true;
}
/*************************************************
描述:正则表达式,判断判断邮编
作者:Vigour
时间:2011-09-09 
*************************************************/
function IsPostId(s) {
    var patrn = /^\d{6}$/;
    if (!patrn.exec(s)) return false;
    return true;
}

//=============================切换验证码======================================
function ToggleCode(obj, codeurl) {
    $(obj).attr("src", codeurl + "?time=" + Math.random());
}

//表格隔行变色
$(function () {
    $(".msgtable tr:nth-child(odd)").addClass("tr_odd_bg"); //隔行变色
    $(".msgtable tr").hover(
			    function () {
			        $(this).addClass("tr_hover_col");
			    },
			    function () {
			        $(this).removeClass("tr_hover_col");
			    }
		    );
});
//==========================页面加载时JS函数结束===============================

//===========================系统管理JS函数开始================================

//Tab控制函数
function tabs(tabId, tabNum) {
    //设置点击后的切换样式
    $(tabId + " .tab_nav li").removeClass("selected");
    $(tabId + " .tab_nav li").eq(tabNum).addClass("selected");
    //根据参数决定显示内容
    $(tabId + " .tab_con").hide();
    $(tabId + " .tab_con").eq(tabNum).show();
    $(".imgShow").hide();
    $(".imgDefect" + tabNum).show();   
}

//可以自动关闭的提示
function jsprint(msgtitle, url, msgcss, callback) {
    $("#msgprint").remove();
    var cssname = "";
    switch (msgcss) {
        case "Success":
            cssname = "pcent success";
            break;
        case "Error":
            cssname = "pcent error";
            break;
        default:
            cssname = "pcent warning";
            break;
    }
    var str = "<div id=\"msgprint\" class=\"" + cssname + "\">" + msgtitle + "</div>";
    $("body").append(str);
    $("#msgprint").show();
	var itemiframe = "#framecenter .l-tab-content .l-tab-content-item";
    var curriframe = "";
    $(itemiframe).each(function () {
        if ($(this).css("display") != "none") {
			curriframe = $(itemiframe).index($(this));
            return false;
        }
    });
    if (url == "back" && curriframe != "") {
        frames[curriframe].history.back(-1);
    } else if (url != "" && curriframe != "") {
        frames[curriframe].location.href = url;
    }
    //3秒后清除提示
    setTimeout(function () {
        $("#msgprint").fadeOut(500);
        //如果动画结束则删除节点
        if (!$("#msgprint").is(":animated")) {
            $("#msgprint").remove();
        }
    }, 3000);
    //执行回调函数
    if (typeof (callback) == "function") callback();
}

//全选取消按钮函数，调用样式如：
function checkAll(chkobj) {
	if($(chkobj).find("span b").text()=="全选")
	{
	    $(chkobj).find("span b").text("取消");
	    $("input[class='cbCheck']").attr("checked", true);
	}else{
        $(chkobj).find("span b").text("全选");
        $("input[class='cbCheck']").attr("checked", false);
	}
}
function checkAll2(chkobj) {
    if ($(chkobj).find("span b").text() == "全选") {
        $(chkobj).find("span b").text("取消");
        $(".checkall input").attr("checked", true);
    } else {
        $(chkobj).find("span b").text("全选");
        $(".checkall input").attr("checked", false);
    }
}

//执行回传函数
function ExePostBack(objId, objmsg) {
    if ($(".checkall input:checked").size() < 1) {
        $.ligerDialog.warn("对不起，请选中您要操作的记录。");
        return false;
    }
    var msg = "删除记录后不可恢复，您确定吗？";
    if (arguments.length == 2) {
        msg = objmsg;
    }
    $.ligerDialog.confirm(msg, "提示信息", function (result) {
        if (result) {
            __doPostBack(objId, '');
        }
    });
    return false;
}

/*************************************************
描述:选择数据后，批量删除操作
作者:Vigour
时间:2011-09-09 
*************************************************/
var confirmMsg = "您确定需要执行本操作吗？", ckCtrl = 'input:checkbox[class="cbCheck"]:checked';

/*************************************************
描述:单个删除操作
作者:Vigour
时间:2011-09-09 
*************************************************/
function DeleteOne(confirmMsg, postData, url) {
    if (confirmMsg == "" || window.confirm(confirmMsg)) {
        if (postData != null) {
            PostDelDate(url, postData);
        }
    }
}
/*************************************************
描述:选择数据后，批量删除操作
作者:Vigour
时间:2011-09-09 
*************************************************/
function DeleteSeleted(confirmMsg, ckCtrl, url,action) {
    var checkedObj = $(ckCtrl);
    var setValue = null;
    checkedObj.each(function () { var isCheck = this.value; if (setValue == null) { setValue = isCheck; } else { setValue += "," + isCheck; } });
    if (ckCtrl != "") {
        if (setValue == null) {
            $.ligerDialog.warn("对不起，请选中您要操作的记录。");
            return false;
        }
        else {
            $.ligerDialog.confirm(confirmMsg, "提示信息", function (result) {
                if (result) {
                    PostDelDate(url, setValue,action);
                }
            });
            return false;
        }
    }
    else {
        $.ligerDialog.warn("该项目无数据。");
        return false;
    }
}
/*************************************************
描述:指定地址，发送数据
作者:Vigour
时间:2011-09-09 
*************************************************/
function PostDelDate(Url, setValue, action) {
    console.log(Url + "--" + setValue + "--" + action);
	var params = {
		ID: setValue,
		Action: action
	};
    $.ajax({
        type: "post",
        dataType: "json",
        data: params,
        url: Url + "?Time=" + (new Date().getTime()),
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //$.ligerDialog.warn("服务运行异常，请联系系统管理员！");
            alert(XMLHttpRequest.status);
            alert(XMLHttpRequest.readyState);
            alert(textStatus);

        },
        success: function (d) {
            if (d.msgCode == "1") {
                parent.jsprint(d.msgStr, "", "Success");
                self.location.reload();
            }
            else {
                $.ligerDialog.warn(d.msgStr);
            }
        }
    });
}









//关闭提示窗口
function CloseTip(objId) {
    $("#" + objId).hide();
}

//===========================系统管理JS函数结束================================

//================上传文件JS函数开始，需和jquery.form.js一起使用===============
//文件上传
function Upload(action, repath, uppath, iswater, isthumbnail, filepath) {
    var sendUrl = "../tools/upload_ajax.ashx?action=" + action + "&ReFilePath=" + repath + "&UpFilePath=" + uppath;
    //判断是否打水印
    if(arguments.length == 4){
        sendUrl = "../tools/upload_ajax.ashx?action=" + action + "&ReFilePath=" + repath + "&UpFilePath=" + uppath + "&IsWater=" + iswater;
    }
    //判断是否生成宿略图
    if (arguments.length == 5) {
        sendUrl = "../tools/upload_ajax.ashx?action=" + action + "&ReFilePath=" + repath + "&UpFilePath=" + uppath + "&IsWater=" + iswater + "&IsThumbnail=" + isthumbnail;
    }
    //自定义上传路径
    if (arguments.length == 6) {
        sendUrl = filepath + "tools/upload_ajax.ashx?action=" + action + "&ReFilePath=" + repath + "&UpFilePath=" + uppath + "&IsWater=" + iswater + "&IsThumbnail=" + isthumbnail;
    }
    //开始提交
    $("#form1").ajaxSubmit({
        beforeSubmit: function (formData, jqForm, options) {
            //隐藏上传按钮
            $("#" + repath).nextAll(".files").eq(0).hide();
            //显示LOADING图片
            $("#" + repath).nextAll(".uploading").eq(0).show();
        },
        success: function (data, textStatus) {
            if (data.msg == 1) {
                $(".files").css("background-image", "url(" + data.msbox + ")");
                $("#" + repath).val(data.msbox);
            } else {
                alert(data.msbox);
            }
            $("#" + repath).nextAll(".files").eq(0).show();
            $("#" + repath).nextAll(".uploading").eq(0).hide();
        },
        error: function (data, status, e) {
            alert("上传失败，错误信息：" + e);
            $("#" + repath).nextAll(".files").eq(0).show();
            $("#" + repath).nextAll(".uploading").eq(0).hide();
        },
        url: sendUrl,
        type: "post",
        dataType: "json",
        timeout: 600000
    });
};
//文件上传
function Upload2(action, repath, uppath, iswater, isthumbnail, filepath) {
    var sendUrl = "../tools/upload_ajax.ashx?action=" + action + "&ReFilePath=" + repath + "&UpFilePath=" + uppath;
    //判断是否打水印
    if (arguments.length == 4) {
        sendUrl = "../tools/upload_ajax.ashx?action=" + action + "&ReFilePath=" + repath + "&UpFilePath=" + uppath + "&IsWater=" + iswater;
    }
    //判断是否生成宿略图
    if (arguments.length == 5) {
        sendUrl = "../tools/upload_ajax.ashx?action=" + action + "&ReFilePath=" + repath + "&UpFilePath=" + uppath + "&IsWater=" + iswater + "&IsThumbnail=" + isthumbnail;
    }
    //自定义上传路径
    if (arguments.length == 6) {
        sendUrl = filepath + "tools/upload_ajax.ashx?action=" + action + "&ReFilePath=" + repath + "&UpFilePath=" + uppath + "&IsWater=" + iswater + "&IsThumbnail=" + isthumbnail;
    }
    //开始提交
    $("#form1").ajaxSubmit({
        beforeSubmit: function (formData, jqForm, options) {
            //隐藏上传按钮
            $("#" + repath).nextAll(".files").eq(0).hide();
            //显示LOADING图片
            $("#" + repath).nextAll(".uploading").eq(0).show();
        },
        success: function (data, textStatus) {
            if (data.msg == 1) {
                var item = data.msbox.split(',');
                $(".files").css("background-image", "url(" + item[1] + ")");
                $("#" + repath).val(item[1]);
            } else {
                alert(data.msbox);
            }
            $("#" + repath).nextAll(".files").eq(0).show();
            $("#" + repath).nextAll(".uploading").eq(0).hide();
        },
        error: function (data, status, e) {
            alert("上传失败，错误信息：" + e);
            $("#" + repath).nextAll(".files").eq(0).show();
            $("#" + repath).nextAll(".uploading").eq(0).hide();
        },
        url: sendUrl,
        type: "post",
        dataType: "json",
        timeout: 600000
    });
};
//文件上传
function Upload3(action, repath, uppath, iswater, isthumbnail, controlId) {
    var sendUrl = "../tools/upload_ajax.ashx?action=" + action + "&ReFilePath=" + repath + "&UpFilePath=" + uppath;
    //判断是否打水印
    if (arguments.length == 5) {
        sendUrl = "../tools/upload_ajax.ashx?action=" + action + "&ReFilePath=" + repath + "&UpFilePath=" + uppath + "&IsWater=" + iswater;
    }
    //判断是否生成宿略图
    if (arguments.length == 6) {
        sendUrl = "../tools/upload_ajax.ashx?action=" + action + "&ReFilePath=" + repath + "&UpFilePath=" + uppath + "&IsWater=" + iswater + "&IsThumbnail=" + isthumbnail;
    }
    //开始提交
    $("#form1").ajaxSubmit({
        beforeSubmit: function (formData, jqForm, options) {
        },
        success: function (data, textStatus) {
            if (data.msg == 1) {
                var item = data.msbox.split(',');
                if (controlId != "") {
                    $("#" + controlId).attr("src", item[1]);
                }
                $("#" + repath).val(data.msbox);
            } else {
                alert(data.msbox);
            }
        },
        error: function (data, status, e) {
            alert("上传失败，错误信息：" + e);
            $("#" + repath).nextAll(".files").eq(0).show();
            $("#" + repath).nextAll(".uploading").eq(0).hide();
        },
        url: sendUrl,
        type: "post",
        dataType: "json",
        timeout: 600000
    });
};
//附件上传
function AttachUpload(repath, uppath) {
    var submitUrl = "../tools/upload_ajax.ashx?action=AttachFile&UpFilePath=" + uppath;
    //开始提交
    $("#form1").ajaxSubmit({
        beforeSubmit: function (formData, jqForm, options) {
            //隐藏上传按钮
            $("#" + uppath).parent().hide();
            //显示LOADING图片
            $("#" + uppath).parent().nextAll(".uploading").eq(0).show();
        },
        success: function (data, textStatus) {
            if (data.msg == 1) {
                var listBox = $("#" + repath + " ul");
                var newLi = '<li>'
                + '<input name="hidFileName" type="hidden" value="0|' + data.mstitle + "|" + data.msbox + '" />'
                + '<b title="删除" onclick="DelAttachLi(this);"></b>附件：' + data.mstitle
                + '</li>';
                listBox.append(newLi);
                //alert(data.mstitle);
            } else {
                alert(data.msbox);
            }
            $("#" + uppath).parent().show();
            $("#" + uppath).parent().nextAll(".uploading").eq(0).hide();
        },
        error: function (data, status, e) {
            alert("上传失败，错误信息：" + e);
            $("#" + uppath).parent().show();
            $("#" + uppath).parent().nextAll(".uploading").eq(0).hide();
        },
        url: submitUrl,
        type: "post",
        dataType: "json",
        timeout: 600000
    });
};
//===========================上传文件JS函数结束================================


//地区绑定
$(function () {
    $("#ddlProvince").change(function () {
        getCity();
    });
    $("#ddlCity").change(function () {
        getArea();
    });
});

function getPro() {
    //查询省份
    $.ajax({
        type: "post",
        dataType: "json",
        data: "",
        url: "/MemberShip/Ajax/AjaxCode.ashx?action=GetProvince",
        success: function (d) {
            var data = eval(d);
            $("#ddlProvince").empty();
            for (var i = 0; i < data.length; i++) {
                var option = "<option value='" + data[i].Code + "'>" + data[i].Name + "</option>";
                $("#ddlProvince").append(option);
            }
            getCity();
        }
    });
}

function getCity() {
    //查询城市
    var proId = $("#ddlProvince").val();
    $.ajax({
        type: "post",
        dataType: "json",
        data: "proId=" + proId,
        url: "/MemberShip/Ajax/AjaxCode.ashx?action=GetCity",
        success: function (d) {
            $("#ddlCity").empty();
            var data = eval(d);
            for (var i = 0; i < data.length; i++) {
                var option = "<option value='" + data[i].Code + "'>" + data[i].Name + "</option>";
                $("#ddlCity").append(option);
            }
            getArea();
        }
    });
}

function getArea() {
    //查询地区
    var cityId = $("#ddlCity").val();
    $.ajax({
        type: "post",
        dataType: "json",
        data: "cityId=" + cityId,
        url: "/MemberShip/Ajax/AjaxCode.ashx?action=GetArea",
        success: function (d) {
            $("#ddlArea").empty();
            var data = eval(d);
            for (var i = 0; i < data.length; i++) {
                var option = "<option value='" + data[i].Code + "'>" + data[i].Name + "</option>";
                $("#ddlArea").append(option);
            }
        }
    });
}

//会员中心左侧栏目
$(function () {
    $('#left_menu h3').click(function () {
        $(this).next('ul').slideToggle('slow');
    });
    var current_menu = location.href;
    var host_url = location.protocol + '//' + location.hostname + '/';
    $('#left_menu li').each(function (index, element) {
        var href = host_url + $(element).children('a').attr('href');
        if (current_menu.indexOf(href) >= 0) {
            $(element).addClass('hover');
        }
    });
    if ($('#left_menu li[class="hover"]').length > 1) {
        $('#left_menu li[class="hover"]').each(function (index, element) {
            var href = host_url + $(element).children('a').attr('href');
            if (current_menu != href) {
                $(element).removeClass('hover');
            }
        });
    }
});
/*************************************************
描述：审核
作者：Red-East
时间：2013-07-02
*************************************************/
function ISAudit(ID, ISAudit) {
    var _ISAudit;
    if (ISAudit == 0) {

        _ISAudit = 1;
    }
    else {
        _ISAudit = 0;
    }
    DoPosta("Ajax.aspx", ID, "ISAudit", _ISAudit);
}

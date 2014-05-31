<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsInfoma.aspx.cs" Inherits="QDHServer.news.NewsInfo.NewsInfoma" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title></title>
<link href="../Scripts/Themes/Aqua/Css/Ligerui-All.css" rel="stylesheet" type="text/css" />
<link href="../Css/Style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../Scripts/JQuery.js"></script>
<script type="text/javascript" src="../Scripts/Plugins/JQuery.Form.js"></script>
<script type="text/javascript" src="../Scripts/Plugins/JQuery.Validate.js"></script>
<script type="text/javascript" src="../Scripts/Plugins/JQuery.Messages.js"></script>
<script type="text/javascript" src="../Scripts/Common.js"></script>
<script type="text/javascript" src="../Scripts/LigerBuild.js"></script>
<script type="text/javascript" src="../Scripts/Cms.js"></script>
<script type="text/javascript" src="../Scripts/ThickBox/Option.js"></script>
<script type="text/javascript" language="javascript" src="../Common/Calendar/js/jscal2.js"></script>
<script type="text/javascript" language="javascript" src="../Common/Calendar/js/lang/cn.js"></script>
<link type="text/css" href="../Common/Calendar/css/jscal2.css" rel="stylesheet"/>
<link type="text/css" href="../Common/Calendar/css/border-radius.css" rel="stylesheet"/>
<link type="text/css" href="../Common/Calendar/css/steel/steel.css" rel="stylesheet"/>
<script type="text/javascript">
    //表单验证
    $(function () {
        $("#form1").validate({
            invalidHandler: function (e, validator) {
                parent.jsprint("有 " + validator.numberOfInvalids() + " 项填写有误，请检查！", "", "Warning");
            },
            errorPlacement: function (lable, element) {
                //可见元素显示错误提示
                if (element.parents(".tab_con").css('display') != 'none') {
                    element.ligerTip({ content: lable.html(), appendIdTo: lable });
                }
            },
            success: function (lable) {
                lable.ligerHideTip();
            }
        });
    });
</script>
</head>
<body class="mainbody">
<form id="form1" runat="server" onsubmit="return CheckForm();" enctype="multipart/form-data">
    <input type="hidden" name="ID" value="<%=_ID %>" />
    <input type="hidden" name="PageNo" value="<%=_PageNo %>" />
<div class="navigation">当前位置：&nbsp;信息管理 &gt; 信息编辑</div>
<div id="contentTab">
<ul class="tab_nav">
<li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">新闻信息</a></li>
</ul>
<div class="tab_con" style="display:block;">
<table class="form_table">
<tbody>
<tr>
<th style="width:15%">标题：</th>
<td style="width:35%"><asp:TextBox ID="TxtNewsTitle" runat="server" CssClass="txtInput required"  Width="400px"/></td>
<th style="width:15%">栏目：</th>
<td style="width:35%">
    <asp:DropDownList ID="TxtColumnID" runat="server" Width="220"></asp:DropDownList></td>
</tr>
<tr>
<th style="width:15%">图片：</th>
<td style="width:35%"><asp:TextBox ID="TxtNewsLogourl1" runat="server" CssClass="txtInput" Width="220px"/><input type="file" name="TxtNewsLogourl" id="TxtNewsLogourl" width="0" /></td>
<th style="width:15%">地址：</th>
<td style="width:35%"><asp:TextBox ID="TxtAddress" runat="server" CssClass="txtInput" Width="220px"/></td>
</tr>
<tr>
<th style="width:15%">电话：</th>
<td style="width:35%"><asp:TextBox ID="TxtPhone" runat="server" CssClass="txtInput" Width="220px"/></td>
<th style="width:15%">人均消费：</th>
<td style="width:35%"><asp:TextBox ID="TxtPersonPay" runat="server" CssClass="txtInput" Width="220px"/></td>
</tr>
<tr>
<th style="width:15%">推荐指数：</th>
<td style="width:35%"><asp:TextBox ID="TxtRecommend" runat="server" CssClass="txtInput" Width="220px"/>(输入数字，例如： 4.5)</td>
<th style="width:15%">人气指数：</th>
<td style="width:35%"><asp:TextBox ID="TxtPop" runat="server" CssClass="txtInput" Width="220px"/>(输入数字，例如： 4.5)</td>
</tr>
<tr>
<th style="width:15%">综合指数：</th>
<td style="width:35%"><asp:TextBox ID="TxtComplex" runat="server" CssClass="txtInput" Width="220px"/>(输入数字，例如： 4.5)</td>
<th style="width:15%">环境指数：</th>
<td style="width:35%"><asp:TextBox ID="TxtEnvironment" runat="server" CssClass="txtInput" Width="220px"/>(输入数字，例如： 4.5)</td>
</tr>
<tr>
<th style="width:15%">口味指数：</th>
<td style="width:35%"><asp:TextBox ID="TxtTaste" runat="server" CssClass="txtInput" Width="220px"/>(输入数字，例如： 4.5)</td>
<th style="width:15%">设施指数：</th>
<td style="width:35%"><asp:TextBox ID="TxtFacility" runat="server" CssClass="txtInput" Width="220px"/>(输入数字，例如： 4.5)</td>
</tr>
<tr>
<th style="width:15%">服务指数：</th>
<td style="width:35%"><asp:TextBox ID="TxtService" runat="server" CssClass="txtInput" Width="220px"/>(输入数字，例如： 4.5)</td>
<th style="width:15%">卫生指数：</th>
<td style="width:35%"><asp:TextBox ID="TxtHealth" runat="server" CssClass="txtInput" Width="220px"/>(输入数字，例如： 4.5)</td>
</tr>
<tr>
<th style="width:15%">摘要：</th>
<td style="width:85%" colspan="3"><asp:TextBox ID="TxtBz" runat="server" CssClass="txtInput" Width="686px" Height="100px" Rows="5" TextMode="MultiLine" /></td>
</tr>
<tr>
<th style="width:15%">是否有车位：</th>
<td style="width:35%">
    <asp:RadioButtonList ID="TxtPark" runat="server" RepeatDirection="Horizontal">
        <asp:ListItem Selected="True" Value="1">是</asp:ListItem>
        <asp:ListItem Value="0">否</asp:ListItem>
    </asp:RadioButtonList>
</td>
<th style="width:15%">是否有包厢：</th>
<td style="width:35%">
    <asp:RadioButtonList ID="TxtBox" runat="server" RepeatDirection="Horizontal">
        <asp:ListItem Selected="True" Value="1">是</asp:ListItem>
        <asp:ListItem Value="0">否</asp:ListItem>
    </asp:RadioButtonList>
</td>
</tr>
<tr>
<th style="width:15%">公交线路：</th>
<td style="width:35%"><asp:TextBox ID="TxtBus" runat="server" CssClass="txtInput" Width="220px"/></td>
<th style="width:15%">坐标：</th>
<td style="width:35%"><asp:TextBox ID="TxtCoordinate" runat="server" CssClass="txtInput" Width="220px"/></td>
</tr>
<tr>
<th style="width:15%">是否有Wifi：</th>
<td style="width:35%">
    <asp:RadioButtonList ID="TxtWifi" runat="server" RepeatDirection="Horizontal">
        <asp:ListItem Selected="True" Value="1">是</asp:ListItem>
        <asp:ListItem Value="0">否</asp:ListItem>
    </asp:RadioButtonList>
</td>
<th style="width:15%">景区级别：</th>
<td style="width:35%"><asp:TextBox ID="TxtScenicLevel" runat="server" CssClass="txtInput" Width="220px"/></td>
</tr>
<tr>
<th style="width:15%">景区票价说明：</th>
<td style="width:35%"><asp:TextBox ID="TxtScenicDes" runat="server" CssClass="txtInput" Width="220px"/></td>
<th style="width:15%">营业时间：</th>
<td style="width:35%"><asp:TextBox ID="TxtOperateTime" runat="server" CssClass="txtInput" Width="220px"/></td>
</tr>
<tr>
<th style="width:15%">发布时间：</th>
<td style="width:35%">
    <asp:TextBox ID="TxtCreatetime" runat="server" CssClass="txtInput" 
        Width="220px"/>
</td>
<th style="width:15%">网址：</th>
<td style="width:35%"><asp:TextBox ID="TxtHttpurl" runat="server" CssClass="txtInput" Width="220px"/></td>
</tr>
<tr>
<th style="width:15%">内容：</th>
<td colspan="3"><FCKeditorV2:FCKeditor ID="TxtNewsContent" runat="server" Width="680px" Height="350px" ToolbarSet="CMS"></FCKeditorV2:FCKeditor></td>
</tr>
<tr>
<th></th>
<td colspan="3"><asp:Button ID="Button1" runat="server" Text="保 存" CssClass="btnSubmit" onclick="Button1_Click"/>
&nbsp;<input type="button" class="btnSubmit" value="返 回" onclick="javascript:history.go(-1)"/></td>
</tr>
</tbody>
</table>
</div>
</div>
</form>
</body>
</html>
<script type="text/javascript">//<![CDATA[
    Calendar.setup({
        weekNumbers: true,
        inputField: "TxtCreatetime",
        trigger: "TxtCreatetime",
        onSelect: function () { this.hide() },
        showTime: 24,
        dateFormat: "%Y-%m-%d %H:%M:%S"
    });
    //]]></script>
<script type="text/javascript">
    function CheckForm() {
        var rslt = false;
        if ($("select[name='TxtColumnID']").val() == "0") {
            alert("请选择栏目");
            $("select[name='TxtColumnID']").focus();
        }
    }
</script>

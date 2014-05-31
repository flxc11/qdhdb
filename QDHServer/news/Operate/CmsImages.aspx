<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CmsImages.aspx.cs" Inherits="QDHServer.news.Operate.CmsImages" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Css/Global.css" rel="stylesheet" type="text/css" />
    <link href="../Css/Style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../Common/jquery.js"></script>
    <script src="../Common/Jquery.MaskInput.js" type="text/javascript"></script>
    <script src="CMS.js" type="text/javascript"></script>
    <script src="../Common/Option.js" type="text/javascript"></script>
    <script src="../Common/ThickBox.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".list_tb tr").mouseover(function () {
                $(this).addClass("over");
            }).mouseout(function () {
                $(this).removeClass("over");
            })
            $(".list_tb tr:even").addClass("alt");
            $("#TimeStart").mask("9999-99-99", { completed: function () { CheckDate($(this).get(0), $(this).val(), 'date', false); } });
            $("#TimeEnd").mask("9999-99-99", { completed: function () { CheckDate($(this).get(0), $(this).val(), 'date', false); } });
        });
        function SelectFile(imagepath) {
            setTBConfig("retVal", imagepath);
            self.returnValue = imagepath;
            removeTB();
        }
        function CheckForm() {
            if ($("#ImagesURL").val() == "") {
                alert("图片地址不能为空。");
                $("#ImagesURL").focus();
                return false;
            }
            return true;
        }
</script>
<style>
#TB_title {
background-color: #5B69A6;
height: 27px;
cursor: move;
}
</style>
</head>
<body style="background:#f7fafb;" scroll="no">
<form id="form1" runat="server" onsubmit="return CheckForm();">
<table width="650" border="0" cellspacing="0" cellpadding="0" align="center">
<tr>
<td style="height:10px"></td>
</tr>
</table>
<table width="650" border="0" cellspacing="0" cellpadding="0" align="center">
<tr>
<td class="cell_group"><img src="../Images/ListIconTitle.jpg" />&nbsp;您的位置：信息管理 > 选择图片</td>
</tr>
</table>
<table width="650" border="0" cellspacing="0" cellpadding="0" align="center">
<tr>
<td style="height:10px"></td>
</tr>
</table>
<table width="650" border="0" cellspacing="0" cellpadding="0" align="center">
<tr>
<td width="400" valign="top">
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="list_tb">
<tr>
<th width="75%">&nbsp;图片名称<a href="CmsImagesAdd.aspx">[+]</a></th>
<th width="25%">上传时间</th>
</tr>
<asp:Repeater ID="Repeater1" runat="server">
<ItemTemplate>
<tr>
<td>&nbsp;<a href="###" onclick="CmsViewImages('<%#Eval("FilePath").ToString().Replace("{#InstallDir}",CNVP.Config.UIConfig.InstallDir)%>','<%#Eval("FileExtension")%>')"><%#GetFileName(Eval("FileName").ToString())%></a></td>
<td><%#Convert.ToDateTime(Eval("PostTime")).ToString("yyyy-MM-dd")%></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</table>
<table width="400" border="0" cellspacing="0" cellpadding="0" align="center">
<tr>
<td style="height:5px"></td>
</tr>
</table>
<table width="400" border="0" cellspacing="0" cellpadding="0" align="center">
<tr>
<td class="cell_center"><asp:Literal ID="LitPager" runat="server"></asp:Literal></td>
</tr>
</table>
</td>
<td width="250" valign="top">
<table width="240" border="0" cellspacing="0" cellpadding="0" align="center">
<tr>
<td colspan="2" align="center"><img id="ImagesView" src="../Images/Blank.jpg" width="200" height="150"/></td>
</tr>
<tr>
<td height="10" colspan="2"></td>
</tr>
<tr>
<td width="80" height="25" align="right">图片地址</td>
<td width="120"><input type="text" id="ImagesURL" name="ImagesURL" style="width:120px;border:solid 1px #CCCCCC" readonly="readonly"/><input type="hidden" id="HidFileExtension" name="HidFileExtension" value=""/></td>
</tr>
<tr>
<td width="80" height="25" align="right">缩略方式</td>
<td width="120"><select id="mode" name="mode" style="width:124px;"><option value="HW">指定高宽缩放</option>
<option value="W">指定宽，高按比例</option>
<option value="H">指定高，宽按比例</option>
<option value="Cut">指定高宽裁减</option></select></td>
</tr>
<tr>
<td width="80" height="25" align="right">图片大小</td>
<td width="120"><input type="text" id="width" name="width" style="width:49px;border:solid 1px #CCCCCC" value="100" maxlength="5"/>×<input type="text" id="height" name="height" style="width:49px;border:solid 1px #CCCCCC" value="100" maxlength="5"/></td>
</tr>
<tr>
<td height="10" colspan="2"></td>
</tr>
<tr>
<td colspan="2" align="center"><input type="button" id="btnLink" name="btnLink" value="上传图片" style="width:60px;height:25px" class="column_operatebtn" onclick="window.location.href = 'CmsImagesAdd.aspx'"/> <asp:Button ID="Button1" runat="server" Text="插入原图" class="column_operatebtn" Width="60px" Height="25px" onclick="Button1_Click"/> <asp:Button ID="Button2" runat="server" Text="插入缩略图" class="column_operatebtn" Width="70px" Height="25px" onclick="Button2_Click"/></td>
</tr>
</table>
</td>
</tr>
</table>
</form>
</body>
</html>

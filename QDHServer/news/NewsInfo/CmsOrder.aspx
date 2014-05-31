<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CmsOrder.aspx.cs" Inherits="CNVP.CMS.Admin.JCms.CmsOrder" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title></title>
<link href="../Images/Global.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript" src="../Scripts/Jquery.js"></script>
<script language="javascript" type="text/javascript" src="../Scripts/Option.js"></script>
<link href="Column.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../Scripts/CMS.js"></script>
<script type="text/javascript">
function CheckForm()
{
    var Str = GetSelectValue(document.form1.LstNewsList);
	$("#HidSelectValue").attr('value', Str);
	return true;
}
</script>
</head>
<body style="background:#f7fafb;overflow-x:hidden;">
<form id="form1" runat="server" onsubmit="return CheckForm();">
<input type="hidden" value="<%=_MaxOrderID%>" id="HidOrderID" name="HidOrderID">
<input type="hidden" value="" id="HidSelectValue" name="HidSelectValue">
<table width="480" border="0" cellspacing="0" cellpadding="0" align="center" class="unit_table">
<tr>
<td width="400" valign="top" class="cell_left">
<fieldset style="width:390px;height:260px;border:solid 1px #CCCCCC">
<legend>栏目排序操作</legend>
<table width="380" border="0" cellspacing="0" cellpadding="0" align="center">
<tr>
<td width="100" height="35">栏目信息检索从</td>
<td width="60" align="center"><input type="text" id="TxtMin" name="TxtMin" onkeydown="return funKeyDown(event)" class="column_popinput" maxlength="4" value="<%=_FirstRow%>"/></td>
<td width="50" align="center">到</td>
<td width="80"><input type="text" id="TxtMax" name="TxtMax" onkeydown="return funKeyDown(event)" class="column_popinput" maxlength="4" value="<%=_EndRow%>"/></td>
<td width="100"><input type="button" id="button1" name="button1" value="检 索" class="column_button" onclick="CmsSearch('<%=_ParentID %>')"/></td>
</tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
<tr>
<td><asp:ListBox ID="LstNewsList" runat="server" CssClass="pop_select" SelectionMode="Multiple" Width="100%" Height="200px"></asp:ListBox></td>
</tr>
</table>
</fieldset>
</td>
<td width="80" align="center" valign="top" class="cell_left">
<fieldset style="width:80px;height:260px;border:solid 1px #CCCCCC">
<legend>顺序调整</legend>
<table align="center" height="260" valign="middle">
<tr>
<td align="center" valign="middle">
<input type="hidden" name="maxnum" value="12">
<input type="hidden" name="minnum" value="1">
<input onclick="moveUp(this.form.LstNewsList,this.form.jumpto.value)" type=button value=" ∧ " class="column_movebutton">
<br>到<br><input type="text" class="column_popinput" style="width:30px; text-align:center" name="jumpto" size="2" title="1～12，默认移动一格" onkeyup="value=value.replace(/[^\d]/g,'');checkInput(this.form)"><br>位<br /><br />
<input onclick="moveDown(this.form.LstNewsList, this.form.jumpto.value)" type=button value=" ∨ " class="column_movebutton">
</td></tr></table>
</fieldset>
</td>
</tr>
<tr>
<td colspan="2" class="cell_right"><asp:Button ID="Button1" runat="server" 
        Text="保  存" CssClass="pop_button" onclick="Button1_Click"/> <input id="CloseBtn" type="button" value="关  闭" class="pop_button" onclick="top.removeTB();"/></td>
</tr>
</table>
</form>
</body>
</html>
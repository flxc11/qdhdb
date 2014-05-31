<%@ Page Language="c#" Trace="false" Inherits="FredCK.FCKeditorV2.FileBrowser.Browser" AutoEventWireup="false" %>
<%@ Register Src="config.ascx" TagName="Config" TagPrefix="FCKeditor" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<script language="javascript" type="text/javascript">
function GetValue(FilePath,FileName)
{
    parent.SetUrl(FilePath,FileName);
}
</script>
<style type="text/css">
body{margin:0px; padding:0px; font-size:12px;background:#f1f1e3; color:#737357}
table,td{margin:0px; padding:0px; font-size:12px;}
a{text-decoration:none;color:#737357}
</style>
</head>
<body>
<form id="form1" runat="server">
<FCKeditor:Config id="Config" runat="server"></FCKeditor:Config>
<asp:Panel ID="Panel1" runat="server" Visible="false">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
<tr bgcolor="#e3e3c7">
<td width="70%" height="25">&nbsp;&nbsp;文件名称</td>
<td width="30%">上传时间</td>
</tr>
<asp:Repeater ID="Repeater1" runat="server">
<ItemTemplate>
<tr>
<td height="20">&nbsp;&nbsp;<a href="#" onclick="GetValue('<%#Eval("FilePath").ToString().Replace("{#InstallDir}","/")%>','<%#Eval("FileName")%>');"><%#Eval("FileName")%></a></td>
<td><%#Convert.ToDateTime(Eval("PostTime")).ToString("yyyy-MM-dd HH:mm:ss") %></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
<tr>
<td style="height:5px"></td>
</tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
<tr>
<td style="text-align:center;background:#e3e3c7" height="25"><asp:Literal ID="LitPager" runat="server"></asp:Literal></td>
</tr>
</table>
</asp:Panel>
</form>
</body>
</html>
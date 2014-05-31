<%@ Page Language="c#" Trace="false" Inherits="FredCK.FCKeditorV2.FileBrowser.BigUploader" AutoEventWireup="false" %>
<%@ Register assembly="CNVP.Framework" namespace="CNVP.Framework.UploadFile" tagprefix="cc1" %>
<%@ Register Src="config.ascx" TagName="Config" TagPrefix="FCKeditor" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
<title></title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<script language="javascript" type="text/javascript">
function GetValue(FilePath,FileName)
{
	parent.SetUrl(FilePath,FileName);
}
</script>
<style type="text/css">
body{margin:0px; padding:0px;font-size:11px;background:#f1f1e3;}
table,td{margin:0px; padding:0px;font-size:11px;}
form{margin:0px; padding:0px}
</style>
</head>
<body scroll="no" style="OVERFLOW: hidden">
<form id="form1" runat="server">
<FCKeditor:Config id="Config" runat="server"></FCKeditor:Config>
<div>
附件名称<br />
<asp:TextBox ID="TxtFileName" runat="server" Width="100%"></asp:TextBox>
选择附件<br />
<cc1:Upload ID="Upload1" runat="server" ShowLoad="Yes" FileReName="false"></cc1:Upload>
<br />
<asp:Button ID="Button1" runat="server" Text="发送到服务器上" onclick="Button1_Click"/>
</div>
</form>
</body>
</html>
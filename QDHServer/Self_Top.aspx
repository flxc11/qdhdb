<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Self_Top.aspx.cs" Inherits="QDHServer.Self_Top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<style>
body{
	margin: 0px;
}
#rightBtn{
    position:absolute;
}
* {margin:0; padding:0; list-style:none; } 
.wrapper {width: 100%; } 
.left {width:358px; background:#fcc; position:absolute; left:0 ;z-index:1; } 
.right {width:100%; background:#ccc; position:absolute; left:0; background:url(images/top_bg_1.gif);} 
.content {margin-left:358px;float:right; margin: 10px 0} 
</style>
</head>
<script language=javascript type="text/javascript" >
    function exitManage() {
        __doPostBack('imgExit', '');
    }
    function homeManage() {
        window.parent.mainFrame.location.href = "Default.aspx";
    }
</script>

<body background="images/top_bg_1.gif">
<form id="form1" runat="server" >
<div class="wrapper">
<div class="left" ><img src="images/logo.gif"  /></div>
 
<div class="right" >
    <div class="content" >
       <table width="100%" border="0">
        <tr>
            <td width="707" height="50%" rowspan="2" align="right" valign="top"> &nbsp;<asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton></td>
            <td align="right" width="62" valign="top"><div align="center"><a href="#" style="border:none"><img border="0" src="images/homepage.gif" onClick="homeManage()"/></a></div></td>
            <td align="right" width="60" valign="top"><div align="center"><a href="#" style="border:none"><img border="0" src="images/refresh.gif" onClick="window.parent.mainFrame.location.reload()" /></a></div></td>
            <td align="right" width="56" valign="top"><div align="center"><a href="#" style="border:none"><img border="0" src="images/help.gif" /></a></div></td>
            <td align="right" width="58" valign="top"><div align="center">
            <asp:ImageButton ID="imgExit" runat="server" ImageUrl="~/images/logout.gif" OnClick="imgExit_Click" /></div></td>
            <td width="40" rowspan="2" align="right" valign="top">&nbsp;</td>
        </tr>
        <tr>
            <td align="right" valign="top"><div align="center"><a href="#" style="border:none; text-decoration: none" onClick="onClick=homeManage()"><font color="#FFFFFF" size="-1" >首页</font></a></div></td>
            <td align="right" valign="top"><div align="center"><a href="#" style="border:none; text-decoration: none" onClick="window.parent.mainFrame.location.reload()"><font color="#FFFFFF" size="-1" >刷新</font></a></div></td>
            <td align="right" valign="top"><div align="center"><a href="#" style="border:none; text-decoration: none"><font color="#FFFFFF" size="-1" >帮助</font></a></div></td>
            <td align="right" valign="top"><div align="center"><a href="#" style="border:none; text-decoration: none" onClick="exitManage()"><font color="#FFFFFF" size="-1" >退出</font></a></div></td>
        </tr>
       </table>
    </div>
</div>
</div>
</form>
</body>
</html>

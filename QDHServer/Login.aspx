<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="QDHServer.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <script language="javascript" src="js/jquery-1.4.4.min.js"></script>
    <script language="javascript" src="js/jquery.easyui.min.1.2.2.js"></script>
    <link rel="stylesheet" type="text/css" href="js/themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="js/themes/icon.css">
    <style type="text/css">
        a
        {
            color: #008EE3;
        }
        a:link
        {
            text-decoration: none;
            color: #008EE3;
        }
        A:visited
        {
            text-decoration: none;
            color: #666666;
        }
        A:active
        {
            text-decoration: underline;
        }
        A:hover
        {
            text-decoration: underline;
            color: #0066CC;
        }
        A.b:link
        {
            text-decoration: none;
            font-size: 12px;
            font-family: "Helvetica,微软雅黑,宋体";
            color: #FFFFFF;
        }
        A.b:visited
        {
            text-decoration: none;
            font-size: 12px;
            font-family: "Helvetica,微软雅黑,宋体";
            color: #FFFFFF;
        }
        A.b:active
        {
            text-decoration: underline;
            color: #FF0000;
        }
        A.b:hover
        {
            text-decoration: underline;
            color: #ffffff;
        }
        
        .table1
        {
            border: 1px solid #CCCCCC;
        }
        .font
        {
            font-size: 12px;
            text-decoration: none;
            color: #999999;
            line-height: 20px;
        }
        .input
        {
            font-size: 12px;
            color: #999999;
            text-decoration: none;
            border: 0px none #999999;
        }
        
        td
        {
            font-size: 12px;
            color: #007AB5;
        }
        form
        {
            margin: 1px;
            padding: 1px;
        }
        input
        {
            border-style: none;
            border-color: inherit;
            border-width: 0px;
            color: #007AB5;
        }
        .unnamed1
        {
            border: thin none #FFFFFF;
        }
        select
        {
            border: 1px solid #cccccc;
            height: 18px;
            color: #666666;
        }
        body
        {
            background-repeat: no-repeat;
            background-color: #9CDCF9;
            background-position: 0px 0px;
        }
        .tablelinenotop
        {
            border-top: 0px solid #CCCCCC;
            border-right: 1px solid #CCCCCC;
            border-bottom: 0px solid #CCCCCC;
            border-left: 1px solid #CCCCCC;
        }
        .tablelinenotopdown
        {
            border-top: 1px solid #eeeeee;
            border-right: 1px solid #eeeeee;
            border-bottom: 1px solid #eeeeee;
            border-left: 1px solid #eeeeee;
        }
        
        .textbox
        {
            background: url(../images/login_6.gif) repeat-x;
            border: solid 1px #27B3FE;
            height: 20px;
            background-color: #FFFFFF;
        }
        
        .btn_login
        {
            background: url(../images/login_5.gif) no-repeat;
        }
        .style1
        {
            height: 29px;
        }
    </style>
    <script type="text/javascript">

        //改变验证码
        function ChangeCode() {
            var date = new Date();
            var myImg = document.getElementById('<%=ImageCheck.ClientID%>');
            myImg.src = "/ValidateCode.aspx?GUID=-1&flag=" + date.getMilliseconds()
            return false;
        }

        function alert() {
            $.messager.alert("系统提示", "用户信息不存在", "error");
        }

        function alertError() {
            $.messager.alert("系统提示", "系统异常,请稍后再试！！", "error");
        }
    </script>
</head>
<body style="background-color: #9CDCF9">
    <table width="681" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 120px">
        <tr>
            <td width="353" height="259" align="center" valign="bottom" background="../images/login_1.gif">
                <table width="90%" border="0" cellspacing="3" cellpadding="0">
                    <tr>
                        <td align="right" valign="bottom" style="color: #05B8E4">
                            Power by qiandaohu Copyright 2014
                        </td>
                    </tr>
                </table>
            </td>
            <td width="195" background="../images/login_2.gif">
                <table height="106" border="0" align="center" cellpadding="2" cellspacing="0" 
                    style="width: 198px">
                    <form id="Form1" method="post" runat="server" name="NETSJ_Login">
                    <tr>
                        <td height="50" colspan="2" align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="60" height="30" align="left">
                            登陆名称
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserName" CssClass="textbox" Width="120px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="30" align="left">
                            登陆密码
                        </td>
                        <td>
                            <asp:TextBox ID="txtPwd" CssClass="textbox" Width="120px" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="30">
                            验 证 码
                        </td>
                        <td>
                            <asp:TextBox ID="txtCode" CssClass="textbox" Width="60px" runat="server"></asp:TextBox>
                            <a id="A2" href="" onclick="ChangeCode();return false;"><a id="A1" href="" onclick="ChangeCode();return false;">
                                <asp:Image ID="ImageCheck" runat="server" ImageUrl="/ValidateCode.aspx?GUID=GUID"
                                    ImageAlign="AbsMiddle" ToolTip="看不清，换一个"></asp:Image></a>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center" class="style1">
                            <asp:Panel ID="pnl_prompt" Visible="false" runat="server" ForeColor="Red">
                                <img src="../images/tip.gif" height="16" style="width: 16px">
                                <asp:Label ID="lbl_prompt" runat="server" Text="请勿非法登陆！"></asp:Label>
                            </asp:Panel>
                        </td>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btn_login" CssClass="btn_login" runat="server" Text=" 登  录 " Height="24px"
                                    Width="74px" OnClick="btn_login_Click" />
                                <asp:Button ID="btnClear" CssClass="btn_login" runat="server" Text=" 清  空 " Height="24px"
                                    Width="74px" onclick="btnClear_Click"  />
                            </td>
                            <tr>
                                <td height="5" colspan="2">
                                </td>
                    </form>
                </table>
            </td>
            <td width="133" background="../images/login_3.gif">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td height="161" colspan="3" background="../images/login_4.gif">
            </td>
        </tr>
    </table>
</body>
</html>

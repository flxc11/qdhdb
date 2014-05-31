<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemLog.aspx.cs" Inherits="QDHServer.ad.SystemLog.SystemLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript" src="My97DatePicker/WdatePicker.js"
        charset="gb2312"></script>
    <script language="javascript" src="../../js/jquery-1.4.4.min.js"></script>
    <script language="javascript" src="../../js/jquery.easyui.min.1.2.2.js"></script>
    <link rel="stylesheet" type="text/css" href="../../js/themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../../js/themes/icon.css">
    <style type="text/css">
        #txtTime
        {
            width: 131px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="系统日志" Font-Bold="true"></asp:Label>
    &gt;<asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Red" Style="text-decoration: none">用户操作日志</asp:LinkButton>
    &nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label2" runat="server" Text="查询:" Font-Bold="true"></asp:Label>
    <asp:Label ID="Label5" runat="server" Text="时间:"></asp:Label>
    <input id="txtTime" type="text" onclick="WdatePicker()" runat="server" class="Wdate"
        onfocus="WdatePicker({skin:'whyGreen',dateFmt:'yyyyMMdd'})" />
    &nbsp;
    <asp:LinkButton ID="btnSearch" class="easyui-linkbutton" icon="icon-search" runat="server"
        OnClick="btnSearch_Click" Style="vertical-align: top">搜索</asp:LinkButton>
    <asp:LinkButton ID="btnReturn" class="easyui-linkbutton" icon="icon-back" runat="server"
        OnClick="btnReturn_Click" Style="vertical-align: top">返回</asp:LinkButton>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="lblLog" runat="server" Text="Label" Visible="false" ForeColor="Red"
        align="center"></asp:Label>
    <br />
    <asp:GridView ID="dvLog" runat="server" Width="574px" RowStyle-HorizontalAlign="Center">
    </asp:GridView>
    </form>
</body>
</html>

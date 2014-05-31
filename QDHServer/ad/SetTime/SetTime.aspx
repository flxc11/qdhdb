<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetTime.aspx.cs" Inherits="QDHServer.ad.SetTime.SetTime" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" src="../../js/jquery-1.4.4.min.js"></script>
    <script language="javascript" src="../../js/jquery.easyui.min.1.2.2.js"></script>
    <link rel="stylesheet" type="text/css" href="../../js/themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../../js/themes/icon.css">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:LinkButton ID="lnkPowerOnTime" class="easyui-linkbutton" icon="icon-setOpenTime"
            runat="server" OnClick="lnkPowerOnTime_Click">设置开机时间</asp:LinkButton>
        <asp:LinkButton ID="lnkCloseTime" class="easyui-linkbutton" icon="icon-setCloseTime"
            runat="server" OnClick="lnkCloseTime_Click">设置关机时间</asp:LinkButton>
        <asp:LinkButton ID="lnkRestartTime" class="easyui-linkbutton" icon="icon-ReStartTime"
            runat="server" OnClick="lnkRestartTime_Click">重启终端</asp:LinkButton>
    <%--    <asp:LinkButton ID="lnkOpenCloseList" class="easyui-linkbutton" icon="icon-openClose"
            runat="server" onclick="lnkOpenCloseList_Click">终端开机关机时间列表</asp:LinkButton>--%>
    </div>
    </form>
</body>
</html>

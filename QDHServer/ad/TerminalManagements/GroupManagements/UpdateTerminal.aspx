<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateTerminal.aspx.cs"
    Inherits="QDHServer.ad.TerminalManagements.GroupManagements.UpdateTerminal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>千岛湖广告发布系统</title>
    <script language="javascript" type="text/javascript" src="../../js/jquery-1.4.4.min.js"></script>
    <script language="javascript" type="text/javascript" src="../../js/jquery.easyui.min.1.2.2.js"></script>
    <link rel="stylesheet" type="text/css" href="../../js/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../js/themes/icon.css" />
    <style type="text/css">
        .style2
        {
            height: 13px;
        }
        .style3
        {
            width: 277px;
        }
        .style4
        {
            width: 277px;
            height: 13px;
        }
    </style>
    <script type="text/javascript">
        function alertWarningNet() {
            $.messager.alert("系统提示", "网络异常,请稍后再试！！", "error");
        }

        function alertUpdateSuccess() {
            $.messager.alert("系统提示", "更新成功！！", "info");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left: 0px; vertical-align: top">
        <table style="width: 100%; margin-left: 0px">
            <tr>
                <td colspan="4">
                    <div style="background-color: lightBlue; height: 25px;">
                        <img alt="" src="../../images/LeftTile.gif" style="height: 17px" />
                        <asp:Label ID="Label1" runat="server" Text="分组管理>修改" Style="font-size: small"></asp:Label>
                        <asp:ImageButton ID="IbtnReturn" runat="server" ImageUrl="~/ad/images/return.png"
                            align="right" Height="23px" OnClick="IbtnReturn_Click" />
                    </div>
                </td>
            </tr>
            <tr>
                <td align="right" class="style3">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    <asp:Label ID="Label2" runat="server" Text="组名:" Style="font-size: small"></asp:Label>
                </td>
                <td align="left" class="style2">
                    <asp:TextBox ID="txtGroupName" runat="server"></asp:TextBox>
                </td>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" class="style4">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label4" runat="server" Text="*"
                        ForeColor="Red"></asp:Label>
                    <asp:Label ID="Label5" runat="server" Text="地区:" Style="font-size: small"></asp:Label>
                </td>
                <td align="left" class="style2">
                </td>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" class="style3">
                    &nbsp;
                </td>
                <td align="left" class="style2">
                    <asp:ImageButton ID="IbtnSave" runat="server" OnClick="IbtnSave_Click" Height="27px"
                        ImageUrl="~/ad/images/save.png" Width="59px" />
                </td>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style2">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

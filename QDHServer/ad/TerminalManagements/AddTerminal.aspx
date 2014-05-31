<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddTerminal.aspx.cs" Inherits="QDHServer.ad.TerminalManagement.AddTerminal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="margin-left: 0px; vertical-align: top">
            <table style="width: 100%; margin-left: 0px">
                <tr>
                    <td colspan="4">
                        <div style="background-color: lightBlue; height: 25px;">
                            <img alt="" src="../images/leftTile.gif" style="height: 17px" />
                            <asp:Label ID="Label1" runat="server" Style="font-size: small" Text="终端管理>终端管理>添加"></asp:Label>
                            <asp:ImageButton ID="IbtnReturn" runat="server" Height="24px" ImageUrl="~/ad/images/return.png"
                                align="right" OnClick="IbtnReturn_Click" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td align="left">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" class="style1" colspan="4">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>

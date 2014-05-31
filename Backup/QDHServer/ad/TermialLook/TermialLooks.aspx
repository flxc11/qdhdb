<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TermialLooks.aspx.cs" Inherits="QDHServer.ad.TermialLooks" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" src="../../js/jquery-1.4.4.min.js"></script>
    <script language="javascript" src="../../js/jquery.easyui.min.1.2.2.js"></script>
    <link rel="stylesheet" type="text/css" href="../../js/themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../../js/themes/icon.css">
    <script type="text/javascript">
        function alertErrorgDeletePic() {
            $.messager.alert("系统提示", "网络异常,请稍后再试！！", "error");
        }

        function alertWarningInputTerminal() {
            $.messager.alert("系统提示", "请选择数据！！", "warning");
        }

        function alertWarningSuccess() {
            $.messager.alert("系统提示", "审核成功！！", "info");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-size: small">
        <asp:Label ID="Label1" runat="server" Text="未审核终端" ForeColor="Red" Font-Bold="true"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;搜索共计:<asp:Label ID="lblTermial" runat="server"
            Text="Label"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:ImageButton ID="IbtnLook" runat="server" OnClick="IbtnLook_Click" Height="22px"
            ImageUrl="~/ad/ad/look.jpg" Width="57px" />
        &nbsp;<asp:ImageButton ID="IbtnReturn" runat="server" Height="22px" ImageUrl="~/ad/ad/return.jpg"
            OnClick="IbtnReturn_Click" Width="57px" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <asp:DataList ID="dlTerminal" runat="server" Width="26%" Font-Size="Small" BorderColor="Tan"
            BorderWidth="0px" CellPadding="2" ForeColor="Black" Font-Bold="False" Font-Italic="False"
            Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" />
            <FooterStyle BackColor="Tan" />
            <HeaderStyle BackColor="Gray" Font-Bold="True" />
            <HeaderTemplate>
                <table width="500px" style="font-size: small">
                    <tr>
                        <td width="8%" align="center" bgcolor="#E8F0F7" style="font-size: small">
                        </td>
                        <td width="15%" align="center" bgcolor="#E8F0F7" style="font-size: small">
                            终端编号
                        </td>
                        <td width="10%" align="center" bgcolor="#E8F0F7" style="font-size: small">
                            分辨率
                        </td>
                    </tr>
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <table width="500px" style="font-size: small">
                    <tr>
                        <td bgcolor="#FFFFFF" width="8%" align="center" style="font-size: small">
                            <asp:CheckBox ID="ckbTermial" runat="server"></asp:CheckBox>
                            <asp:HiddenField ID="HidID" runat="server" Value='<%#Eval("terminalid") %>' />
                        </td>
                        <td align="center" bgcolor="#FFFFFF" width="15%" style="font-size: small">
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"DetailTermial.aspx?termialId="+Eval("terminalid") %>'><%#Eval("terminalid") %></asp:HyperLink>
                        </td>
                        <td align="center" bgcolor="#FFFFFF" width="10%" style="font-size: small">
                            <%#Eval("picsize")%>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" />
        </asp:DataList>
        <hr style="width: 500px" align="left" />
        <asp:Label ID="Label2" runat="server" Text="已发布终端(新增)" Font-Bold="true" ForeColor="Red"></asp:Label>
        &nbsp;搜索共计:<asp:Label ID="lblTerminalAdd" runat="server" Text="Label"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton
            ID="IbtnLookAdd" runat="server" OnClick="IbtnLookAdd_Click" Height="22px" ImageUrl="~/ad/ad/look.jpg"
            Width="57px" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DataList ID="dlTerminalAdd" runat="server" Width="60%" Font-Size="Small" BorderColor="Tan"
            BorderWidth="0px" CellPadding="2" ForeColor="Black" Font-Bold="False" Font-Italic="False"
            Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" />
            <FooterStyle BackColor="Tan" />
            <HeaderStyle BackColor="Gray" Font-Bold="True" />
            <HeaderTemplate>
                <table width="500px" style="font-size: small">
                    <tr>
                        <td width="8%" align="center" bgcolor="#E8F0F7" style="font-size: small">
                        </td>
                        <td width="15%" align="center" bgcolor="#E8F0F7" style="font-size: small">
                            终端编号
                        </td>
                        <td width="10%" align="center" bgcolor="#E8F0F7" style="font-size: small">
                            分辨率
                        </td>
                    </tr>
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <table width="500px" style="font-size: small">
                    <tr>
                        <td bgcolor="#FFFFFF" width="8%" align="center" style="font-size: small">
                            <asp:CheckBox ID="ckbTermialAdd" runat="server"></asp:CheckBox>
                            <asp:HiddenField ID="HidIDAdd" runat="server" Value='<%#Eval("terminalid") %>' />
                        </td>
                        <td align="center" bgcolor="#FFFFFF" width="15%" style="font-size: small">
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"DetailTermial.aspx?termialId="+Eval("terminalid") %>'><%#Eval("terminalid") %></asp:HyperLink>
                        </td>
                        <td align="center" bgcolor="#FFFFFF" width="10%" style="font-size: small">
                            <%#Eval("picsize")%>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" />
        </asp:DataList>
    </div>
    </form>
</body>
</html>

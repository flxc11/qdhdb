<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetStartCloseTime.aspx.cs"
    Inherits="QDHServer.ad.SetTime.SetStartCloseTime" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" src="../../../js/jquery-1.4.4.min.js"></script>
    <script language="javascript" src="../../../js/jquery.easyui.min.1.2.2.js"></script>
    <link rel="stylesheet" type="text/css" href="../../../js/themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../../../js/themes/icon.css">
    <script type="text/javascript">

        //终端号全选
        function selectAllTermial(boxs) {
            var table = document.getElementById("dlTerminal");   //获得datalist
            var checkes = table.getElementsByTagName("input");
            for (var i = 0; i < checkes.length; i++) {
                if (boxs.checked == true) {
                    if (checkes[i].type == "checkbox") {
                        checkes[i].checked = true;
                    }
                }
                else {
                    if (checkes[i].type == "checkbox") {
                        checkes[i].checked = false;
                    }
                }
            }
        }

        function alertWarningLastPic() {
            $.messager.alert("系统提示", "请选择终端编号！！", "warning");
        }

        function alertError() {
            $.messager.alert("系统提示", "请输入正确的终端编号！！", "error");
        }

        function waiting() {
            $.messager.alert("系统提示", "重启成功！！", "info");
        }

        function alertAgainError() {
            $.messager.alert("系统提示", "网络异常,,请稍后再试！！", "error");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="tb">
        <table style="width: 100%;">
            <tr>
                <td class="style1" align="right">
                    <asp:Label ID="Label2" runat="server" Text="终端编号:" ForeColor="DeepSkyBlue"></asp:Label>
                </td>
                <td>
                    <%--<asp:TextBox ID="txtTerminal" runat="server" Height="19px" Width="174px"></asp:TextBox>--%>
                    <asp:DataList ID="dlTerminal" runat="server" Width="20%" DataKeyField="TERMINALID"
                        Font-Size="Small" BackColor="#E8F0F7" BorderWidth="1px" CellPadding="2" ForeColor="DeepSkyBlue">
                        <HeaderStyle BackColor="#E8F0F7" Font-Bold="True" />
                        <HeaderTemplate>
                            <table width="200px" style="font-size: small">
                                <tr>
                                    <td width="35%" align="center" style="font-size: small">
                                        终端编号
                                    </td>
                                    <td width="35%" align="center" style="font-size: small">
                                        关机时间
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table width="200px" style="font-size: small">
                                <tr>
                                    <td align="center" bgcolor="#E8F0F7" width="35%" style="font-size: small">
                                        <%#Eval("TERMINALID")%>
                                    </td>
                                    <td align="center" bgcolor="#E8F0F7" width="35%" style="font-size: small">
                                        <%#Eval("CLOSETIME")%>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2">
                </td>
                <td class="style3">
                    <asp:LinkButton ID="lnkReturn" class="easyui-linkbutton" icon="icon-back" runat="server"
                        OnClick="lnkReturn_Click">返回</asp:LinkButton>
                    <%--         <asp:HyperLink ID="lkChakan" class="easyui-linkbutton" icon="icon-search" runat="server"
                        Style="text-decoration: none">查看</asp:HyperLink>--%>
                    <%--   <a href="#" class="easyui-linkbutton" icon="icon-add" runat="server" >add</a> &nbsp;--%>
                </td>
                <td class="style3">
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style3">
                    &nbsp;
                </td>
                <td class="style3">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

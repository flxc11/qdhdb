<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReStartTime.aspx.cs" Inherits="QDHServer.ad.SetTime.ReStartTime" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" src="../../../js/jquery-1.4.4.min.js"></script>
    <script language="javascript" src="../../../js/jquery.easyui.min.1.2.2.js"></script>
    <link rel="stylesheet" type="text/css" href="../../../js/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../../js/themes/icon.css" />
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

        function alertInputTerminal() {
            $.messager.alert("系统提示", "请输入终端编号！！", "warning");
        }

        function alertError() {
            $.messager.alert("系统提示", "请输入正确的终端编号！！", "error");
        }

        function waiting() {
            $.messager.alert("系统提示", "重启成功！！", "info");
        }

        function alertAgainError() {
            $.messager.alert("系统提示", "网络异常,请稍后再试！！", "error");
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 165px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="tb">
        <table style="width: 100%;">
            <tr>
                <td class="style1" align="right">
                    <asp:Label ID="Label2" runat="server" Text="终端编号:" ForeColor="DeepSkyBlue"></asp:Label>
                    &nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtTerminal" runat="server" Height="16px" Width="125px"></asp:TextBox>
                    <asp:LinkButton ID="lnkShowTerminal" class="easyui-linkbutton" icon="icon-search"
                        runat="server" OnClick="lnkShowTerminal_Click" Style="vertical-align: top">搜索</asp:LinkButton>
                    <asp:LinkButton ID="lnkReturn" class="easyui-linkbutton" icon="icon-back" runat="server"
                        OnClick="lnkReturn_Click"  Style="vertical-align: top">返回</asp:LinkButton>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1" align="right">
                    &nbsp;
                </td>
                <td>
                    <%--<asp:TextBox ID="txtTerminal" runat="server" Height="19px" Width="174px"></asp:TextBox>--%>
                    <asp:DataList ID="dlTerminal" runat="server" Width="20%" DataKeyField="TERMINALID"
                        Font-Size="Small" BackColor="#E8F0F7" BorderWidth="1px" CellPadding="2" ForeColor="DeepSkyBlue">
                        <HeaderStyle BackColor="#E8F0F7" Font-Bold="True" />
                        <HeaderTemplate>
                            <table width="300px" style="font-size: small">
                                <tr>
                                    <td width="8%" align="center" style="font-size: small">
                                        <asp:CheckBox ID="ckbTerminal" runat="server" onclick="selectAllTermial(this)" AutoPostBack="False">
                                        </asp:CheckBox>
                                    </td>
                                    <td width="35%" align="center" style="font-size: small">
                                        终端编号
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table width="300px" style="font-size: small">
                                <tr>
                                    <td bgcolor="#E8F0F7" width="8%" align="center" style="font-size: small">
                                        <asp:CheckBox ID="ckbTerminal" runat="server"></asp:CheckBox>
                                        <asp:HiddenField ID="HidID" runat="server" Value='<%#Eval("TERMINALID") %>' />
                                    </td>
                                    <td align="center" bgcolor="#E8F0F7" width="35%" style="font-size: small">
                                        <%#Eval("TERMINALID")%>
                                        <%--  <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "javascript:window.showModalDialog(\"DetailPic.aspx?picName="+Eval("PICNAME").ToString()+"\",null,\"dialogWidth=530px,dialogHeight=530px\");void 0;" %>'
                                            Style="text-decoration: none"><%#  Eval("PICNAME")%></asp:HyperLink>--%>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1" align="right">
                    &nbsp;
                </td>
                <td align="justify">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                                    FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" PageSize="10"
                                    Wrap="False" PagingButtonStyle="text-decoration:none" ShowMoreButtons="False"
                                    ShowPageIndex="False">
                                </webdiyer:AspNetPager>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="justify">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">
                </td>
                <td class="style3" >
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lnkRestart" class="easyui-linkbutton" icon="icon-ReStartTime"
                        runat="server" OnClick="lnkRestart_Click">重启</asp:LinkButton>
                    <%#Eval("TERMINALID")%>
                    <%--  <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "javascript:window.showModalDialog(\"DetailPic.aspx?picName="+Eval("PICNAME").ToString()+"\",null,\"dialogWidth=530px,dialogHeight=530px\");void 0;" %>'
                                            Style="text-decoration: none"><%#  Eval("PICNAME")%></asp:HyperLink>--%>
                </td>
                <td class="style3">
                    &nbsp;
                </td>
                <td class="style3">
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;
                </td>
                <td class="style3">
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

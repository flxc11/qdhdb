<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RealTime.aspx.cs" Inherits="QDHServer.ad.RealTime.RealTime" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" src="../../../js/jquery-1.4.4.min.js"></script>
    <script language="javascript" src="../../../js/jquery.easyui.min.1.2.2.js"></script>
    <link rel="stylesheet" type="text/css" href="../../../js/themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../../../js/themes/icon.css">
    <style type="text/css">
        .style1
        {
            width: 261px;
        }
        .style2
        {
            width: 261px;
            height: 20px;
        }
        .style3
        {
            height: 20px;
        }
        .style4
        {
            height: 20px;
        }
        .style5
        {
            height: 20px;
            width: 25px;
        }
        .style6
        {
            width: 377px;
            height: 20px;
        }
        .style7
        {
            width: 261px;
            height: 4px;
        }
        .style8
        {
            height: 4px;
        }
    </style>
    <script type="text/javascript">
        function alertWarningLastPic() {
            $.messager.alert("系统提示", "请输入终端编号！！", "warning");
        }

        function alertError() {
            $.messager.alert("系统提示", "请输入正确的终端编号！！", "error");
        }

        function waiting() {
            $.messager.alert("系统提示", "请稍后！！", "warning");
        }


        function alertInputTerminal() {
            $.messager.alert("系统提示", "请输入终端编号！！", "warning");
        }

        function alertWarningDeletePic() {
            $.messager.alert("系统提示", "网络异常,请稍后再试！！", "error");
        }
        //        function Chakan() {
        //            if (document.getElementById("txtTerminal").value == "") {
        //                alertWarningLastPic();
        //            } else { 
        //                 
        //            }
        //        }
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
                <td colspan="4">
                    <asp:TextBox ID="txtTerminal" runat="server"></asp:TextBox>
                    <asp:LinkButton ID="lnkShowTerminal" runat="server" class="easyui-linkbutton" icon="icon-search"
                        OnClick="lnkShowTerminal_Click" Style="vertical-align: top">搜索</asp:LinkButton>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style7" align="right">
                </td>
                <td colspan="4" class="style8">
                </td>
                <td class="style8">
                </td>
            </tr>
            <tr>
                <td class="style1" align="right">
                    &nbsp;
                </td>
                <td colspan="4">
                    <asp:DataList ID="dlTerminal" runat="server" Width="20%" DataKeyField="TERMINALID"
                        Font-Size="Small" BackColor="#E8F0F7" BorderWidth="1px" CellPadding="2" ForeColor="DeepSkyBlue">
                        <HeaderStyle BackColor="#E8F0F7" Font-Bold="True" />
                        <ItemTemplate>
                            <table width="250px" style="font-size: small">
                                <tr>
                                    <td align="center" bgcolor="#E8F0F7" width="25%" style="font-size: small">
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"RealTimeDetails.aspx?terminal="+Eval("TERMINALID")%>'
                                            Style="text-decoration: none">      <%#Eval("TERMINALID")%></asp:HyperLink>
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
                <td class="style5">
                    &nbsp;
                </td>
                <td class="style6" style="text-decoration: none">
                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                        FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" PageSize="10"
                        Wrap="False" PagingButtonStyle="text-decoration:none" ShowMoreButtons="False"
                        ShowPageIndex="False">
                    </webdiyer:AspNetPager>
                </td>
                <td class="style3">
                    &nbsp;
                </td>
                <td class="style4">
                    &nbsp;
                </td>
                <td class="style3">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TerminalManagement.aspx.cs"
    Inherits="QDHServer.ad.TerminalManagement.TerminalManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>千岛湖广告发布系统</title>
    <script language="javascript" type="text/javascript" src="../../js/jquery-1.4.4.min.js"></script>
    <script language="javascript" type="text/javascript" src="../../js/jquery.easyui.min.1.2.2.js"></script>
    <link rel="stylesheet" type="text/css" href="../../js/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../js/themes/icon.css" />
    <style type="text/css">
        .table_head_style
        {
            text-align: center;
            color: #ffffff;
        }
    </style>
    <script type="text/javascript">
        function alertWarningNet() {
            $.messager.alert("系统提示", "网络异常,请稍后再试！！", "error");
        }

        function alertDeleteSuccess() {
            $.messager.alert("系统提示", "删除成功！！", "info");
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
                        <img alt="" src="../images/LeftTile.gif" style="height: 17px" />
                        <asp:Label ID="Label1" runat="server" Text="当前位置:终端管理>终端管理 终端机器记录" Style="vertical-align: middle;
                            font-size: small"></asp:Label>
                        <asp:Label ID="lblCount" runat="server" Text="Label" Style="vertical-align: inherit;
                            font-size: small; color: red" Font-Bold="True"></asp:Label>
                        <asp:Label ID="Label3" runat="server" Text="台" Style="vertical-align: middle; font-size: small"></asp:Label>
                        <asp:ImageButton ID="IbtnAdd" runat="server" Height="24px" ImageUrl="~/ad/images/Add.png"
                            align="right" OnClick="IbtnAdd_Click" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="终端名称:" Style="vertical-align: middle;
                        font-size: small; color: darkBlue"></asp:Label>
                    <asp:TextBox ID="txtTerminalName" runat="server" Height="15px" Width="136px"></asp:TextBox>
                    <asp:DropDownList ID="ddlStatus" runat="server" Style="color: darkBlue">
                        <asp:ListItem Value="节目单状态"></asp:ListItem>
                        <asp:ListItem Value="正常"></asp:ListItem>
                        <asp:ListItem Value="异常"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:LinkButton ID="lnkShowTerminal" class="easyui-linkbutton" icon="icon-search"
                        runat="server" Style="vertical-align: top" OnClick="lnkShowTerminal_Click">搜索</asp:LinkButton>
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
                    <asp:GridView ID="gvTerminalManagement" runat="server" AutoGenerateColumns="False"
                        Width="820px" EnableModelValidation="True" Font-Size="Small" OnRowDeleting="gvTerminalManagement_RowDeleting"
                        OnRowUpdating="gvTerminalManagement_RowUpdating" 
                        onrowdatabound="gvTerminalManagement_RowDataBound">
                        <Columns>
                            <asp:BoundField HeaderText="序号" DataField="TerminalID">
                                <HeaderStyle BackColor="#E8F0F7" HorizontalAlign="Center" ForeColor="#6699FF" VerticalAlign="Middle"
                                    CssClass="table_head_style" Font-Size="Small" />
                                <ItemStyle Height="25px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="终端名称" DataField="TerminalName">
                                <HeaderStyle BackColor="#E8F0F7" HorizontalAlign="Center" ForeColor="#6699FF" CssClass="table_head_style"
                                    Font-Size="Small" />
                            </asp:BoundField>
                            <asp:ImageField HeaderText="状态" DataImageUrlField="TerminalState">
                                <HeaderStyle BackColor="#E8F0F7" HorizontalAlign="Center" ForeColor="#6699FF" VerticalAlign="Middle"
                                    CssClass="table_head_style" Font-Size="Small" />
                            </asp:ImageField>
                            <asp:BoundField HeaderText="终端IP地址" DataField="TerminalIP">
                                <HeaderStyle BackColor="#E8F0F7" ForeColor="#6699FF" CssClass="table_head_style"
                                    Font-Size="Small" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="终端mac地址" DataField="TerminalMac">
                                <HeaderStyle BackColor="#E8F0F7" ForeColor="#6699FF" CssClass="table_head_style"
                                    Font-Size="Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TerminalAddress" HeaderText="终端地址">
                                <HeaderStyle BackColor="#E8F0F7" ForeColor="#6699FF" CssClass="table_head_style"
                                    Font-Size="Small" />
                            <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Createtime" HeaderText="上报时间">
                                <HeaderStyle BackColor="#E8F0F7" ForeColor="#6699FF" CssClass="table_head_style"
                                    Font-Size="Small" />
                            <ItemStyle Width="120px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbUpdate" runat="server" CommandName="Update" Font-Size="Small"
                                        Text="修改" Style="text-decoration: none"></asp:LinkButton>|
                                    <asp:LinkButton ID="lkbDelete" runat="server" CommandName="Delete" Text="删除" Font-Size="Small"
                                        Style="text-decoration: none"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle BackColor="#E8F0F7" CssClass="table_head_style" Font-Size="Small" ForeColor="#6699FF" />
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                            <div align="center">
                                <p style="font-weight: bolder">
                                    Sorry！没有找到您所需的数据。</p>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="4" align="center" style="font-size: small">
                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" FirstPageText="首页"
                        LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" PageSize="10" Wrap="False"
                        PagingButtonStyle="text-decoration:none" ShowMoreButtons="False" ShowPageIndex="False"
                        OnPageChanged="AspNetPager1_PageChanged">
                    </webdiyer:AspNetPager>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

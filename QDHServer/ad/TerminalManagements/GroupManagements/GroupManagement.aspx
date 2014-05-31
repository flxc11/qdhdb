<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupManagement.aspx.cs"
    Inherits="QDHServer.ad.TerminalManagements.GroupManagements.GroupManagement" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>千岛湖广告发布系统</title>
    <script language="javascript" type="text/javascript" src="../../../js/jquery-1.4.4.min.js"></script>
    <script language="javascript" type="text/javascript" src="../../../js/jquery.easyui.min.1.2.2.js"></script>
    <link rel="stylesheet" type="text/css" href="../../../js/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../../js/themes/icon.css" />
    <style type="text/css">
        .table_head_style
        {
            text-align: center;
            color: #ffffff;
        }
        .style1
        {
            height: 30px;
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
                <td class="style1">
                    <div style="background-color: lightBlue; height: 22px;">
                        <img alt="" src="../../images/LeftTile.gif" style="height: 17px" />
                        <asp:Label ID="Label1" runat="server" Text="当前位置:终端管理>分组管理" Style="vertical-align: middle;
                            font-size: small"></asp:Label>
                        <asp:ImageButton ID="IbtnAdd" runat="server" Height="23px" ImageUrl="~/ad/images/Add.png"
                            OnClick="IbtnAdd_Click" align="right" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvGroupManagement" runat="server" AutoGenerateColumns="False" Width="820px"
                        EnableModelValidation="True" Font-Size="Small" OnRowDeleting="gvGroupManagement_RowDeleting"
                        OnRowUpdating="gvGroupManagement_RowUpdating" OnRowDataBound="gvGroupManagement_RowDataBound">
                        <Columns>
                            <asp:BoundField HeaderText="序号" DataField="TerminalTypeID">
                                <HeaderStyle BackColor="#E8F0F7" HorizontalAlign="Center" ForeColor="#6699FF" VerticalAlign="Middle"
                                    CssClass="table_head_style" Font-Size="Small" />
                                <ItemStyle Height="25px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="组名" DataField="TerminalTypeName">
                                <HeaderStyle BackColor="#E8F0F7" HorizontalAlign="Center" ForeColor="#6699FF" CssClass="table_head_style"
                                    Font-Size="Small" />
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="区域" DataField="TerminalTypeArea">
                                <HeaderStyle BackColor="#E8F0F7" HorizontalAlign="Center" ForeColor="#6699FF" CssClass="table_head_style"
                                    Font-Size="Small" />
                                <ItemStyle Width="130px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="创建时间" DataField="Createtime">
                                <HeaderStyle BackColor="#E8F0F7" ForeColor="#6699FF" CssClass="table_head_style"
                                    Font-Size="Small" />
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Updatetime" HeaderText="上次修改时间">
                                <HeaderStyle BackColor="#E8F0F7" ForeColor="#6699FF" CssClass="table_head_style"
                                    Font-Size="Small" />
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbUpdate" runat="server" ForeColor="Blue" CommandName="Update"
                                        Font-Size="Small" Text="修改" Style="text-decoration: none"></asp:LinkButton>|
                                    <asp:LinkButton ID="lkbDelete" runat="server" CommandName="Delete" Text="删除" ForeColor="Blue"
                                        Font-Size="Small" Style="text-decoration: none"></asp:LinkButton>| <a href='<%#"../TerminalManagements/TerminalManagement.aspx?id="+Eval("TerminalTypeID") %>'
                                            style="text-decoration: none; color: Blue">终端维护</a>
                                </ItemTemplate>
                                <HeaderStyle BackColor="#E8F0F7" CssClass="table_head_style" Font-Size="Small" ForeColor="#6699FF" />
                                <ItemStyle ForeColor="#3399FF" />
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
                <td class="style1" align="center" style="font-size: small">
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

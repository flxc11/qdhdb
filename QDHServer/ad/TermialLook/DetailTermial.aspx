<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetailTermial.aspx.cs"
    Inherits="QDHServer.ad.TermialLook.DetailTermial" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 51px;
        }
        .style2
        {
            width: 26px;
        }
        .style3
        {
            width: 298px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <table style="width: 100%;">
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style1" style="color: DeepSkyBlue">
                    图片:
                </td>
                <td class="style3">
                    <asp:DataList ID="dlPic" runat="server" Width="49%" Font-Size="Small" Font-Bold="False"
                        Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                        <HeaderTemplate>
                            <table width="300px" style="font-size: small">
                                <tr>
                                    <td width="15%" align="center" bgcolor="#E8F0F7" style="font-size: small">
                                        图片
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table width="300px" style="font-size: small">
                                <tr>
                                    <td align="center" bgcolor="#FFFFFF" width="15%" style="font-size: small">
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "javascript:window.showModalDialog(\"DetailShowPic.aspx?picName="+Eval("PICNAME").ToString()+"\",null,\"dialogWidth=530px,dialogHeight=530px\");void 0;" %>'><%#  Eval("PICNAME")%></asp:HyperLink>
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
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style1" style="color: DeepSkyBlue">
                    视频:
                </td>
                <td class="style3">
                    <asp:DataList ID="dlVideo" runat="server" Width="49%" Font-Size="Small" Font-Bold="False"
                        Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                        <HeaderTemplate>
                            <table width="300px" style="font-size: small">
                                <tr>
                                    <td width="15%" align="center" bgcolor="#E8F0F7" style="font-size: small">
                                        视频
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table width="300px" style="font-size: small">
                                <tr>
                                    <td align="center" bgcolor="#FFFFFF" width="15%" style="font-size: small">
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "javascript:window.showModalDialog(\"DetailShowVideo.aspx?videoName="+Eval("VideoName").ToString()+"\",null,\"dialogWidth=530px,dialogHeight=530px\");void 0;" %>'><%#  Eval("VideoName")%></asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
                <td>
                    <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click" Text="&lt;&lt;返回" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            </table>
        &nbsp;&nbsp;
    </div>
    </form>
</body>
</html>

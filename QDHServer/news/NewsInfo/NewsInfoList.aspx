<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsInfoList.aspx.cs" Inherits="QDHServer.news.NewsInfo.NewsInfoList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title></title>
<link href="../Scripts/Themes/Aqua/Css/Ligerui-All.css" rel="stylesheet" type="text/css" />
<link href="../Css/Style.css" rel="stylesheet" type="text/css" />
<link href="../Scripts/ThickBox/ThickBox.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript" src="../Scripts/JQuery.js"></script>
<script language="javascript" type="text/javascript" src="../Scripts/Common.js"></script>
<script language="javascript" type="text/javascript" src="../Scripts/Cms.js"></script>
<script language="javascript" type="text/javascript" src="../Scripts/LigerBuild.js"></script>
<script language="javascript" type="text/javascript" src="../Scripts/ThickBox/ThickBox.js"></script>
<script language="javascript" type="text/javascript" src="../Scripts/ThickBox/Option.js"></script>
</head>
<body class="mainbody">
<form id="formsea" runat="server" name="formsea" onsubmit="return sub()">
<div class="navigation">当前位置：&nbsp;<a href="NewsInfoList.aspx">信息管理</a> &gt; 信息列表</div>
<div class="tools_box">
<div class="tools_search">
<table cellpadding="0" cellspacing="0" width="350">
<tr>
  <td width="50" height="45" align="center">标题</td>
  <td width="100" align="center">
      <asp:TextBox ID="keyword" runat="server" Width="90"></asp:TextBox></td>
  <td width="50" height="45" align="center">栏目</td>
  <td width="100" align="center">
      <asp:DropDownList ID="TxtColumnID" runat="server" Width="90">
      </asp:DropDownList>
  </td>
  <td width="50" align="center">
      <input type="submit" name="sub1" value="搜  索" /></td>
</tr>
</table>
<script type="text/javascript">
    function sub() {
        $("#__VIEWSTATE").attr("disabled", true);
        var keyword = $("#keyword").val();
        var ddlcolumn = $("#TxtColumnID").val();
        document.getElementById("formsea").action = "NewsinfoList.aspx?action=Search&keyword=" + keyword + "&ColumnID=" + ddlcolumn;
        document.getElementById("formsea").submit();
        return true;
    }
</script>
</div>
<div class="tools_bar">
<a href="javascript:void(0);" onclick="checkAll(this);" class="tools_btn"><span><b class="all">全选</b></span></a>
<asp:Literal ID="LitUsersDel" runat="server"></asp:Literal>
<a href="NewsInfoAdd.aspx" class="tools_btn"><span><b class="add">新增</b></span></a>
<a href="javascript:;" onclick="CmsOrder(<%=PcolumnId%>)" class="tools_btn">
    <span><b class="px">排序</b></span></a>
</div>
</div>
<table id="tbList" width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
<tr>
<th width="5%">选择</th>
<th width="35%" align="left">标题</th>
<th width="15%" align="left">栏目</th>
<th width="10%" align="left">排序</th>
<th width="10%" align="left">审核</th>
<th width="15%" align="left">发布时间</th>
<th width="10%" align="center">操作</th>
</tr>
<asp:Repeater ID="Repeater1" runat="server">
<ItemTemplate>
<tr>
<td><input class="cbCheck" type="checkbox" value='<%# Eval("NewsID")%>' /></td>
<td><a href="NewsInfoma.aspx?ID=<%# Eval("NewsID")%>&PageNo=<%=PageNo %>"><%#Eval("NewsTitle")%></a></td>
<td><%#CNVP.UI.Column.GetCloumnName(Eval("ColumnID").ToString())%></td>
<td></td>
<td><%#IsAudit(Eval("NewsID").ToString(), Convert.ToInt32(Eval("NewsState").ToString())) %></td>
<td><%#Convert.ToDateTime(Eval("CreateTime"))%></td>
<td align="center"><a href="NewsInfoma.aspx?ID=<%# Eval("NewsID")%>&PageNo=<%=PageNo %>">编辑</a> | <a href="javascript:void(0);" onclick="DeleteOne(confirmMsg,<%# Eval("NewsID")%>,'Ajax.aspx')">删除</a></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</table>
<div class="line15"></div>
<div id="paging" class="flickr right"><asp:Literal ID="LitPager" runat="server"></asp:Literal></div>
<div class="line10"></div>
</form>
</body>
</html>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="QDHServer.test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </div>
    <asp:GridView ID="GridView1" runat="server" EnableModelValidation="True" 
        AutoGenerateColumns="false" Height="162px" Width="193px">
        <Columns>
             <asp:BoundField DataField="rq" HeaderText="日期" SortExpression="rq" />
                <asp:BoundField DataField="tq" HeaderText="天气" SortExpression="tq" />
                 <asp:BoundField DataField="tqimg" HeaderText="天气图片" SortExpression="tqimg" />
                <asp:BoundField DataField="qw" HeaderText="气温" SortExpression="qw" />
                 <asp:BoundField DataField="fx" HeaderText="风向" SortExpression="fx" />

        </Columns>
    </asp:GridView>
    </form>
</body>
</html>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="footer.ascx.cs" Inherits="QDHServer.Default.footer" %>

<div id="hottitle" class="hot">
    <ul id="ulid">
        <asp:Repeater runat="server" ID="rptnotice">
            <ItemTemplate>
                <li><a href='shownews.aspx?ID=<%#Eval("NewsID") %>>' title='<%#Eval("NewsTitle") %>'><%#Eval("NewsTitle") %></a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
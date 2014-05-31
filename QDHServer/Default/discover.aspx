<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="discover.aspx.cs" Inherits="QDHServer.Default.discover" %>

<%@ Register Src="~/Default/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/Default/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/Default/loading.ascx" TagPrefix="uc1" TagName="loading" %>

<!DOCTYPE html>

<html class="loading">
<head>
    <uc1:header runat="server" ID="header" />
    <link rel="stylesheet" href="css/Global.css"/>
    <link rel="stylesheet" href="css/style.css"/>
    <script src="scripts/jquery.js"></script>
    <script src="scripts/MSClass.js"></script>
    <script src="scripts/common.js?Time=new date()"></script>
    <script src="scripts/cnvp.js"></script>
</head>
<body>
<div class="wrapper">
    <div class="main bg1">
        <div class="header clearfix" style="height: 145px">
            <span class="back"><a href="index.aspx">返回上页</a></span>
            <div class="position"><a href="discover.aspx" class="crt">玩转千岛湖</a></div>
        </div>
        <div class="wzqdh clearfix">
            <div class="wz_r">
                <a href="/Col/Col29/Index.aspx "><img src="images/imgwzqdh7.png" style="margin-bottom: 33px;" alt="美食"/></a>
                <a href="/Col/Col15/Index.aspx "><img src="images/imgwzqdh8.png" alt=""/></a>
            </div>
            <div class="wz_m">
                <a href="/Col/Col13/Index.aspx "><img src="images/imgwzqdh4.png" style="margin-bottom: 32px;" alt=""/></a>
                <a href="/Col/Col31/Index.aspx "><img src="images/imgwzqdh5.png" style="margin-bottom: 31px;" alt=""/></a>
                <a href="/Col/Col14/Index.aspx "><img src="images/imgwzqdh6.png" alt=""/></a>
            </div>
            <div class="wz_l">
                <a href="show.aspx?ClassID=21"><img src="images/imgwzqdh1.png" alt="景色" style="margin-bottom: 36px;" alt=""/></a>
                <a href="/Col/Col19/Index.aspx "><img src="images/imgwzqdh2.png" style="margin-bottom: 32px;" alt=""/></a>
                <a href="/Col/Col18/Index.aspx "><img src="images/imgwzqdh3.png" alt=""/></a>
            </div>
        </div>
        <div class="bottom">
            <uc1:footer runat="server" ID="footer" />
        </div>
    </div>
</div>
<uc1:loading runat="server" ID="loading" />
</body>
<script>
    new Marquee(["hottitle", "ulid"], 2, 2, 986, 103, 20, 0, 0);
</script>
</html>

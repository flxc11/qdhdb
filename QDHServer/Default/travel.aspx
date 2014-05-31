<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="travel.aspx.cs" Inherits="QDHServer.Default.travel" %>

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
    <script src="scripts/common.js?time=new Date().getTime()"></script>
    <script src="scripts/cnvp.js"></script>
</head>
<body>
<div class="wrapper">
    <div class="main bg1">
        <div class="header clearfix">
            <span class="back"><a href="index.aspx">返回上页</a></span>
            <div class="position"><a href="travel.aspx" class="crt">主题旅游</a></div>
        </div>
        <div class="zthd clearfix">
            <div class="z_r">
                <a href="show.aspx?ClassID=15"><img src="images/imgztly2.png" alt="婚纱蜜月"/></a>
                <a href="/Col/Col8/Index.aspx "><img src="images/imgztly3.png" style="margin-top: 29px;" alt="水上运动"/></a>
            </div>
            <div class="z_l">
                <a href="/Col/Col6/Index.aspx "><img src="images/imgztly1.png" alt="环湖骑行"/></a>
                <a href="/Col/Col9/Index.aspx "><img src="images/imgztly4.png" style="margin-right: 19px;" alt="登山露营"/></a>
                <a href="/Col/Col10/Index.aspx "><img src="images/imgztly5.png" alt="工农业旅游"/></a>
                <a href="/Col/Col11/Index.aspx "><img src="images/imgztly6.png" alt="城市旅游"/></a>
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

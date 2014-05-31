<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="QDHServer.Default.index" %>

<%@ Register Src="~/Default/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/Default/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/Default/loading.ascx" TagPrefix="uc1" TagName="loading" %>


<!DOCTYPE html>
<html class="loading">
    <head>
        <meta charset='utf-8'>
        <uc1:header runat="server" ID="header" />
        <link rel="stylesheet" href="css/Global.css"/>
        <link rel="stylesheet" href="css/style.css"/>
        <script src="scripts/jquery.js"></script>
        <script src="scripts/MSClass.js"></script>
        <script src="scripts/cnvp.js"></script>
        <script src="scripts/common.js?Time=new date()"></script>
        <script src="scripts/cookie.js"></script>
        <script>
            (function ($) {
                $.getUrlParam = function (name) {
                    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
                    var r = window.location.search.substr(1).match(reg);
                    if (r != null) return unescape(r[2]); return null;
                }
            })(jQuery);
        </script>
    </head>
<body>
<div class="wrapper">
    <div class="main bg1">
        <div class="clearfix">
            <div class="logo"><a href="/default/index.html">返回首页</a></div>
            <div class="ewm"><img src="images/imgewm.png" alt=""/></div>
        </div>

        <div class="index_column">
            <ul>
                <li><a href="travel.aspx"><img src="images/imgindex1.png" alt="主题旅游" /></a></li>
                <li><a href="/Col/Col2/Index.aspx "><img src="images/imgindex2.png" alt="节庆活动" /></a></li>
                <li><a href="discover.aspx"><img src="images/imgindex3.png" alt="玩转千岛湖" /></a></li>
                <li style="padding-right: 8px;"><a href="/Col/Col4/Index.aspx"><img src="images/imgindex4.png" alt="旅游咨询" /></a></li>
                <li><a href="/Col/Col5/Index.aspx"><img src="images/imgindex5.png" alt="码上游" /></a></li>
                <li>
                    <div class="weather clearfix">
                        <div class="wea_l">
                            <div style="width: 100%; cursor: pointer;" id="wea_content" onclick="javascript:window.location.href='/Col/Col41/Index.aspx'">;

                            </div>
                        </div>
                        <div class="wea_r"><script>showLocale()</script>

                        </div>
                    </div>
                </li>
                <li style="padding-right: 8px;"><a href="/Col/Col40/Index.aspx"><img src="images/imgindex7.png" alt="查看周边" /></a></li>
                <li><a href="/Col/Col34/Index.aspx"><img src="images/imgindex8.png" alt="企业介绍" /></a></li>
            </ul>
        </div>
        <div class="bottom">
            <uc1:footer runat="server" ID="footer" />
        </div>
    </div>
</div>
    <uc1:loading runat="server" ID="loading" />
</body>
<script language="javascript" type="text/javascript">
    <!--
    //获得当前时间,刻度为一千分一秒
    var initializationTime = (new Date()).getTime();
    function showLeftTime() {
        var now = new Date();
        var year = now.getYear();
        var month = now.getMonth();
        var day = now.getDate();
        var hours = now.getHours();
        var minutes = now.getMinutes();
        var seconds = now.getSeconds();
        document.getElementById("i_time").innerHTML = "" + hours + ":" + minutes + ":" + seconds + "";
        //一秒刷新一次显示时间
        var timeID = setTimeout(showLeftTime, 1000);
    }
    //-->
    var _data = $.cookie("MACAdd") || $.getUrlParam("data");
    $.cookie("MACAdd", _data, { expires: 7 });
    function GetFirstMac(macadd) {
        $.ajax({
            type: "post",
            dataType: "json",
            url: "/webajax.aspx?Time=" + (new Date().getTime()),
            data: {
                Action: "GetFirstMAC",
                Address: macadd
            },
            error: function () {

            },
            success: function (d) {

            }
        })
    }
    function GetWea() {
        var _var = "";
        var reg_num = /\d/;
        var reg_time = /^\w/;
        var _timeurl = "";
        $.getJSON("http://query.yahooapis.com/v1/public/yql", {
            q: "select * from json where url=\"http://www.weather.com.cn/data/cityinfo/101210104.html\"",
            format: "json"
        }, function (data) {
            if (data.query.results) {
                var J_data = JSON.parse(JSON.stringify(data.query.results));
                var _imgurl = J_data.weatherinfo.img1;
                var _num = "0" + _imgurl.match(reg_num);
                _num = _num.substring(_num.length - 2, _num.length);
                var _time = _imgurl.match(reg_time);
                if (_time == "d") {
                    _timeurl = "day";
                } else {
                    _timeurl = "night";
                }
                _html = '<dl>';
                _html += '<dd class="tmp">' + J_data.weatherinfo.temp1 + '/' + J_data.weatherinfo.temp2 + '</dd>';
                _html += '<dd class="tm_pic"><img src="images/' + _timeurl + "/" + _num + '.png" /></dd>';
                _html += '<dd class="tm_time"><span class="i_time" id="i_time">&nbsp;</span></dd>';
                _html += '</dl>';
                $("#wea_content").html(_html);
                //showLeftTime();
            }
        });
    }
</script>
<script>
    $(function () {
        GetFirstMac($.cookie("MACAdd"));
        GetWea();
    })
    new Marquee(["hottitle", "ulid"], 2, 2, 986, 103, 20, 0, 0);
</script>
</html>

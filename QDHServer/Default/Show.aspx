<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="QDHServer.Default.Show" %>

<%@ Register Src="~/Default/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/Default/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/Default/loading.ascx" TagPrefix="uc1" TagName="loading" %>

<!DOCTYPE html>

<html class="loading">
<head runat="server">
    <uc1:header runat="server" ID="header" />
    <link rel="stylesheet" href="css/Global.css"/>
    <link rel="stylesheet" href="css/style.css"/>
    <link rel="stylesheet" href="css/tango/skin.css"/>
    <script src="scripts/jquery.js"></script>
    <script src="scripts/MSClass.js"></script>
    <script src="scripts/jquery.jcarousel.min.js"></script>
    <script src="scripts/common.js?time=new Date().getTime()"></script>
    <script src="scripts/cnvp.js"></script>
</head>
<body>
<div class="wrapper">
    <div class="main bg1 posa">
        <div class="icon1 icon_next"></div>
        <div class="icon1 icon_prev"></div>
        <input type="hidden" name="ColumnID" id="ColumnID" value="<%=ColumnId %>" />
        <div class="header clearfix">
            <span class="back"><a href="javascript:history.back()">返回上页</a></span>
            <div class="position"><!--unit name="列表_当前位置"-->列表_当前位置<!--/unit--></div>
        </div>
        <div class="jd" id="newslist">
        </div>
        <div class="xkpage"></div>
        <script type="text/javascript">
            window._CurPage = "";
            window.PageCount = "";
            function reset() {
                $("input[type='text'][id='ColumnID']").val("");
            }
            function getInfo(CurPage) {
                var $ColumnID = $("#ColumnID");
                _CurPage = CurPage;
                if (_CurPage == "" || _CurPage == undefined) {
                    _CurPage = "1";
                }
                $.ajax({
                    type: "post",
                    dataType: "json",
                    url: "webajax.aspx?Time=" + (new Date().getTime()), //目标地址
                    data: {
                        Action: "NewsList",
                        PageNo: _CurPage,
                        ColumnID: $ColumnID.val()
                    },
                    beforeSend: function () {
                        $("#newslist").html('<div><img src="images/Loading.gif" /></div>');
                    },
                    success: function (d) {
                        var html = "";
                        if (d.RecordCount > 0) {
                            if (d.RecordCount > 7) {
                                $(".icon1").show();
                            };
                            PageCount = d.PageCount;
                            $.each(d.list, function (i, v) {
                                if (v.Wifi == "1") {
                                    sp3 = '<span class="p_span3"></span>';
                                }
                                if (v.Park == "1") {
                                    sp4 = '<span class="p_span4"></span>';
                                }
                                if (v.Box == "1") {
                                    sp5 = '<span class="p_span5"></span>';
                                }
                                html += '<a href="/Art/Art_' + v.ColumnID + '/Art_' + v.ColumnID + '_' + v.NewsID + '.aspx"><dl>';
                                html += '<dt>' + v.NewsTitle + '</dt>';
                                html += '<dd class="pic"><img src="' + v.NewsLogourl + '" alt="' + v.NewsTitle + '""/></dd>';
                                html += '<dd class="txt">' + CutString(v.bz, 60);
                                html += '</dd>';
                                html += '<dd class="parameter">';
                                //html += '<a href="/Art/Art_' + v.ColumnID + '/Art_' + v.ColumnID + '_' + v.NewsID + '.aspx"><span class="jd_gdatail">详情>></span></a>';
                                //html += '<span class="p_span1">' + arr[0] + '</span>';
                                html += '<span class="p_span2">推荐指数：<em>' + v.Recommend + '</em></span>';
                                html += '<span class="p_span2">人气指数：<em>' + v.Pop + '</em></span>';
                                html += sp3;
                                html += sp4;
                                html += sp5;
                                html += "</dl></a>";
                            })
                        } else {
                            //html += "<span>暂无信息</span>";
                        }
                        $("#newslist").html(html);
                        var _div = "";
                        if (_CurPage < 5 && PageCount <= 5) {
                            //_div += "共 " + d.RecordCount + " 条记录";
                            //_div += " 共 " + PageCount + " 页 ";

                            for (var i = 1; i <= PageCount; i++) {
                                _div += '<a rel="' + i + '" href="javascript:;" class="a-num" onclick="getInfo(' + i + ')">' + i + '</a>';

                            }
                            //_div += '<a href="javascript:;" class="a-num" ' +
                            //'onclick="getInfo(' + d.PageCount + ')">尾页</a>';

                        }
                        else if (_CurPage < 5 && PageCount > 5) {
                            //_div += "共 " + d.RecordCount + " 条记录";
                            //_div += " 共 " + PageCount + " 页 ";
                            for (var i = 1; i <= 5; i++) {
                                _div += '<a rel="' + i + '" href="javascript:;" class="a-num" onclick="getInfo(' + i + ')">' + i + '</a>';

                            }
                            //_div += '<a href="javascript:;" class="a-num" ' +
                            //'onclick="getInfo(' + d.PageCount + ')">尾页</a>';
                        }
                        else if (_CurPage > d.PageCount - 4) {
                            //_div += "共 " + d.RecordCount + " 条记录";
                            //_div += " 共 " + d.PageCount + " 页 ";
                            //_div += '<a href="javascript:;" class="a-num" ' +
                            //'onclick="getInfo(1)">首页</a>';
                            _div += '<a rel="' + (d.PageCount - 4) + '" href="javascript:;" class="a-num" onclick="getInfo(' + (d.PageCount - 4) + ')">' + (d.PageCount - 4) + '</a>';
                            _div += '<a rel="' + (d.PageCount - 3) + '" href="javascript:;" class="a-num" onclick="getInfo(' + (d.PageCount - 3) + ')">' + (d.PageCount - 3) + '</a>';
                            _div += '<a rel="' + (d.PageCount - 2) + '" href="javascript:;" class="a-num" onclick="getInfo(' + (d.PageCount - 2) + ')">' + (d.PageCount - 2) + '</a>';
                            _div += '<a rel="' + (d.PageCount - 1) + '" href="javascript:;" class="a-num" onclick="getInfo(' + (d.PageCount - 1) + ')">' + (d.PageCount - 1) + '</a>';
                            _div += '<a rel="' + (d.PageCount) + '" href="javascript:;" class="a-num" onclick="getInfo(' + d.PageCount + ')">' + d.PageCount + '</a>';
                        } else {
                            //_div += "共 " + d.RecordCount + " 条记录";
                            //_div += " 共 " + d.PageCount + " 页 ";
                            // _div += '<a href="javascript:;" class="a-num" ' +
                            //'onclick="getInfo(1)">首页</a>';
                            for (var i = -2; i <= 2; i++) {
                                _div += '<a rel="' + (_CurPage + i) + '" href="javascript:;" class="a-num" onclick="getInfo(' + (_CurPage + i) + ')">' + (_CurPage + i) + '</a>';
                            }
                            //_div += '<a href="javascript:;" class="a-num" ' +
                            //'onclick="getInfo(' + d.PageCount + ')">尾页</a>';
                        }
                        $(".xkpage").html(_div);
                        $(".xkpage a[rel=" + _CurPage + "]").attr("class", "current1");
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {

                    }
                })
            };
            $(".icon_next").on("click", function () {
                if (_CurPage < PageCount) {
                    getInfo(_CurPage + 1);
                }
            })
            $(".icon_prev").on("click", function () {
                if (_CurPage > 1) {
                    getInfo(_CurPage - 1);
                }
            })
        </script>
        <div class="bottom">
            <uc1:footer runat="server" ID="footer" />
        </div>
    </div>
</div>
<uc1:loading runat="server" ID="loading" />
</body>
<script>
    $(function () {
        $(".sub_column a").on("click", function () {
            var $a = $(this);
            var third_ColID = $a.attr("data");
            $a.parent().addClass("lihover")
                    .siblings().removeClass("lihover");
            $("#ColumnID").val(third_ColID);
            getInfo(1);
        })
        getInfo(1);
    })
    new Marquee(["hottitle", "ulid"], 2, 2, 986, 103, 20, 0, 0);
</script>
</html>

/** WebStorm v0.1.0
 * @author: Merainy
 * @date: 2014/4/11
 * @email: Merainy.a@Gmail.com
 * @description:
 * ========================
 */
$(window).bind('DOMContentLoaded',function(){
    setTimeout(function(){
        $('html').removeClass('loading');
    },400);
});
$(function(){
    $(".index_column img").on({
        click: function(){
            $(this).css("transform", "scale(0.95)");
        },
        touchstart: function(){
            $(this).css("transform", "scale(0.95)");
        },
        touchhend: function(){
            $(this).css("transform", "scale(1)");
        }
    })
    $(".wzqdh img").on({
        click: function(){
            $(this).css("transform", "scale(0.95)");
        },
        touchstart: function(){
            $(this).css("transform", "scale(0.95)");
        },
        touchhend: function(){
            $(this).css("transform", "scale(1)");
        }
    })
    $(".zthd img").on({
        mousedown: function(){
            $(this).css("transform", "scale(0.95)");
        },
        mouseup: function(){
            $(this).css("transform", "scale(1)");
        },
        touchstart: function(){
            $(this).css("transform", "scale(0.95)");
        },
        touchhend: function(){
            $(this).css("transform", "scale(1)");
        }
    })

    var $title = $(".slt_title");
    var $txt = $(".slt_txt");
    $title.on("click", function() {
        if ($txt.not(":animated")) {
            if ($txt.is(":visible")) {
                $txt.slideUp("fast");
                $title.removeClass("open");
            } else {
                $txt.slideDown("1000");
                $title.addClass("open");
            }
        }
    })
    $txt.find("li").on("click", function(){
        var _url = $(this).attr("data");
        $txt.slideUp("fast");
        $title.removeClass("open");
        $title.html($(this).html());
        $(this).siblings().removeClass("select");
        $(this).addClass("select");
        window.location.href=_url;
    })
})
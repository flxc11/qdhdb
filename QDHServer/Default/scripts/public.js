$(function(){
	// 首页动画
	if($('#isindex').length>0){
		//活动与赛事动画
		var $j_activity = $('#j_activity');
		var $activity = $('.activity');
		var $activity_item = $('.activity a');
		var activity_opened = false;
		var fnActivityIn = function(){
			activity_opened = true;
			if(Modernizr.csstransitions){
				$activity.addClass('activity_opened');
			}else{
				$activity_item.eq(0).animate({top:-202, left:-274}, {duration:300, queue:false, easing:'easeOutQuad'});
				$activity_item.eq(1).animate({top:-259, left:-2}, {duration:300, queue:false, easing:'easeOutQuad'});
				$activity_item.eq(2).animate({top:-201, left:268}, {duration:300, queue:false, easing:'easeOutQuad'});
			}
		};
		var fnActivityOut = function(){
			activity_opened = false;
			if(Modernizr.csstransitions){
				$activity.removeClass('activity_opened');
			}else{
				$activity_item.animate({top:0, left:0}, {duration:300, queue:false, easing:'easeOutQuad'});
			}
		};
		$j_activity.on('click', function(){
			if(activity_opened){
				fnActivityOut();
			}else{
				fnServiceOut();
				fnActivityIn();
			}
		});

		//会所与服务动画
		var $j_service = $('#j_service');
		var $service = $('.service');
		var $service_item = $('.service a');
		var service_opened = false;
		var fnServiceIn = function(){
			service_opened = true;
			if(Modernizr.csstransitions){
				$service.addClass('service_opened');
			}else{
				$service_item.eq(0).animate({top:-185, left:-332}, {duration:300, queue:false, easing:'easeOutQuad'});
				$service_item.eq(1).animate({top:-245, left:-168}, {duration:300, queue:false, easing:'easeOutQuad'});
				$service_item.eq(2).animate({top:-285, left:-3}, {duration:300, queue:false, easing:'easeOutQuad'});
				$service_item.eq(3).animate({top:-245, left:161}, {duration:300, queue:false, easing:'easeOutQuad'});
				$service_item.eq(4).animate({top:-185, left:325}, {duration:300, queue:false, easing:'easeOutQuad'});
			}
		};
		var fnServiceOut = function(){
			service_opened = false;
			if(Modernizr.csstransitions){
				$service.removeClass('service_opened');
			}else{
				$service_item.animate({top:0, left:0}, {duration:300, queue:false, easing:'easeOutQuad'});
			}
		};
		$j_service.on('click', function(){
			if(service_opened){
				fnServiceOut();
			}else{
				fnActivityOut();
				fnServiceIn();
			}
		});

		var tIn = false;
		var tOut = false;
		var $overlay = $('.overlay');
		$('.activity a, .service a').hover(
			function(){
				if(tOut){
					clearTimeout(tOut);
					tOut = false;
				}
				tIn = setTimeout(function(){ $overlay.fadeIn(); }, 200);
			},
			function(){
				if(tIn){
					clearTimeout(tIn);
					tIn = false;
				}
				tOut = setTimeout(function(){ $overlay.fadeOut(); }, 200);
			}
		);

		var $win = $(window);
		var winWidth = $win.width();
		var winHeight = $win.height();
		var $slider_item = $('.slider_bd img');
		var resizeTimer = null;
		$slider_item.elemAutoSize(winWidth, winHeight);
		$(window).on('resize', function(){
			if(resizeTimer){
				clearTimeout(resizeTimer)
			}
			resizeTimer = setTimeout(function(){
				winWidth=$win.width();
				winHeight=$win.height();
				$slider_item.elemAutoSize(winWidth, winHeight);
			}, 200);
		});
	}// isindex end

	// 延迟加载
	var $lazy = $("img.lazy");
	if($lazy.length>0){
		$lazy.lazyload({
            effect: "fadeIn"
        });
	}


});

$(function(){
	// 背景幻灯
	var $slider_tab_item = $('.slider_tab li');
	var $slider_item = $('.slider_bd img');
	var num =  $slider_item.length;
	var i = 0;
	var slider_t = false;
	var fnAuto = function(n){
		if(n!=undefined){
			i = n;
		}else{
			i = i<(num-1) ? i+1 : 0;
		}
		$slider_item.eq(i).fadeIn(800).siblings().fadeOut(800);
		$slider_tab_item.eq(i).addClass('current').siblings().removeClass('current');
	};
	slider_t = setInterval(function(){ fnAuto(); }, 6000);
	$slider_tab_item.on('click', function(){
		if(slider_t){
			clearInterval(slider_t);
			slider_t = false;
		}
		fnAuto($(this).index());
		slider_t = setInterval(function(){ fnAuto(); }, 6000);
	});
});

function activity_calendar(url){
	$('.activity_calendar_loading').show();
	$.ajax({
		type:'GET',
		url:url,
		dataType:'html'
	}).done(function(html){
		$('.activity_calendar').html(html);
		$('.activity_calendar_loading').hide();
	});
}
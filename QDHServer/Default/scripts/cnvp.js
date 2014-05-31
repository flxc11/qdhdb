var ieVersion = function(){
    var ver = 100,
        ie = (function(){
            var undef,
                v = 3,
                div = document.createElement('div'),
                all = div.getElementsByTagName('i');
            while (
                div.innerHTML = '<!--[if gt IE ' + (++v) + ']><i></i><![endif]-->',
                    all[0]
                );
            return v > 4 ? v : undef;
        }());
    if(ie) ver = ie;
    return ver;
};
function showLocale()
{
	var objD = new Date();
	var str,colorhead,colorfoot;
	var yy = objD.getYear();
	if(yy<1900) yy = yy+1900;
	var MM = objD.getMonth()+1;
	if(MM<10) MM = '0' + MM;
	var dd = objD.getDate();
	if(dd<10) dd = '0' + dd;
	var hh = objD.getHours();
	if(hh<10) hh = '0' + hh;
	var mm = objD.getMinutes();
	if(mm<10) mm = '0' + mm;
	var ss = objD.getSeconds();
	if(ss<10) ss = '0' + ss;
	var ww = objD.getDay();
	if  (ww==0)  ww="星期日";
	if  (ww==1)  ww="星期一";
	if  (ww==2)  ww="星期二";
	if  (ww==3)  ww="星期三";
	if  (ww==4)  ww="星期四";
	if  (ww==5)  ww="星期五";
	if  (ww==6)  ww="星期六";
	str = ww + "<br /><span class='i_date'>" + yy + "-" + MM + "-" + dd + "" + "</span><br />"  ;
	document.write(str);
}
function showLocale1()
{
	var objD = new Date();
	var str,colorhead,colorfoot;
	var yy = objD.getYear();
	if(yy<1900) yy = yy+1900;
	var MM = objD.getMonth()+1;
	if(MM<10) MM = '0' + MM;
	var dd = objD.getDate();
	if(dd<10) dd = '0' + dd;
	var hh = objD.getHours();
	if(hh<10) hh = '0' + hh;
	var mm = objD.getMinutes();
	if(mm<10) mm = '0' + mm;
	var ss = objD.getSeconds();
	if(ss<10) ss = '0' + ss;
	var ww = objD.getDay();
	if  (ww==0)  ww="星期日";
	if  (ww==1)  ww="星期一";
	if  (ww==2)  ww="星期二";
	if  (ww==3)  ww="星期三";
	if  (ww==4)  ww="星期四";
	if  (ww==5)  ww="星期五";
	if  (ww==6)  ww="星期六";
	str = yy + "-" + MM + "-" + dd + " " + ww;
	document.write(str);
}
function tick(Obj)
{
	var today;
	today = new Date();
	document.getElementById(Obj).innerHTML = showLocale();
	window.setTimeout("tick()", 1000);
}
function Flash(url,w,h,s){
	if (s==1){
	document.write('<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0" width="'+w+'" height="'+h+'"> ');
	document.write('<param name="movie" value="' + url + '">');
	document.write('<param name="quality" value="high"> ');
	document.write('<param name="wmode" value="transparent"> ');
	document.write('<param name="menu" value="false"> ');
	document.write('<embed src="' + url + '" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" width="'+w+'" height="'+h+'" wmode="transparent"></embed> ');
	document.write('</object> ');
	}
	else{
	document.write('<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0" width="'+w+'" height="'+h+'"> ');
	document.write('<param name="movie" value="' + url + '">');
	document.write('<param name="quality" value="high"> ');
	document.write('<param name="menu" value="false"> ');
	document.write('<embed src="' + url + '" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" width="'+w+'" height="'+h+'"></embed> ');
	document.write('</object> ');
	}
}
//加入收藏与设为首页
var hostName = "http://" + window.location.hostname;
var hostHref = window.location.href;
var tt;
var siteName;
$.get(hostHref,
	 {},
	 function(data){
	  tt = data.match(/<title>(.+)<\/title>/);
	  siteName=tt[1];
	 }
	);
function AddFavorite() 
{ 
	try
	{
		window.external.addFavorite(hostName,siteName); 
	}
	catch(e)
	{
		try
		{
			window.sidebar.addPanel(siteName, hostName, ""); 
		}
		catch(e)
		{
			alert("添加收藏夹失败，请手动添加");
		}
	}
}
function SetHomePage()
{
  if(window.netscape)
  {
        try
		{  
          	netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
        }  
        catch (e)  
        {  
			alert("浏览器拒绝了设为首页的操作！");  //地址栏-->about:config,signed.applets.codebase_principal_support=true  
        }
	var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components.interfaces.nsIPrefBranch);
  	prefs.setCharPref('browser.startup.homepage',hostName);
  }
  else
  {
     document.getElementById("homepage").style.behavior='url(#default#homepage)';
   	 document.getElementById("homepage").sethomepage(hostName);
  }
}
function TopCheckForm(){	
	if ($('#Keyword').val()=='' || $('#Keyword').val()=='请输入搜索内容')	
	{		
		alert('请输入搜索内容！');	
		$('#Keyword').focus();	
		return false;	
	};	
	return true;
};
function RemLastChar(Str,char)
{
	var lastindex = Str.lastIndexOf(char)
    if(lastindex>-1)
	{
	  Str = Str.substring(0,lastindex)
	}
	return Str
}
function IndexBox5Tab(id,n){
	$(n+' .IndexRigTit dt').each(function(i){
		if(i == id)
		{
			$(this).addClass("s");			
			$(n+' .con').eq(i).show(); 				
		}else{		
			$(this).removeClass("s");			
			$(n+' .con').eq(i).hide(); 	 	
		}		
	});
}
function IndexBox5TabSL(id,t){
	t.find("a").each(function(i){
		if(id == i){
			$(this).addClass("s");
			t.parent().find("ul").eq(i).show();
		}else{
			$(this).removeClass("s");
			t.parent().find("ul").eq(i).hide();	
		}
	})	
}

//显示蒙灰层
function ShowMark(){
     var xp_mark=document.getElementById("xp_mark");
     if(xp_mark!=null) {
         //设置DIV
         xp_mark.style.left=0+"px";
         xp_mark.style.top=0+"px";
         xp_mark.style.position="absolute";
         xp_mark.style.backgroundColor="#000";
         xp_mark.style.zIndex="190";
         if(document.all) {
            xp_mark.style.filter="alpha(opacity=50)";
            var Ie_ver=navigator["appVersion"].substr(22,1);
           // if(Ie_ver==6||Ie_ver==5){hideSelectBoxes();}
         }
         else{xp_mark.style.opacity="0.5";}
         xp_mark.style.width="100%";
       var heights=XP_getPageSize().h;
       if(heights<600) {
         heights=620;
       }
       xp_mark.style.height=heights+"px";
         xp_mark.style.height=="100%";
         xp_mark.style.display="block";
     }
     else{
     //页面添加div explainDiv,注意必须是紧跟body 内的第一个元素.否则IE6不正常.
     $("body").prepend("<div id='xp_mark' style='display:none;'></div>");
     ShowMark();//继续回调自己
     }              
}
//隐藏蒙灰层
function HideMark(){
    var xp_mark=document.getElementById("xp_mark");
    xp_mark.style.display="none";    
    var Ie_ver=navigator["appVersion"].substr(22,1);
   // if(Ie_ver==6||Ie_ver==5){showSelectBoxes();}
}
//获取页面的高度与宽度
function XP_getPageSize(){
    var pt = {w:0,h:0}; 
    if (window.innerHeight && window.scrollMaxY){  
      pt.w = document.body.scrollWidth;
      pt.h = window.innerHeight + window.scrollMaxY;
    }
    else if (document.body.scrollHeight > document.body.offsetHeight){ // all but Explorer Mac
      pt.w = document.body.scrollWidth;
      pt.h = document.body.scrollHeight;
    } 
    else { // Explorer Mac...would also work in Explorer 6 Strict, Mozilla and Safari
      pt.w = document.body.offsetWidth;
      pt.h = document.body.offsetHeight;
    }
    return pt;
}
//显示所有的下拉列表框
function showSelectBoxes(){
 selects = document.getElementsByTagName("select");
 for (i = 0; i != selects.length; i++) {selects[i].style.visibility = "visible"; }
}

//隐藏所有的下拉列表框
function hideSelectBoxes(){
 selects = document.getElementsByTagName("select");
 for (i = 0; i != selects.length; i++) {selects[i].style.visibility = "hidden";}
}
function showDiv(obj){//页面可以用obj == document.getElementById();
 $(obj).show().css({"z-index":"200","position":"absolute"});
 center(obj);
 $(window).scroll(function(){
  center(obj);
 });
 $(window).resize(function(){
  center(obj);
 }); 
}
function center(obj){//页面可以用obj == document.getElementById();
    var windowWidth = document.documentElement.clientWidth;   
   var windowHeight = document.documentElement.clientHeight;   
   
   var popupHeight =$(obj).height();   
   var popupWidth =$(obj).width();    
   
   $(obj).css({
    "top": (windowHeight-popupHeight-200)/2+$(document).scrollTop()+130,   
    "left": (windowWidth-popupWidth)/2   
   });  
}
 //让层居中隐藏
function closeDiv(obj){
    $(obj).hide();
    $(window).unbind();
}	
//会员注册检查
function checkReg(){
	if(/^\s*$/.test($('#txtUserName').val())){
		alert("用户名不能为空，请输入用户名");
		$('#txtUserName').focus();
		return false;
	}
	if(/^\s*$/.test($('#txtUserPass').val())){
		alert("密码不能为空，请输入密码");
		$('#txtUserPass').focus();
		return false;
	}
	if(document.getElementById('txtUserPass').value != document.getElementById('repeatUserPass').value){
		alert("两次输入的密码不同，请确认");
		$('#txtUserPass').focus();
		return false;
	}
	if(/^\s*$/.test($('#class').val())){
		alert("班级不能为空，请输入班级");
		$('#class').focus();
		return false;
	}
	if(/^\s*$/.test($('#GroupID').val())){
		alert("会员类型不能为空，请输入班级");
		return false;
	}
	if(/^\s*$/.test($('#CheckCode').val())){
		alert("请输入验证码");
		$('#CheckCode').focus();
		return false;
	}
	return true;
}
//会员登录检查
function checkFrom(){
	if(/^\s*$/.test($('#UserName').val())){
		alert("用户名不能为空，请输入用户名");
		$('#UserName').focus();
		return false;
	}
	if(/^\s*$/.test($('#UserPass').val())){
		alert("密码不能为空，请输入密码");
		$('#UserPass').focus();
		return false;
	}
	return true;
}
//重置新闻链接地址
function ResetNewsA(Obj){
  var Url=document.URL;
  var UrlArray=Url.split("/");
  var ColUrl = UrlArray[UrlArray.length-2];
  var ColID=ColUrl.substring(3,ColUrl.length);
  $(Obj+" a").each(function(i){    			  
	  $(this).attr("href","/api/CmsContent.aspx?ColumnID="+ColID+"&NewsID="+GetArtID($(this).attr("href")));			  
  });
}
function GetArtID(Urlhref){
  var UrlIDstr=Urlhref.substring(Urlhref.lastIndexOf("/")+1,Urlhref.lastIndexOf("."));
  return UrlIDstr.substring(UrlIDstr.lastIndexOf("_")+1,UrlIDstr.length);
}
//首页顶部广告图片显示
var h = 0;  
function loadImage(url,callback){
	var img = new Image();
	img.src = url;
	if(img.complete){   
		callback.call(img);
		return;
	}
	img.onload = function(){    
		callback.call(img);
	}
}
function imgLoaded(){
	$("#AdvTop img").attr("src",this.src) ;   
	showImage(100);     
}
function showImage(height){
	if(h < height){
		h = h + 5;
	}else{
		if(parseInt($("#AdvTopParameter").val()) != 0)
		{
			setTimeout("hideImage("+height+")",parseInt($("#AdvTopParameter").val())*1000);  
		}
		return;
	}
	$("#AdvTop").css("display","block");
	$("#AdvTop").css("height",h + "px");
	setTimeout("showImage("+height+")",30);
}
function hideImage(height){
	if(h > 0){
		h = h - 5;
	}else{
		$("#AdvTop").css("display","none");    
		return;
	}
	$("#AdvTop").css("display","block");
	$("#AdvTop").css("height",h + "px");
	setTimeout("hideImage("+height+")",30);
}
//字数限制
function CutString(Str,Num)
{
	var NewCutString = Str;
	if(Str.length>Num)
	{
		NewCutString = Str.substr(0,Num)+"...";
	}
	return NewCutString;
}
//左右广告位
var Class = {
	create: function(){
		return function(){
			this.initialize.apply(this,arguments);
		}
	}
}
var CoupletsAD = Class.create();
CoupletsAD.prototype = {
	initialize:function(CLName,CRName){
		this.CLName=CLName;
		this.CRName=CRName;
		this.lastScrollY_ = 0;
		$("#"+CLName).attr("style","left:5px;position:absolute;top:100px;z-index:1500");
		$("#"+CRName).attr("style","right:5px;position:absolute;top:100px;z-index:1500");
		//document.write("<div id='" + this.CLName + "' style='" + this.CLStyle + "'>" + cLContent + "</div>");
		//document.write("<div id='" + this.CRName + "' style='" + this.CRStyle + "'>" + cRContent + "</div>");
	},
	Load:function(){
		var diffY_;
		if (document.documentElement && document.documentElement.scrollTop){
			diffY_ = document.documentElement.scrollTop;
		} else if (document.body) {
			diffY_ = document.body.scrollTop
		} else {
		}
		percent=.1*(diffY_ - this.lastScrollY_);
		if(percent>0)percent=Math.ceil(percent);
		else percent=Math.floor(percent);
		document.getElementById(this.CLName).style.top=parseInt(document.getElementById(this.CLName).style.top)+percent+"px";
		document.getElementById(this.CRName).style.top=parseInt(document.getElementById(this.CRName).style.top)+percent+"px";
		this.lastScrollY_=this.lastScrollY_+percent;
	},
	Echo:function(){
		alert(this.CLName);
	}
}
var cAD;
function LeftRigAdvInit(){
	if($("#LeftRigAdvData .L").length > 0){
		$("#CoupletLeft").append("<a href='"+$("#LeftRigAdvData .L").find("dd").html()+"'><img src='"+$("#LeftRigAdvData .L").find("dt").html()+"' /></a><div class='Btn'></div>");
		$("#CoupletLeft .Btn").click(function(){
			$(this).parent().hide();
		});
	};
	if($("#LeftRigAdvData .R").length > 0){
		$("#CoupletRig").append("<a href='"+$("#LeftRigAdvData .R").find("dd").html()+"'><img src='"+$("#LeftRigAdvData .R").find("dt").html()+"' /></a><div class='Btn'></div>");
		$("#CoupletRig .Btn").click(function(){
			$(this).parent().hide();
		});	
	};
	if($("#LeftRigAdvData .L").length > 0 || $("#LeftRigAdvData .R").length > 0){
		cAD=new CoupletsAD('CoupletLeft','CoupletRig');
		window.setInterval("cAD.Load()",20); 
	}
	$("#LeftRigAdvData").remove();	
}
function GoIndex() {
    window.location.href = "/index.aspx";
}
$(function(){
    //window.setInterval(GoIndex(), 10000);
    //window.setTimeout("GoIndex()",3*60*1000);
    //var html = $(".position").html();
    //if (html) {
    //    html = html.substring(2,html.length);
    //    $(".position").html(html);
    //}
    var second_a = $(".position a:last");
    second_a.addClass("crt");
    var _crtID = $("#ColumnID").val();
    $(".third a[data=_crtID]").addClass("hover1");
    //搜索
    $("input[name='Keyword']").bind("focus", function () {
        if($(this).val() == this.defaultValue) {
            $(this).val("");
        }
    }).bind("blur", function () {
        if($(this).val() == "") {
            $(this).val(this.defaultValue);
        }
    })

    //tab选项卡
    $(".cnvp-tab-nav>a").bind('mouseenter', function () {
        var tabs = $(this).parent().children("a");
        var selectedclass = getClass(tabs);
        var panels = $(this).parent().parent().children(".cnvp-tab-panle");
        var index = $.inArray(this, tabs);
        if (panels.eq(index)[0]) {
            $(tabs).removeClass(selectedclass)
                .eq(index).addClass(selectedclass);
            $(panels).addClass("cnvp-tabs-hide")
                .eq(index).removeClass("cnvp-tabs-hide");
        }
    });
    String.prototype.trim = function () {
        return this.replace(/(^\s*)|(\s*$)/g, "");
    }
    getClass = function (items) {
        currCls = null;
        items.each(function (i, item) {
            cls = $(item).attr('class');
            if (cls && !cls.trim() == '') {
                currCls = cls;
                return cls;
            }
        });
        return currCls;
    };

    //导航下拉
    $('li.topnavli:eq(1)').find("div").attr("style","padding-left:100px");
    $('li.topnavli:eq(3)').find("div").attr("style","padding-left:70px");
    $('li.topnavli:eq(4)').find("div").attr("style","padding-left:70px");
    $('li.topnavli:eq(5)').find("div").attr("style","padding-left:180px");
    $('li.topnavli:eq(6)').find("div").attr("style","padding-left:380px");
    $('ul.topnav li').mouseenter(function() {
        if($(this).find('div.subnav ul li').length>0) {
            $(this).find('div.subnav').parent().children('a').first().removeClass('top');
            $(this).find('div.subnav').parent().children('a').first().addClass('tophover');
            if(!$(this).find('div.subnav').is(":animated")) {
                $(this).find('div.subnav')
                    .slideDown('fast')
                    .show();
            }
            $(this).hover(function() {
                    if($(this).find('div.subnav ul li').length>0) {
                        $(this).find('div.subnav').parent().children('a').first().removeClass('top');
                        $(this).find('div.subnav').parent().children('a').first().addClass('tophover');
                        if(!$(this).find('div.subnav').is(":animated")) {
                            $(this).find('div.subnav')
                                .slideDown('fast')
                                .show();
                        }
                    }
                },
                function(){
                    $(this).find('div.subnav').parent().children('a').first().removeClass('tophover');
                    $(this).find('div.subnav').parent().children('a').first().addClass('top');
                    $(this).find('div.subnav')
                        .slideUp('fast');
                }).hover(function() {
                    $(this).addClass('subhover');
                }, function(){
                    $(this).removeClass('subhover');
                }
            );
        }
    });
});
//文字上下翻动
var page = 1;
function scrollUAD() {

    var $parent = $("div.link");
    var $l_show = $parent.find("div.link-cnt-list");
    var $l_content = $parent.find("div.link-cnt");
    var l_height = $l_content.height();
    var c_height = $l_show.height();
    var page_count = Math.ceil(c_height / l_height);
    if( !$l_show.is(":animated") ){
        if( page == page_count ){
            $l_show.css("top","0");
            page = 1;
        }else{
            $l_show.animate( { top : '-=' + l_height }, "slow");
//            $l_show.fadeOut("slow",function(){
//                $(this).animate( { top : '-=' + l_height }, "slow");
//            })
            page++;
        }
    }
}
function timestamp(url){
    //  var getTimestamp=Math.random();
    var getTimestamp=new Date().getTime();
    if(url.indexOf("?")>-1){
        url=url+"&timestamp="+getTimestamp
    }else{
        url=url+"?timestamp="+getTimestamp
    }
    return url;
}

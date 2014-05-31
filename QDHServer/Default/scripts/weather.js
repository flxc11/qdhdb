/** WebStorm v0.1.0
 * @author: Merainy
 * @date: 2014/4/18
 * @email: Merainy.a@Gmail.com
 * @description:
 * ========================
 */
function getWeather() {

    $.getJSON("http://query.yahooapis.com/v1/public/yql", {
        q: "select * from json where url=\"http://m.weather.com.cn/data/101210104.html\"",
        format: "json"
    }, function (data) {
        if (data.query.results) {
            //$("#content").text(JSON.stringify(data.query.results));
            var J_data = JSON.parse(JSON.stringify(data.query.results));
            //alert(J_data.weatherinfo.city);
            $("#content").append("<p>"+J_data.weatherinfo.city+"天气预报(数据来源中国天气网)"+"</p>");
            $("#content").append("<p>"+J_data.weatherinfo.date_y+"&nbsp;"+J_data.weatherinfo.week+"&nbsp;"+J_data.weatherinfo.temp1+"&nbsp;"+J_data.weatherinfo.weather1+"&nbsp;"+J_data.weatherinfo.wind1+"&nbsp;"+J_data.weatherinfo.index+"&nbsp;"+J_data.weatherinfo.index_d+"</p>");
            var t= J_data.weatherinfo.date_y;
            t=t.replace("年","/");
            t=t.replace("月","/");
            t=t.replace("日","");

            var tdy = new Date(t);

            var t2 = new Date();


            t2.setDate(tdy.getDate()+1);



            $("#content").append("<p>"+ t2.Format("yyyy年MM月dd日")+"&nbsp;"+getweekdays(t2)+"&nbsp;"+J_data.weatherinfo.temp2+"&nbsp;"+J_data.weatherinfo.weather2+"&nbsp;"+J_data.weatherinfo.wind2+"</p>");

            var t3 = new Date();

            t3.setDate(tdy.getDate()+2);
            $("#content").append("<p>"+t3.Format("yyyy年MM月dd日")+"&nbsp;"+getweekdays(t3)+"&nbsp;"+J_data.weatherinfo.temp3+"&nbsp;"+J_data.weatherinfo.weather3+"&nbsp;"+J_data.weatherinfo.wind3+"</p>");

            var t4 = new Date();

            t4.setDate(tdy.getDate()+3);
            $("#content").append("<p>"+t4.Format("yyyy年MM月dd日")+"&nbsp;"+getweekdays(t4)+"&nbsp;"+J_data.weatherinfo.temp4+"&nbsp;"+J_data.weatherinfo.weather4+"&nbsp;"+J_data.weatherinfo.wind4+"</p>");

            var t5 = new Date();

            t5.setDate(tdy.getDate()+4);
            $("#content").append("<p>"+t5.Format("yyyy年MM月dd日")+"&nbsp;"+getweekdays(t5)+"&nbsp;"+J_data.weatherinfo.temp5+"&nbsp;"+J_data.weatherinfo.weather5+"&nbsp;"+J_data.weatherinfo.wind5+"</p>");

            var t6 = new Date();

            t6.setDate(tdy.getDate()+5);
            $("#content").append("<p>"+t6.Format("yyyy年MM月dd日")+"&nbsp;"+getweekdays(t6)+"&nbsp;"+J_data.weatherinfo.temp6+"&nbsp;"+J_data.weatherinfo.weather6+"&nbsp;"+J_data.weatherinfo.wind6+"</p>");



            //alert(getweekdays(t2));

        } else {
            $("#content").text('no such code: ' + code);
        }
    });

    //$.getJSON("http://m.weather.com.cn/data/101210101.html", null, function(json) { alert(json); });

}

function getweekdays(datey)
{
//    if(datey.getDay()==0)
//    {
//        return "星期日";
//    }
//    else if(datey.getDay()==1)
//    {
//        return "星期一";
//    }
//    else if(datey.getDay()==2)
//    {
//        return "星期二";
//    }
//    else if(datey.getDay()==3)
//    {
//        return "星期三";
//    }
//    else if(datey.getDay()==4)
//    {
//        return "星期四";
//    }
//    else if(datey.getDay()==5)
//    {
//        return "星期五";
//    }
//    else if(datey.getDay()==6)
//    {
//        return "星期六";
//    }

    return "星期" + "日一二三四五六".charAt(new Date().getDay());

}
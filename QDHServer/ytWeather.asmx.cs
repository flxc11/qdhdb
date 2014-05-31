using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace QDHServer
{
    /// <summary>
    /// ytWeather 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class ytWeather : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public DataTable getWeatherAdd(string provice,string city)
        {
            //eg:zhejiang  WenZhou

            DataTable dtResult = new DataTable();
            try
            {

                string postData = "http://qq.ip138.com/weather/"+ provice +"/"+city+".htm";
                string pageHtml = Get_Http(postData);

                dtResult = Get_WeatherData(pageHtml);

            }
            catch
            {
                
            }

            return dtResult;
        }

        /// <summary>
        /// 获取天气信息
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private DataTable Get_WeatherData(string str)
        {
            DataRow dr;
            DataTable dt = new DataTable();
            dt.Columns.Add(new System.Data.DataColumn("rq", typeof(System.String)));  //日期
            dt.Columns.Add(new System.Data.DataColumn("tq", typeof(System.String)));  //天气
            dt.Columns.Add(new System.Data.DataColumn("tqimg", typeof(System.String))); //天气图片
            dt.Columns.Add(new System.Data.DataColumn("qw", typeof(System.String))); //气温
            dt.Columns.Add(new System.Data.DataColumn("fx", typeof(System.String))); //风向

            string rowContent = string.Empty;

            string columnConent = string.Empty;

            str = Search_string(str, "borderColorDark=\"#ffffff\" borderColorLight=\"#008000","</table>");

            string rowPatterm = @"<tr[^>]*>[\s\S]*?<\/tr>";  //每行

            string columnNPattern = @"<th[^>]*>[\s\S]*?<\/td>"; //每列

            string columnPattern = @"<td[^>]*>[\s\S]*?<\/td>"; //每列

            MatchCollection rowCollection;
            MatchCollection columnCollection;
            
            //先生成七行
            for (int k = 0; k < 7; k++)
            {
                dr = dt.NewRow();

                dr[0] = "rq";
                dr[1] = "tq";
                dr[2] = "tqimg";
                dr[3] = "qw";
                dr[4] = "fx";

                dt.Rows.Add(dr);
            }

            //对tr进行筛选
            rowCollection = Regex.Matches(str, rowPatterm, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

            string strtype = "";

            for (int i = 0; i < rowCollection.Count; i++) //行
            {
                rowContent = rowCollection[i].Value;  //列的内容


                if (rowContent.Contains("日期"))
                {

                    //对th进行筛选
                    columnCollection = Regex.Matches(rowContent, columnNPattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                }
                else
                {
                    //对td进行筛选
                    columnCollection = Regex.Matches(rowContent, columnPattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                }

                for (int j = 0; j < columnCollection.Count; j++)  
                {
                    //日期 
                    //天气 图片
                    //气温
                    //风向

                    columnConent = columnCollection[j].Value; //列的内容 

                   

                    if (columnConent.Contains("日期"))
                    {
                        //不处理
                        strtype = "rq";
                    }
                    else if (columnConent.Contains("天气"))
                    {
                        //不处理
                        strtype = "tq";
                    }
                    else if (columnConent.Contains("气温"))
                    {
                        //不处理
                        strtype = "qw";
                    }
                    else if (columnConent.Contains("风向"))
                    {
                        //不处理
                        strtype = "fx";
                    }
                    else
                    {
                        if (strtype != "tq")
                        {
                            dt.Rows[j - 1][strtype] = StripHT(columnConent.ToString());  //替代
                        }
                        else
                        {
                            dt.Rows[j - 1]["tq"] = Search_string(columnConent.ToString(), "<br/>", "</td>");

                            string strimg = columnConent.ToString().Replace("<br/>", "");  
                            //<img src="/image/b3.gif" alt="阵雨" /><br/>阵雨
                            string img = "http://qq.ip138.com" + StripHT(Search_string(strimg, "src=\"", "\" alt"));  //图片
                            dt.Rows[j - 1]["tqimg"] = img;
                        }
                    }
                }
            }

            

            return dt;
        }

        /// <summary>
        ///从html中提取纯文本
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        private string StripHT(string strHtml)
        {
            Regex regex = new Regex("<.+?>", RegexOptions.IgnoreCase);
            string strOutput = regex.Replace(strHtml, "");//替换掉"<"和">"之间的内容
            strOutput = strOutput.Replace("<", "");
            strOutput = strOutput.Replace(">", "");
            strOutput = strOutput.Replace(" ", "");
            return strOutput;
        }


        /// <summary>
        /// 获取两个字符串中间的字符串
        /// </summary>
        /// <param name="s">总字符串</param>
        /// <param name="s1">开始位置</param>
        /// <param name="s2">结束位置</param>
        /// <returns></returns>
        private string Search_string(string s, string s1, string s2)
        {
            int n1, n2;
            n1 = s.IndexOf(s1, 0) + s1.Length;   //开始位置 
            n2 = s.IndexOf(s2, n1);               //结束位置 
            return s.Substring(n1, n2 - n1);
        }

        //以GET方式抓取远程页面内容
        private string Get_Http(string tUrl)
        {
            string strResult;
            try
            {
                HttpWebRequest hwr = (HttpWebRequest)HttpWebRequest.Create(tUrl);
                hwr.Timeout = 19600;
                HttpWebResponse hwrs = (HttpWebResponse)hwr.GetResponse();
                Stream myStream = hwrs.GetResponseStream();
                StreamReader sr = new StreamReader(myStream, Encoding.Default);
                StringBuilder sb = new StringBuilder();
                while (-1 != sr.Peek())
                {
                    sb.Append(sr.ReadLine() + "\r\n");
                }
                strResult = sb.ToString();
                hwrs.Close();
            }
            catch (Exception ee)
            {
                strResult = ee.Message;
            }
            return strResult;
        }
    }

    
}

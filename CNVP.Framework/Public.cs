using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CNVP.Framework.Helper;
using Microsoft.VisualBasic;
using System.Net;
using System.IO;
using System.Web;

namespace CNVP.Framework.Utils
{
    public class Public
    {
        #region "获取用户地址"
        /// <summary>
        /// 获取用户IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetUserIP()
        {
            string _UserIP = "";
            try
            {
                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    _UserIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else
                {
                    _UserIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                }
            }
            catch
            { 
            }
            return _UserIP;
        }
        #endregion
        #region "比较字符相似"
        /// <summary>
        /// 比较两个字符串相似度
        /// </summary>
        /// <param name="fromString">原始值</param>
        /// <param name="toString">比较值</param>
        /// <returns>差异值，返回0表示一致</returns>
        public static int CompareStrings(string fromString, string toString)
        {
            var fLength = fromString.Length;
            var tLength = toString.Length;

            if (fLength == 0)
            {
                return tLength;
            }
            if (tLength == 0)
            {
                return fLength;
            }
            var martix = new int[fLength + 1, tLength + 1];
            for (int i = 0; i <= fLength; i++)
            {
                martix[i, 0] = i;
            }

            for (int j = 0; j <= tLength; j++)
            {
                martix[0, j] = j;
            }
            for (int i = 1; i <= fLength; i++)
            {
                var tempF = fromString[i - 1];
                var cost = 0;
                for (int j = 1; j <= tLength; j++)
                {
                    var tempT = toString[j - 1];
                    if (tempT == tempF)
                    {
                        cost = 0;
                    }
                    else
                    {
                        cost = 1;
                    }

                    var valueAbove = martix[i - 1, j] + 1;
                    var valueLeft = martix[i, j - 1] + 1;
                    var valueDiag = martix[i - 1, j - 1] + cost;

                    var cellValue = valueAbove < valueLeft ? (valueDiag < valueAbove ? valueDiag : valueAbove) : (valueDiag < valueLeft ? valueDiag : valueLeft);
                    martix[i, j] = cellValue;
                }
            }

            var result = martix[fLength, tLength];

            return result;
        }
        #endregion
        #region "过滤特殊符号"
        /// <summary>
        /// 过滤特殊特号(完全过滤)
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string FilterSql(string Str)
        {
            string[] aryReg = { "'", "\"", "\r", "\n", "<", ">", "%", "?", ",", "=", "-", "_", ";", "|", "[", "]", "&", "/" };
            if (!string.IsNullOrEmpty(Str))
            {
                foreach (string str in aryReg)
                {
                    Str = Str.Replace(str, string.Empty);
                }
                return Str;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// Json特符字符过滤，参见http://www.json.org/
        /// </summary>
        /// <param name="sourceStr">要过滤的源字符串</param>
        /// <returns>返回过滤的字符串</returns>
        public static string JsonCharFilter(string sourceStr)
        {
            sourceStr = sourceStr.Replace("\\", "\\\\");
            sourceStr = sourceStr.Replace("\b", "\\\b");
            sourceStr = sourceStr.Replace("\t", "\\\t");
            sourceStr = sourceStr.Replace("\n", "\\\n");
            sourceStr = sourceStr.Replace("\n", "\\\n");
            sourceStr = sourceStr.Replace("\f", "\\\f");
            sourceStr = sourceStr.Replace("\r", "\\\r");
            return sourceStr.Replace("\"", "\\\"");
        }
        #endregion
        #region "字符格式检查"
        /// <summary>
        /// 固定电话格式判断
        /// </summary>
        /// <param name="TelPhone">固定电话</param>
        /// <returns></returns>
        public static bool IsTelphone(string TelPhone)
        {
            return Regex.IsMatch(TelPhone, @"^(\d{3,4}-)?\d{6,8}$");
        }
        /// <summary>
        /// 手机号码格式判断
        /// </summary>
        /// <param name="Mobile">手机号码</param>
        /// <returns></returns>
        public static bool IsMobile(string Mobile)
        {
            return Regex.IsMatch(Mobile, @"^[1]+[3,5]+\d{9}");
        }
        /// <summary>
        /// 身份证号码判断
        /// </summary>
        /// <param name="IDCard">身份证号码</param>
        /// <returns></returns>
        public static bool IsIDCard(string IDCard)
        {
            return Regex.IsMatch(IDCard, @"(^\d{18}$)|(^\d{15}$)");
        }
        /// <summary>
        /// 数字格式判断
        /// </summary>
        /// <param name="Number">数字</param>
        /// <returns></returns>
        public static bool IsNumber(string Number)
        {
            return Regex.IsMatch(Number, @"^[0-9]*$");
        }
        /// <summary>
        /// 邮政编码格式判断
        /// </summary>
        /// <param name="PostCode">邮政编码</param>
        /// <returns></returns>
        public static bool IsPostCode(string PostCode)
        {
            return Regex.IsMatch(PostCode, @"^\d{6}$");
        }
        /// <summary>
        /// 邮件地址格式判断
        /// </summary>
        /// <param name="Email">邮件地址</param>
        /// <returns></returns>
        public static bool IsEmail(string Email)
        {
            return Regex.IsMatch(Email, @"\\w{1,}@\\w{1,}\\.\\w{1,}");
        }
        #endregion
        #region "合并数据表格"
        /// <summary>
        /// 合并DataTable
        /// </summary>
        /// <param name="DataTable1">表1</param>
        /// <param name="DataTable2">表2</param>
        /// <param name="DTName">合并后新表的名称</param>
        /// <returns>合并后的新表</returns>
        public static DataTable MergerDt(DataTable DataTable1, DataTable DataTable2, string DTName)
        {
            //克隆DataTable1的结构
            DataTable newDataTable = DataTable1.Clone();
            for (int i = 0; i < DataTable2.Columns.Count; i++)
            {
                //再向新表中加入DataTable2的列结构
                newDataTable.Columns.Add(DataTable2.Columns[i].ColumnName);
            }
            object[] obj = new object[newDataTable.Columns.Count];
            //添加DataTable1的数据
            for (int i = 0; i < DataTable1.Rows.Count; i++)
            {
                DataTable1.Rows[i].ItemArray.CopyTo(obj, 0);
                newDataTable.Rows.Add(obj);
            }
            if (DataTable1.Rows.Count >= DataTable2.Rows.Count)
            {
                for (int i = 0; i < DataTable2.Rows.Count; i++)
                {
                    for (int j = 0; j < DataTable2.Columns.Count; j++)
                    {
                        newDataTable.Rows[i][j + DataTable1.Columns.Count] = DataTable2.Rows[i][j].ToString();
                    }
                }
            }
            else
            {
                DataRow dr3;
                //向新表中添加多出的几行
                for (int i = 0; i < DataTable2.Rows.Count - DataTable1.Rows.Count; i++)
                {
                    dr3 = newDataTable.NewRow();
                    newDataTable.Rows.Add(dr3);
                }
                for (int i = 0; i < DataTable2.Rows.Count; i++)
                {
                    for (int j = 0; j < DataTable2.Columns.Count; j++)
                    {
                        newDataTable.Rows[i][j + DataTable1.Columns.Count] = DataTable2.Rows[i][j].ToString();
                    }
                }
            }
            newDataTable.TableName = DTName; //设置DT的名字
            return newDataTable;
        }
        #endregion
        #region "户号的格式化"
        /// <summary>
        /// 格式化字符串(后面补上空格)
        /// </summary>
        /// <param name="Str">字符串</param>
        /// <param name="Length">规定长度</param>
        /// <returns></returns>
        public static string FormatString(string Str, int Length)
        {
            int _Length = Length - Str.Length;
            string _SplitStr = "";
            for (int i = 0; i < _Length; i++)
            {
                _SplitStr += " ";
            }
            return Str + _SplitStr;
        }
        /// <summary>
        /// 格式化数字(位数不足前面补0)
        /// </summary>
        /// <param name="Number">金额(保留小数点2位)</param>
        /// <param name="Length">规定长度</param>
        /// <returns></returns>
        public static string FormatNumber(string Str, int Length)
        {
            int _Length = Length - Str.ToString().Length;
            string _SplitStr = "";
            for (int i = 0; i < _Length; i++)
            {
                _SplitStr += "0";
            }
            return _SplitStr + Str;
        }
        #endregion
        #region "根据类型转换"
        /// <summary>
        /// 根据类型转换值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static object GetDefaultValue(object obj, Type type)
        {
            try
            {
                if (obj == null || obj == DBNull.Value)
                {
                    obj = default(object);
                }
                else
                {
                    if (type == typeof(String))
                        obj = obj.ToString().Trim();
                    obj = Convert.ChangeType(obj, Nullable.GetUnderlyingType(type) ?? type);
                }
                return obj;
            }
            catch
            {
                return null;
            }
        }
        #endregion
        #region "获取货币差值"
        /// <summary>
        /// 获取货币差值
        /// </summary>
        /// <param name="Money1">金额1</param>
        /// <param name="Money2">金额2</param>
        /// <returns></returns>
        public static string GetMoney(string Money1, string Money2)
        {
            string Str=string.Empty;
            if (string.IsNullOrEmpty(Money1))
            {
                Money1 = "0";
            }
            if (string.IsNullOrEmpty(Money2))
            {
                Money2 = "0";
            }

            Str = (Convert.ToDecimal(Money1) - Convert.ToDecimal(Money2)).ToString();
            return Str;
        }
        #endregion
        #region "日期时间格式"
        /// <summary>
        /// 返回标准日期(年-月-日)
        /// </summary>
        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 返回标准日期(年-月-日)
        /// </summary>
        /// <param name="dataTime">日期时间</param>
        /// <returns></returns>
        public static string GetDate(string dataTime)
        {
            string Str = dataTime;
            if (!string.IsNullOrEmpty(dataTime))
            {
                try
                {
                    Str = Convert.ToDateTime(dataTime).ToString("yyyy-MM-dd");
                }
                catch
                {

                }
            }
            return Str;
        }
        /// <summary>
        /// 返回标准时间(时-分-秒)
        /// </summary>
        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }
        /// <summary>
        /// 返回标准时间(年-月-日 时-分-秒)
        /// </summary>
        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 返回标准时间(年-月-日 时-分-秒)
        /// </summary>
        /// <param name="dataTime">日期时间</param>
        /// <returns></returns>
        public static string GetDateTime(string dataTime)
        {
            string Str = dataTime;
            if (!string.IsNullOrEmpty(dataTime))
            {
                try
                {
                    Str = Convert.ToDateTime(dataTime).ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch
                {
                }
            }
            return Str;
        }
        /// <summary>
        /// 返回标准时间(四位年)
        /// </summary>
        /// <param name="dataTime"></param>
        /// <returns></returns>
        public static string GetYear(string dataTime)
        {
            string Str = dataTime;
            if (!string.IsNullOrEmpty(dataTime))
            {
                try
                {
                    Str = Convert.ToDateTime(dataTime).ToString("yyyy");
                }
                catch
                {
                }
            }
            return Str;
        }
        /// <summary>
        /// 返回标准时间(两位月)
        /// </summary>
        /// <param name="dataTime"></param>
        /// <returns></returns>
        public static string GetMonth(string dataTime)
        {
            string Str = dataTime;
            if (!string.IsNullOrEmpty(dataTime))
            {
                try
                {
                    Str = Convert.ToDateTime(dataTime).ToString("MM");
                }
                catch
                {
                }
            }
            return Str;
        }
        /// <summary>
        /// 返回月份间隔
        /// </summary>
        /// <param name="dataTime1">起始时间</param>
        /// <param name="dataTime2">结束时间</param>
        /// <returns></returns>
        public static string GetMonth(string dataTime1, string dataTime2)
        {
            int Month = 0;
            DateTime dtbegin = Convert.ToDateTime(dataTime1);
            DateTime dtend = Convert.ToDateTime(dataTime2);
            Month = (dtend.Year - dtbegin.Year) * 12 + (dtend.Month - dtbegin.Month);
            return Month.ToString();
        }
        /// <summary>
        /// 返回标准时间(两位日)
        /// </summary>
        /// <param name="dataTime"></param>
        /// <returns></returns>
        public static string GetDay(string dataTime)
        {
            string Str = dataTime;
            if (!string.IsNullOrEmpty(dataTime))
            {
                try
                {
                    Str = Convert.ToDateTime(dataTime).ToString("dd");
                }
                catch
                {
                }
            }
            return Str;
        }
        /// <summary>
        /// 返回标准时间(两位时)
        /// </summary>
        /// <param name="dataTime"></param>
        /// <returns></returns>
        public static string GetHour(string dataTime)
        {
            string Str = dataTime;
            if (!string.IsNullOrEmpty(dataTime))
            {
                try
                {
                    Str = Convert.ToDateTime(dataTime).ToString("HH");
                }
                catch
                {
                }
            }
            return Str;
        }
        /// <summary>
        /// 返回标准时间(两位分)
        /// </summary>
        /// <param name="dataTime"></param>
        /// <returns></returns>
        public static string GetMinute(string dataTime)
        {
            string Str = dataTime;
            if (!string.IsNullOrEmpty(dataTime))
            {
                try
                {
                    Str = Convert.ToDateTime(dataTime).ToString("mm");
                }
                catch
                {
                }
            }
            return Str;
        }
        /// <summary>
        /// 返回标准时间(两位秒)
        /// </summary>
        /// <param name="dataTime"></param>
        /// <returns></returns>
        public static string GetSecond(string dataTime)
        {
            string Str = dataTime;
            if (!string.IsNullOrEmpty(dataTime))
            {
                try
                {
                    Str = Convert.ToDateTime(dataTime).ToString("ss");
                }
                catch
                {
                }
            }
            return Str;
        }
        /// <summary>
        /// 返回标准时间(年-月-日 时-分-秒-毫秒)
        /// </summary>
        public static string GetDateTimeF()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffffff");
        }
        /// <summary>
        /// 返回标准时间(年-月-日 时-分-秒)
        /// </summary>
        /// <param name="Days">相对天数</param>
        /// <returns></returns>
        public static string GetDateTime(int Days)
        {
            return DateTime.Now.AddDays(Days).ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 返回相对时间
        /// </summary>
        /// <param name="fDateTime">相对时间</param>
        /// <param name="formatStr">日期时间格式</param>
        /// <returns></returns>
        public static string GetStandardDateTime(string fDateTime, string formatStr)
        {
            if (fDateTime == "0000-0-0 0:00:00")
                return fDateTime;
            DateTime time = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            if (DateTime.TryParse(fDateTime, out time))
                return time.ToString(formatStr);
            else
                return "N/A";
        }
        /// <summary>
        /// 返回标准时间 yyyy-MM-dd HH:mm:ss
        /// </sumary>
        public static string GetStandardDateTime(string fDateTime)
        {
            return GetStandardDateTime(fDateTime, "yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 返回标准时间 yyyy-MM-dd
        /// </sumary>
        public static string GetStandardDate(string fDate)
        {
            return GetStandardDateTime(fDate, "yyyy-MM-dd");
        }
        #endregion
        #region "字母转成大写"
        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="str">输入字符串</param>
        /// <returns>首字母大写</returns>
        public static string GetinitialUpper(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = Strings.StrConv(str, VbStrConv.ProperCase,
                    System.Globalization.CultureInfo.CurrentCulture.LCID);
            }
            return str;
        }
        #endregion
        #region "转成汉字数字"
        /// <summary>
        /// 转成汉字数字
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static string ConvertToChinese(double x) 
        { 
            string s = x.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A"); string d = Regex.Replace(s, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}"); return Regex.Replace(d, ".", delegate(Match m) { return "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万亿兆京垓秭穰"[m.Value[0] - '-'].ToString(); });
        }
        /// <summary>
        /// 转成汉字数字
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static string ConvertToChinese(int x)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(0, "零"); 
            dic.Add(1, "一"); 
            dic.Add(2, "二"); 
            dic.Add(3, "三"); 
            dic.Add(4, "四"); 
            dic.Add(5, "五"); 
            dic.Add(6, "六"); 
            dic.Add(7, "七"); 
            dic.Add(8, "八"); 
            dic.Add(9, "九"); 

            string svalue = string.Empty;
            foreach (int key in dic.Keys)
            {
                if (key == x)
                {
                    svalue = dic[key];
                    break;
                }
            }
            return svalue;
        }
        #endregion
        #region "模拟表单提交"
        /// <summary>
        /// 模拟表单提交
        /// </summary>
        /// <param name="PostUrl">远程地址</param>
        /// <param name="Param">表单参数(Action=Login&UserName=Admin&UserPass=Admin)</param>
        /// <returns></returns>
        public static string PostData(string PostUrl, string Param)
        {
            try
            {
                byte[] data = System.Text.Encoding.GetEncoding("Gb2312").GetBytes(Param);
                // 准备请求 
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(PostUrl);

                //设置超时
                req.Timeout = 30000;
                req.Method = "Post";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = data.Length;
                Stream stream = req.GetRequestStream();
                // 发送数据 
                stream.Write(data, 0, data.Length);
                stream.Close();

                HttpWebResponse rep = (HttpWebResponse)req.GetResponse();
                Stream receiveStream = rep.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("GB2312");
                StreamReader readStream = new StreamReader(receiveStream, encode);

                Char[] read = new Char[256];
                int count = readStream.Read(read, 0, 256);
                StringBuilder sb = new StringBuilder("");
                while (count > 0)
                {
                    String readstr = new String(read, 0, count);
                    sb.Append(readstr);
                    count = readStream.Read(read, 0, 256);
                }

                rep.Close();
                readStream.Close();

                return sb.ToString();

            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion
        #region "获取配置文件"
        /// <summary>
        /// 获取配置文件
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }
        #endregion
    }
}
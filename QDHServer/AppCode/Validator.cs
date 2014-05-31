using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace QDHServer.AppCode
{
    public class Validator
    {
        /// <summary>
        /// 检查一个字符串是否可以转化为日期，一般用于验证用户输入日期的合法性。
        /// </summary>
        /// <param name="_value">需验证的字符串。</param>
        /// <returns>是否可以转化为日期的bool值。</returns>
        public static bool IsStringDate(string _value)
        {
            DateTime dt;
            try
            {
                dt = DateTime.Parse(_value);
            }
            catch (FormatException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 把字符串转成日期
        /// </summary>
        /// <param name="_value">字符串</param>
        /// <param name="_defValue">默认值</param>
        /// <returns></returns>
        public static DateTime StrToDate(string _value, DateTime _defValue)
        {
            try
            {
                return DateTime.Parse(_value);
            }
            catch (FormatException)
            {
                return _defValue;
            }
        }

        /// <summary>
        /// 把字符串转成日期,默认值为当前时间
        /// </summary>
        /// <param name="_value">字符串</param>
        /// <returns></returns>
        public static DateTime StrToDate(string _value)
        {
            return StrToDate(_value, DateTime.Now);
        }

        /// <summary>
        /// 把字符串转成整型
        /// </summary>
        /// <param name="_value">字符串</param>
        /// <param name="_defValue">默认值</param>
        /// <returns></returns>
        public static int StrToInt(string _value, int _defValue)
        {
            try
            {
                return int.Parse(_value);
            }
            catch (FormatException)
            {
                return _defValue;
            }
        }

        /// <summary>
        /// 把字符串转成整型,默认值为0
        /// </summary>
        /// <param name="_value">字符串</param>
        /// <returns></returns>
        public static int StrToInt(string _value)
        {
            return StrToInt(_value, 0);
        }

        /// <summary>
        /// string转decimal
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static decimal StrToDecimal(string _value)
        {
            decimal res;
            decimal.TryParse(_value, out res);
            return res;
        }

        /// <summary>
        /// 快速验证一个字符串是否符合指定的正则表达式。
        /// </summary>
        /// <param name="_express">正则表达式的内容。</param>
        /// <param name="_value">需验证的字符串。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool QuickValidate(string _express, string _value)
        {
            if (_value == null) return false;
            Regex myRegex = new Regex(_express);
            if (_value.Length == 0)
            {
                return false;
            }
            return myRegex.IsMatch(_value);
        }

        /// <summary>
        /// 检查一个字符串是否是纯数字构成的，一般用于查询字符串参数的有效性验证。
        /// </summary>
        /// <param name="_value">需验证的字符串。。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool IsNumberId(string _value)
        {
            return Validator.QuickValidate("^[1-9]*[0-9]*$", _value);
        }

        /// <summary>
        /// 获取正确的Id
        /// </summary>
        /// <param name="_value">字符串</param>
        /// <returns></returns>
        public static int StrToId(string _value)
        {
            if (IsNumberId(_value))
                return int.Parse(_value);
            else
                return 0;
        }

        /// <summary>
        /// 检查一个字符串是否是纯字母和数字构成的，一般用于查询字符串参数的有效性验证。
        /// </summary>
        /// <param name="_value">需验证的字符串。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool IsLetterOrNumber(string _value)
        {
            return Validator.QuickValidate("^[a-zA-Z0-9_]*$", _value);
        }

        /// <summary>
        /// 判断是否是数字，包括小数和整数。
        /// </summary>
        /// <param name="_value">需验证的字符串。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool IsNumber(string _value)
        {
            return Validator.QuickValidate("^(0|([1-9]+[0-9]*))(.[0-9]+)?$", _value);
        }
    }
}

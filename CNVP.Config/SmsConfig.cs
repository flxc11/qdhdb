using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CNVP.Config
{
    public class SmsConfig
    {
        /// <summary>
        /// 配置文件地址
        /// </summary>
        //private static string _FilePath = HttpContext.Current.Server.MapPath("~/Config/YongDian.Sms.config");
        private static string _FilePath = Utils.GetMapPath("/Config/YongDian.Sms.config");
        private static void SetFilePath()
        {
            BaseConfig.FilePath = _FilePath;
        }

        /// <summary>
        /// WebService接口地址
        /// </summary>
        public static string Url
        {
            get
            {
                SetFilePath();
                return BaseConfig.GetConfigValue("AppUrl");
            }
        }

        /// <summary>
        /// WebService应用帐号
        /// </summary>
        public static string ApplicationID
        {
            get
            {
                SetFilePath();
                return BaseConfig.GetConfigValue("AppID");
            }
        }

        /// <summary>
        /// WebService应用密码
        /// </summary>
        public static string Password
        {
            get
            {
                SetFilePath();
                return BaseConfig.GetConfigValue("AppKey");
            }
        }

        /// <summary>
        /// 用户注册短信校验码
        /// </summary>
        public static string GetUserRegMsg
        {
            get
            {
                SetFilePath();
                return BaseConfig.GetConfigValue("UserRegMsg");
            }
        }

        /// <summary>
        /// 用户找回密码校验码
        /// </summary>
        public static string GetUserForgotPwdMsg
        {
            get
            {
                SetFilePath();
                return BaseConfig.GetConfigValue("UserForgotPwdMsg");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CNVP.Config
{
    public class AppConfig
    {
        /// <summary>
        /// 配置文件地址
        /// </summary>
        //private static string _FilePath = HttpContext.Current.Server.MapPath("~/Config/YongDian.App.config");
        private static string _FilePath = Utils.GetMapPath("/Config/YongDian.App.config");
        private static void SetFilePath()
        {
            BaseConfig.FilePath = _FilePath;
        }

        /// <summary>
        /// 获取应用程序地址
        /// </summary>
        public static string AppUrl
        {
            get
            {
                SetFilePath();
                return BaseConfig.GetConfigValue("AppUrl");
            }
        }

        /// <summary>
        /// 获取应用程序帐号
        /// </summary>
        public static string AppID
        {
            get
            {
                SetFilePath();
                return BaseConfig.GetConfigValue("AppID");
            }
        }

        /// <summary>
        /// 获取应用程序密码
        /// </summary>
        public static string AppKey
        {
            get
            {
                SetFilePath();
                return BaseConfig.GetConfigValue("AppKey");
            }
        }
    }
}
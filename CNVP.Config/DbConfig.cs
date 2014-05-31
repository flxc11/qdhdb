using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CNVP.Config
{
    public class DbConfig
    {
        /// <summary>
        /// 配置文件地址
        /// </summary>
        //private static string _FilePath = HttpContext.Current.Server.MapPath("~/Config/CNVP.Conn.config");
        private static string _FilePath = Utils.GetMapPath("/Config/CNVP.Conn.config");
        private static void SetFilePath()
        {
            BaseConfig.FilePath = _FilePath;
        }
        public static string TemplateUploadFile
        {
            get
            {
                SetFilePath();
                return BaseConfig.GetConfigValue("UploadFile");
            }
        }
        /// <summary>
        /// 数据库链接
        /// </summary>
        public static string DbConn
        {
            get
            {
                SetFilePath();
                return BaseConfig.GetConfigValue("DbConn");
            }
        }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public static string DbType
        {
            get
            {
                SetFilePath();
                return BaseConfig.GetConfigValue("DbType");
            }
        }
        /// <summary>
        /// 数据表前缀
        /// </summary>
        public static string Prefix
        {
            get
            {
                SetFilePath();
                return BaseConfig.GetConfigValue("Prefix");
            }
        }
    }
}
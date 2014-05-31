using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QDHConfig
{
    public class BaseConfig
    {
        #region  广告 sgr
        /*数据库连接*/
        public static string DataConnectString()
        {
            return "server=(local);uid=sa;pwd=hon*gd0ong;database=qdhdb;";
            //return "server=122.228.134.30;uid=qdh;pwd=qdh2014;database=qdhdb;";
        }

        /*监控*/
        public static string AD_Address_Details_JK()
        {
            return "";
        }
        #endregion 广告
    }
}

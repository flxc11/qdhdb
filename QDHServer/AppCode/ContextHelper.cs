using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;

namespace QDHServer.AppCode
{
    public class ContextHelper
    {
        public static HttpContext Current
        {
            get { return HttpContext.Current; }
        }

        public static HttpRequest Request
        {
            get { return Current.Request; }
        }

        public static HttpResponse Response
        {
            get { return Current.Response; }
        }

        /// <summary>
        /// 等于Request.QueryString;
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string Q(string name)
        {
            return Request.QueryString[name] == null ? "" : Request.QueryString[name];
        }

        /// <summary>
        /// 获取url中的id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int QId(string name)
        {
            return Validator.StrToId(Q(name));
        }

        public static void ShowMessage(string strMsg)
        {
            Response.Write("<script>alert(\"" + strMsg + "\");</script>");
        }
    }
}

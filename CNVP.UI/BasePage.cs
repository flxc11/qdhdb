using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using CNVP.Config;
using CNVP.Data;
using CNVP.Framework.Helper;
using CNVP.Framework.Utils;
using CNVP.Model;

namespace CNVP.UI
{
    public class BasePage : System.Web.UI.Page
    {
        /// <summary>
        /// 帐号序号
        /// </summary>
        public int AdminID { get; set; }
        /// <summary>
        /// 登录帐号
        /// </summary>
        public string AdminName { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string AdminTrueName { get; set; }
        /// <summary>
        /// 用户序号
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 用户帐号
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string UserTrueName { get; set; }
        /// <summary>
        /// 当前路径
        /// </summary>
        public static string CurrentPath = "<img src=\"../Images/UsresIcon.jpg\" align=\"absmiddle\"> 目前您在：<a href=\"../Index.aspx\" class=\"link-666\">首页</a> &gt; <a href=\"Index.aspx\" class=\"link-666\">用户中心</a> &gt; ";

        /// <summary>
        ///  页面访问前调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreInit(object sender, EventArgs e)
        {

        }
        #region "操作提示"
        /// <summary>
        /// 操作提示
        /// </summary>
        /// <param name="Title">提示文字</param>
        /// <param name="Url">返回地址</param>
        /// <param name="CssClass">CSS样式</param>
        public void Message(string Title, string Url, string CssClass)
        {
            string Str = string.Format("this.jsprint(\"{0}\", \"{1}\", \"{2}\")", Title, Url, CssClass);
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", Str, true);
        }
        /// <summary>
        /// 操作提示(带回传函数)
        /// </summary>
        /// <param name="Title">提示文字</param>
        /// <param name="Url">返回地址</param>
        /// <param name="CssClass">CSS样式</param>
        /// <param name="CallBack">JS回调函数</param>
        public void Message(string Title, string Url, string CssClass, string CallBack)
        {
            string msbox = "parent.jsprint(\"" + Title + "\", \"" + Url + "\", \"" + CssClass + "\", " + CallBack + ")";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        }
        /// <summary>
        /// 对话框提示
        /// </summary>
        /// <param name="Title">提示文字</param>
        protected void Alert(string Title)
        {
            string msbox = "$.ligerDialog.warn('" + Title + "');";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsAlert", msbox, true);
        }
        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="TabID">窗口序号</param>
        protected void Close(string TabID)
        {
            StringBuilder Str = new StringBuilder();
            Str.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n");
            Str.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n");
            Str.Append("<head>\r\n");
            Str.Append("<title></title>\r\n");
            Str.Append("<link href=\"../Scripts/Themes/Aqua/Css/Ligerui-All.css\" rel=\"stylesheet\" type=\"text/css\" />\r\n");
            Str.Append("<link href=\"../Css/Style.css\" rel=\"stylesheet\" type=\"text/css\" />\r\n");
            Str.Append("<script language=\"javascript\" type=\"text/javascript\" src=\"../Scripts/JQuery.js\"></script>\r\n");
            Str.Append("<script language=\"javascript\" type=\"text/javascript\" src=\"../Scripts/Common.js\"></script>\r\n");
            Str.Append("<script language=\"javascript\" type=\"text/javascript\" src=\"../Scripts/LigerBuild.js\"></script>\r\n");
            Str.Append("<script language=\"javascript\" type=\"text/javascript\" src=\"System.js\"></script>\r\n");
            Str.Append("</head>\r\n");
            Str.Append("<body class=\"mainbody\">\r\n");
            Str.Append("<script language=\"javascript\" type=\"text/javascript\">\r\n");
            Str.Append(string.Format("alert('对不起，该模块您没有操作权限！');parent.f_closeTab('{0}');", TabID));
            Str.Append("</script>");
            Str.Append("</body>\r\n");
            Str.Append("</html>");
            Response.Write(Str.ToString());
            Response.End();
        }
        /// <summary>
        /// 对话框提示
        /// </summary>
        /// <param name="JsStr">Js脚本</param>
        protected void Select(string JsStr)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "JsSelect", JsStr, true);
        }
        #endregion
    }
}
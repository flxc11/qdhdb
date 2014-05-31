using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace QDHServer.AppCode
{
    public class sysPublic
    {
        public sysPublic()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public static void IsOnLine()
        {
            try
            {
                if (HttpContext.Current.Session["IsOnLine"] == null)// || HttpContext.Current.Session["IsOnLine"].ToString().Equals(""))
                {
                    HttpContext.Current.Session.Clear();
                    HttpContext.Current.Session.Abandon();
                    //HttpContext.Current.Response.Write("<script language='javascript'>window.parent.location='" + HttpContext.Current.Request.Url + "/login.aspx' </script>");
                    //HttpContext.Current.Response.Write("<script language='javascript'>window.parent.location='/login.aspx' </script>");
                    HttpContext.Current.Response.Redirect("/NewRedirect.aspx");//HttpContext.Current.Request.ApplicationPath + 
                    //HttpContext.Current.Response.Write("<script language='javascript' type=\"text/javascript\">window.parent.location='" + HttpContext.Current.Request.ApplicationPath + "/login.aspx' </script>");
                    return;
                }
            }
            catch (Exception)
            {
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
                //HttpContext.Current.Response.Write("<script language='javascript'>window.parent.location='" + HttpContext.Current.Request.ApplicationPath + "/login.aspx' </script>");
                HttpContext.Current.Response.Redirect("/NewRedirect.aspx");//HttpContext.Current.Request.ApplicationPath + 
                return;
            }


        }
        public static void ExectueScript(string sScript)
        {
            HttpResponse response = HttpContext.Current.Response;
            sScript = sScript.Replace("\n", "\\n");
            sScript = sScript.Replace("\"", "'");
            response.Write("<script language=\"javascript\" type=\"text/javascript\">" + sScript + "</script>");
        }
        public static void ShowMsgBox(string sMsg)
        {
            HttpResponse response = HttpContext.Current.Response;
            sMsg = sMsg.Replace("\n", "\\n");
            sMsg = sMsg.Replace("\"", "'");
            response.Write("<script language=\"javascript\" type=\"text/javascript\">window.alert(\"" + sMsg + "\");</script>"); ;
        }

        //public static bool IsLimit(string sResId)
        //{
        //    SQLHelper sh = new SQLHelper();
        //    return sh.GetLimit(sResId);
        //    //return true;
        //}

        public static string GetAreaSql(bool Up_Down)
        {
            string strSQL = "";
            if (Up_Down)
            {
                strSQL = "select * from tAreaData start with area_id='" + HttpContext.Current.Session["CurrSelectAreaId"] + "' connect by prior area_parent_id = area_id ";
            }
            else
            {
                strSQL = "select * from tAreaData start with area_id='" + HttpContext.Current.Session["CurrSelectAreaId"] + "' connect by prior area_id = area_parent_id ";
            }
            return strSQL;
        }

        /// <summary>
        /// 合并GridView中某列相同信息的行（单元格）
        /// </summary>
        /// <param name="GridView1">myGridView</param>
        /// <param name="cellNum">第几列</param>
        public static void GroupRows(GridView myGridView, int cellNum)
        {
            int i = 0, rowSpanNum = 1;
            while (i < myGridView.Rows.Count - 1)
            {
                GridViewRow gvr = myGridView.Rows[i];
                for (++i; i < myGridView.Rows.Count; i++)
                {
                    GridViewRow gvrNext = myGridView.Rows[i];
                    if (gvr.Cells[cellNum].Text == gvrNext.Cells[cellNum].Text)
                    {
                        gvrNext.Cells[cellNum].Visible = false;
                        rowSpanNum++;
                    }
                    else
                    {
                        gvr.Cells[cellNum].RowSpan = rowSpanNum;
                        rowSpanNum = 1;
                        break;
                    }
                    if (i == myGridView.Rows.Count - 1)
                    {
                        gvr.Cells[cellNum].RowSpan = rowSpanNum;
                    }
                    gvr.Cells[cellNum].BackColor = System.Drawing.Color.Azure;
                    //'#519EEC';
                }
            }
        }

        public static void GroupLinkRows(GridView myGridView, int cellNum)
        {
            int i = 0, rowSpanNum = 1;
            while (i < myGridView.Rows.Count - 1)
            {
                GridViewRow gvr = myGridView.Rows[i];
                for (++i; i < myGridView.Rows.Count; i++)
                {
                    GridViewRow gvrNext = myGridView.Rows[i];
                    if (((HyperLink)gvr.Cells[cellNum].Controls[0]).Text == ((HyperLink)gvrNext.Cells[cellNum].Controls[0]).Text)
                    {
                        gvrNext.Cells[cellNum].Visible = false;
                        rowSpanNum++;
                    }
                    else
                    {
                        gvr.Cells[cellNum].RowSpan = rowSpanNum;
                        rowSpanNum = 1;
                        break;
                    }
                    if (i == myGridView.Rows.Count - 1)
                    {
                        gvr.Cells[cellNum].RowSpan = rowSpanNum;
                    }
                    gvr.Cells[cellNum].BackColor = System.Drawing.Color.Azure;
                    //'#519EEC';
                }
            }
        }

        /*替换字符串中特殊的字符*/
        public static string FormatStr(string str)
        {
            string[] aryReg = { "'", "<", ">", "%", "\"\"", ",", ".", ">=", "=<", "-", "_", ";", "||", "[", "]", "&", "/", "-", "|", " ", "select", "update", "insert", "*", };
            for (int i = 0; i < aryReg.Length; i++)
            {
                str = str.Replace(aryReg[i], string.Empty);
            }
            return str;
        }
    }
}

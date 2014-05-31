using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QDHServer.Default
{
    using System.Collections;
    using System.Data;

    using CNVP.Framework.Helper;
    using CNVP.Framework.Utils;

    public partial class WebAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            Response.AddHeader("pragma", "no-cache");
            Response.AddHeader("cache-control", "");
            Response.CacheControl = "no-cache";
            Response.ContentType = "application/json";
            string Action = Request.Params["Action"];
            switch (Action)
            {
                case "NewsList":
                    NewsList();
                    break;
                case "CodeList":
                    CodeList();
                    break;
                case "GetDate":
                    GetDate();
                    break;
                case "GetMAC":
                    GetMAC();
                    break;
                case "GetFirstMAC":
                    GetFirstMAC();
                    break;
            }
        }

        public void NewsList()
        {
            string ColumnID = Public.FilterSql(Request.Params["ColumnID"]);
            if (string.IsNullOrEmpty(ColumnID) || (!Public.IsNumber(ColumnID)))
            {
                ColumnID = "4";
            }
            string PageNo = Request.Params["PageNo"];
            if (string.IsNullOrEmpty(PageNo) || (!Public.IsNumber(PageNo)))
            {
                PageNo = "1";
            }
            int _PageNo = Convert.ToInt32(PageNo);
            int PageSize = 7;
            int PageCount = 0;
            int PCount = 0;
            string rslt = "{";
            string list = "\"list\":[";
            string[] arrString = new string[0];
            using (DataTable dt = DbHelper.ExecuteTable("select * from T_NewsInfo where ColumnID in (" + GetChildColID(ColumnID) + ") and NewsState=1 order by OrderID Desc"))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    ArrayList List1 = new ArrayList(7);
                    PCount = dt.Rows.Count;
                    if (PCount % PageSize == 0)
                    {
                        PageCount = PCount / PageSize;
                    }
                    else
                    {
                        PageCount = PCount / PageSize + 1;
                    }
                    if (PCount < PageSize)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            List1.Add("{\"NewsID\":\"" + dt.Rows[i]["NewsID"] + "\",\"ColumnID\":\"" + dt.Rows[i]["ColumnID"] + "\",\"NewsTitle\":\"" + dt.Rows[i]["NewsTitle"] + "\",\"Coordinate\":\"" + dt.Rows[i]["Coordinate"] + "\",\"Recommend\":\"" + dt.Rows[i]["Recommend"] + "\",\"Pop\":\"" + dt.Rows[i]["Pop"] + "\",\"Wifi\":\"" + dt.Rows[i]["Wifi"] + "\",\"Park\":\"" + dt.Rows[i]["Park"] + "\",\"Box\":\"" + dt.Rows[i]["Box"] + "\",\"NewsLogourl\":\"" + dt.Rows[i]["NewsLogourl"] + "\",\"bz\":\"" + dt.Rows[i]["bz"] + "\",\"Createtime\":\"" + Convert.ToDateTime(dt.Rows[i]["Createtime"].ToString()).ToString("yyyy-MM-dd") + "\"}");
                        }
                    }
                    else
                    {
                        int last_page;
                        if (PCount < _PageNo * PageSize)
                        {
                            last_page = PCount;
                        }
                        else
                        {
                            last_page = _PageNo * PageSize;
                        }
                        for (int i = PageSize * (_PageNo - 1); i < last_page; i++)
                        {
                            List1.Add("{\"NewsID\":\"" + dt.Rows[i]["NewsID"] + "\",\"ColumnID\":\"" + dt.Rows[i]["ColumnID"] + "\",\"NewsTitle\":\"" + dt.Rows[i]["NewsTitle"] + "\",\"Coordinate\":\"" + dt.Rows[i]["Coordinate"] + "\",\"Recommend\":\"" + dt.Rows[i]["Recommend"] + "\",\"Pop\":\"" + dt.Rows[i]["Pop"] + "\",\"Wifi\":\"" + dt.Rows[i]["Wifi"] + "\",\"Park\":\"" + dt.Rows[i]["Park"] + "\",\"Box\":\"" + dt.Rows[i]["Box"] + "\",\"NewsLogourl\":\"" + dt.Rows[i]["NewsLogourl"] + "\",\"bz\":\"" + dt.Rows[i]["bz"] + "\",\"Createtime\":\"" + Convert.ToDateTime(dt.Rows[i]["Createtime"].ToString()).ToString("yyyy-MM-dd") + "\"}");
                        }
                    }
                    arrString = (string[])List1.ToArray(typeof(string));
                    list += string.Join(",", arrString) + "]";
                }
                else
                {
                    list += "{\"NewsID\":\"\",\"ColumnID\":\"\",\"NewsTitle\":\"\",\"Coordinate\":\"\",\"Pop\":\"\",\"Wifi\":\"\",\"Park\":\"\",\"Box\":\"\",\"NewsLogourl\":\"\",\"bz\":\"\",\"Createtime\":\"\"}]";
                }
            }
            rslt += "\"PageCount\":\"" + PageCount + "\",\"RecordCount\":\"" + PCount + "\",\"CurPage\":\"" + _PageNo + "\"," + list + "}";
            Response.Write(rslt);
            Response.End();
        }

        public void CodeList()
        {
            string ColumnID = Public.FilterSql(Request.Params["ColumnID"]);
            if (string.IsNullOrEmpty(ColumnID) || (!Public.IsNumber(ColumnID)))
            {
                ColumnID = "5";
            }
            string PageNo = Request.Params["PageNo"];
            if (string.IsNullOrEmpty(PageNo) || (!Public.IsNumber(PageNo)))
            {
                PageNo = "1";
            }
            int _PageNo = Convert.ToInt32(PageNo);
            int PageSize = 7;
            int PageCount = 0;
            int PCount = 0;
            string rslt = "{";
            string list = "\"list\":[";
            string[] arrString = new string[0];
            using (DataTable dt = DbHelper.ExecuteTable("select * from T_NewsInfo where ColumnID in (" + GetChildColID(ColumnID) + ") and IsShow=1 and ISAuditing=1 order by OrderID Desc"))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    ArrayList List1 = new ArrayList(7);
                    PCount = dt.Rows.Count;
                    if (PCount % PageSize == 0)
                    {
                        PageCount = PCount / PageSize;
                    }
                    else
                    {
                        PageCount = PCount / PageSize + 1;
                    }
                    if (PCount < PageSize)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            List1.Add("{\"NewsID\":\"" + dt.Rows[i]["NewsID"] + "\",\"ColumnID\":\"" + dt.Rows[i]["ColumnID"] + "\",\"NewsTitle\":\"" + dt.Rows[i]["NewsTitle"] + "\",\"NewsImages\":\"" + dt.Rows[i]["NewsImages"].ToString().Replace("{#InstallDir}", "/") + "\",\"NewsFocusImages\":\"" + dt.Rows[i]["NewsFocusImages"].ToString().Replace("{#InstallDir}", "/") + "\",\"NewsBrief\":\"" + dt.Rows[i]["NewsBrief"] + "\",\"PostTime\":\"" + Convert.ToDateTime(dt.Rows[i]["PostTime"].ToString()).ToString("yyyy-MM-dd") + "\"}");
                        }
                    }
                    else
                    {
                        int last_page;
                        if (PCount < _PageNo * PageSize)
                        {
                            last_page = PCount;
                        }
                        else
                        {
                            last_page = _PageNo * PageSize;
                        }
                        for (int i = PageSize * (_PageNo - 1); i < last_page; i++)
                        {
                            List1.Add("{\"NewsID\":\"" + dt.Rows[i]["NewsID"] + "\",\"ColumnID\":\"" + dt.Rows[i]["ColumnID"] + "\",\"NewsTitle\":\"" + dt.Rows[i]["NewsTitle"] + "\",\"NewsImages\":\"" + dt.Rows[i]["NewsImages"].ToString().Replace("{#InstallDir}", "/") + "\",\"NewsFocusImages\":\"" + dt.Rows[i]["NewsFocusImages"].ToString().Replace("{#InstallDir}", "/") + "\",\"NewsBrief\":\"" + dt.Rows[i]["NewsBrief"] + "\",\"PostTime\":\"" + Convert.ToDateTime(dt.Rows[i]["PostTime"].ToString()).ToString("yyyy-MM-dd") + "\"}");
                        }
                    }
                    arrString = (string[])List1.ToArray(typeof(string));
                    list += string.Join(",", arrString) + "]";
                }
                else
                {
                    list += "{\"NewsID\":\"\",\"ColumnID\":\"\",\"NewsTitle\":\"\",\"NewsImages\":\"\",\"NewsFocusImages\":\"\",\"NewsBrief\":\"\",\"PostTime\":\"\"}]";
                }
            }
            rslt += "\"PageCount\":\"" + PageCount + "\",\"RecordCount\":\"" + PCount + "\",\"CurPage\":\"" + _PageNo + "\"," + list + "}";
            Response.Write(rslt);
            Response.End();
        }

        private string GetChildColID(string ParentID)
        {
            string str = string.Empty;
            using (DataTable dt = DbHelper.ExecuteTable("select ColumnID, ParentID from T_Column Where ParentID='" + ParentID + "'"))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        str += "," + dt.Rows[i]["ColumnID"];
                    }
                }
                str = ParentID + str;
            }
            return str;
        }

        public void GetDate()
        {
            string dateday = string.Empty;
            string dateColumnID = string.Empty;
            string dateID = string.Empty;
            string dateTitle = string.Empty;
            string _CurDate = Request.Params["dateMon"];
            using (DataTable datedt = DbHelper.ExecuteTable("Select * From CNVP_NewsInfo Where ColumnID=2 and Convert(varchar,posttime,111) like '%" + _CurDate + "%' and ISShow=1 and ISAuditing=1"))
            {
                if (datedt.Rows.Count > 0)
                {
                    for (int i = 0; i < datedt.Rows.Count; i++)
                    {
                        if (i != datedt.Rows.Count - 1)
                        {
                            string ndd = Convert.ToDateTime(datedt.Rows[i]["PostTime"]).ToString("yyyy-MM-d").Substring(8);
                            dateday += ndd + '|';
                            dateColumnID += datedt.Rows[i]["ColumnID"].ToString() + '|';
                            dateID += datedt.Rows[i]["NewsID"].ToString() + '|';
                            dateTitle += datedt.Rows[i]["NewsTitle"].ToString() + '|';
                        }
                        else
                        {
                            string ndd = Convert.ToDateTime(datedt.Rows[i]["PostTime"]).ToString("yyyy-MM-d").Substring(8);
                            dateday += ndd;
                            dateColumnID += datedt.Rows[i]["ColumnID"].ToString();
                            dateID += datedt.Rows[i]["NewsID"].ToString();
                            dateTitle += datedt.Rows[i]["NewsTitle"].ToString();
                        }
                    }
                    Response.Write("{\"retuanval\":\"OK\",\"dateday\":\"" + dateday + "\",\"dateColumnID\":\"" + dateColumnID + "\",\"dateTitle\":\"" + dateTitle + "\",\"dateID\":\"" + dateID + "\"}");
                    Response.End();
                }
                else
                {
                    Response.Write("{\"retuanval\":\"OK\",\"dateday\":\"\",\"dateColumnID\":\"\",\"dateTitle\":\"\",\"dateID\":\"\"}");
                    Response.End();
                }
            }
        }

        #region 获取本机MAC地址，与后台的MAC栏目对应，找到需要的地理坐标
        private void GetMAC()
        {
            string MACAdd = Request.Params["Address"];
            MACAdd = MACAdd.ToLower();
            using (DataTable dt = DbHelper.ExecuteTable("select * from CNVP_NewsInfo where ColumnID=40 and ISShow=1 and ISAuditing=1 and LOWER(SubNewsTitle)='" + MACAdd + "'"))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    Response.Write("{\"retuanval\":\"OK\",\"coordinate\":\"" + dt.Rows[0]["KeyWord"] + "\"}");
                    Response.End();
                }
                else
                {
                    Response.Write("{\"retuanval\":\"OK\",\"coordinate\":\"\"}");
                    Response.End();
                }
            }
        }
        #endregion

        #region 获取第一次登陆时的MAC地址
        private void GetFirstMAC()
        {
            string MACAdd = Request.Params["Address"];
            MACAdd = MACAdd.ToLower();
            if (!string.IsNullOrEmpty(MACAdd))
            {
                using (DataTable dt = DbHelper.ExecuteTable("select * from cnvp_newsinfo where ColumnID=40 and LOWER(SubNewsTitle)='" + MACAdd + "'"))
                {
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        //DbHelper.ExecuteNonQuery("insert into cnvp_newsinfo ('40',)")
                    }
                }
            }
        }
        #endregion
    }
}
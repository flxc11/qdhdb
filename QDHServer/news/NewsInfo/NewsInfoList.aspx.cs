using System;
using CNVP.Framework.Helper;
using CNVP.Framework.Utils;

namespace QDHServer.news.NewsInfo
{
    using System.Data;
    using System.Web.UI.WebControls;

    public partial class NewsInfoList : System.Web.UI.Page
    {
        public string PageNo = string.Empty;

        public string PcolumnId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 栏目列表3级嵌套循环
                TxtColumnID.Items.Clear();
                using (DataTable dataTable = DbHelper.ExecuteTable("select * from T_Column where ParentID=0"))
                {
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            TxtColumnID.Items.Add(new ListItem(dataTable.Rows[i]["ColumnName"].ToString(), dataTable.Rows[i]["ColumnID"].ToString()));
                            using (DataTable dataTable1 = DbHelper.ExecuteTable("select * from T_Column where ParentID=" + dataTable.Rows[i]["ColumnID"].ToString()))
                            {
                                if (dataTable1 != null && dataTable1.Rows.Count > 0)
                                {
                                    for (int j = 0; j < dataTable1.Rows.Count; j++)
                                    {
                                        TxtColumnID.Items.Add(new ListItem("|－" + dataTable1.Rows[j]["ColumnName"].ToString(), dataTable1.Rows[j]["ColumnID"].ToString()));
                                        using (DataTable dataTable2 = DbHelper.ExecuteTable("select * from T_Column where ParentID=" + dataTable1.Rows[j]["ColumnID"].ToString()))
                                        {
                                            if (dataTable2 != null && dataTable2.Rows.Count > 0)
                                            {
                                                for (int k = 0; k < dataTable2.Rows.Count; k++)
                                                {
                                                    TxtColumnID.Items.Add(new ListItem("|－－" + dataTable2.Rows[k]["ColumnName"].ToString(), dataTable2.Rows[k]["ColumnID"].ToString()));

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                TxtColumnID.Items.Insert(0, new ListItem("请选择栏目", "0"));
                #endregion
                LitUsersDel.Text = "<a href=\"javascript:void(0);\" onclick=\"DeleteSeleted(confirmMsg,ckCtrl,'Ajax.aspx','DeleteSeleted')\" class=\"tools_btn\"><span><b class=\"delete\">删除</b></span></a>";
                string action = Request.QueryString["action"];
                string strWhere = "T_NewsInfo Where 1=1";
                
                if (action == "Search")
                {
                    string columnId = Request.QueryString["ColumnID"];
                    this.TxtColumnID.SelectedValue = columnId;
                    PcolumnId = columnId;
                    string keyWord = Request.QueryString["keyword"];
                    this.keyword.Text = keyWord;
                    if (columnId != "0")
                    {
                        strWhere += " and ColumnID='" + columnId + "'";
                    }
                    strWhere += " and NewsTitle like '%" + keyWord + "%'";
                    
                }
                PageNo = Request.Params["PageNo"];
                if (string.IsNullOrEmpty(PageNo) || (!Public.IsNumber(PageNo)))
                {
                    PageNo = "1";
                }
                int PageSize = 10;
                int RecordCount, PageCount;
                Repeater1.DataSource = DbHelper.ExecutePage("*", strWhere, "NewsID", "Order By OrderID Desc", Convert.ToInt32(PageNo), PageSize, out RecordCount, out PageCount);
                Repeater1.DataBind();
                LitPager.Text = DbHelper.GetPageNormal(RecordCount, PageCount, PageSize, Convert.ToInt32(PageNo));
            }
        }

        #region 审核状态
        public string IsAudit(string Id, int newsState)
        {
            string str = "未审核";
            if (newsState == 1)
            {
                str = "已审核";
                return string.Format("<a href=\"#\" title=\"取消审核\" onclick=\"ISAudit({1},{2})\">{0}</a>", str, Id, newsState);
            }
            else
            {
                return string.Format("<a href=\"#\" title=\"审核\" onclick=\"ISAudit({1},{2})\">{0}</a>", str, Id, newsState);
            }
        }
        #endregion
    }
}
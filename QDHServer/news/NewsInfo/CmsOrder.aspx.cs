using System;
using System.Data;
using System.Web.UI.WebControls;
using CNVP.Data;
using CNVP.Framework.Utils;

namespace CNVP.CMS.Admin.JCms
{
    public partial class CmsOrder : System.Web.UI.Page
    {
        public string _FirstRow = "1", _EndRow = "10", _MaxOrderID = "1", _ParentID="";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                _ParentID = Request.Params["ParentID"];
                if ((string.IsNullOrEmpty(_ParentID)) || (!Public.IsNumber(_ParentID)))
                {
                    _ParentID = "0";
                }
                _FirstRow = Request.Params["FirstRow"];
                if (string.IsNullOrEmpty(_FirstRow) || (!Public.IsNumber(_FirstRow)))
                {
                    _FirstRow = "1";
                }
                _EndRow = Request.Params["EndRow"];
                if (string.IsNullOrEmpty(_EndRow) || (!Public.IsNumber(_EndRow)))
                {
                    _EndRow = "10";
                }
                if (Convert.ToInt32(_FirstRow) > Convert.ToInt32(_EndRow))
                {
                    MessageUtils.ShowPre("请输入正确的数字范围！");
                }
                Column bll_column=new Column();
                string _ColumnID = _ParentID + bll_column.GetChildColumnID(Convert.ToInt32(_ParentID));
                NewsInfoData bll = new NewsInfoData();
                DataTable Dt = bll.GetNewsList(_ColumnID, Convert.ToInt32(_FirstRow), Convert.ToInt32(_EndRow));
                if (Dt != null && Dt.Rows.Count > 0)
                {
                    LstNewsList.Items.Add(new ListItem("-----------你可以选择多项进行排序操作-----------", "0"));
                    _MaxOrderID = Dt.Rows[0]["OrderID"].ToString();
                    foreach (DataRow row in Dt.Rows)
                    {
                        ListItem _List = new ListItem();
                        _List.Text = row["NewsTitle"].ToString();
                        _List.Value = row["NewsID"].ToString();
                        LstNewsList.Items.Add(_List);
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string _SelectValue = Request.Params["HidSelectValue"];
            string _OrderID = Request.Params["HidOrderID"];
            string[] _SAry = _SelectValue.Split(',');
            for (int i = 1; i < _SAry.Length; i++)
            {
                NewsInfoData bll = new NewsInfoData();
                bll.NewsInfoEditOrder(Convert.ToInt32(_SAry[i]), Convert.ToInt32(_OrderID) - i + 1);
            }
            MessageUtils.Write("<script language=\"javascript\" type=\"text/javascript\">top.location.reload();top.removeTB();</script>");
        }
    }
}
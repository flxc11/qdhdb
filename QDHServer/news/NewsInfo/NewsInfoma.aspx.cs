using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;
using CNVP.Data;
using CNVP.Framework.Helper;
using CNVP.Framework.Utils;
using CNVP.Model;
using CNVP.UI;

namespace QDHServer.news.NewsInfo
{
    public partial class NewsInfoma : System.Web.UI.Page
    {
        public string _PageNo = string.Empty;
        public string _ID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                _PageNo = Request.Params["PageNo"];
                _ID = Request.Params["ID"];
                if (string.IsNullOrEmpty(_ID) || !Public.IsNumber(_ID))
                {
                    _ID = "1";
                }
                NewsInfoData bll = new NewsInfoData();
                NewsInfoModel model = bll.GetNewsInfo(Convert.ToInt32(_ID));
                if (model != null)
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
                    TxtColumnID.SelectedValue = model.ColumnID.ToString();
                    #endregion
                    //UpdateModel<NewsInfoModel>.UpdateModels(model, "Txt");
                    UpdateModel<NewsInfoModel>.SetWebControls(this.Page, model);
                    TxtCreatetime.Text = Convert.ToDateTime(model.Createtime.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    TxtNewsLogourl1.Text = model.NewsLogourl;

                    //TxtPark.v = model.Park == 1 ? true : false;
                    TxtPark.SelectedValue = model.Park.ToString();
                    TxtBox.SelectedValue = model.Box.ToString();
                    TxtWifi.SelectedValue = model.Wifi.ToString();
                    
                    TxtNewsContent.Value = model.NewsContent;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string ID = Request.Form["ID"];
            string PageNo = Request.Form["PageNo"];
            NewsInfoData bll = new NewsInfoData();
            NewsInfoModel model = new NewsInfoModel();
            UpdateModel<NewsInfoModel>.UpdateModels(model, "Txt");

            model.NewsID = Convert.ToInt32(ID);
            string picUrl = FileUpload();
            if (picUrl.Length > 0)
            {
                model.NewsLogourl = picUrl;
            }
            else
            {
                model.NewsLogourl = TxtNewsLogourl1.Text.Trim();
            }
            model.NewsContent = TxtNewsContent.Value;
            bll.EditNewsInfo(model);
            Response.Write("<script>alert('修改成功');window.location.href='NewsInfoList.aspx?PageNo=" + PageNo + "';</script>");
            Response.End();
            //BasePage basePage = new BasePage();
            //basePage.Message("恭喜，信息修改成功！", "NewsInfoList.aspx?PageNo=" + PageNo, "Success");
        }

        public static string FileUpload()
        {
            string TempPath = "/UploadFile/News/" + DateTime.Now.ToString("yyyyMMdd");
            string str = string.Empty;
            if (HttpContext.Current.Request.Files.Count != 0)
            {
                for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                {
                    var filepath = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    HttpPostedFile file = HttpContext.Current.Request.Files[i];
                    string fileExtension = Path.GetExtension(file.FileName).ToLower();

                    //if (!TalentFile.CheckFileExt("GIF|JPG|PNG|BMP", fileExtension.Replace(".", "")) && !string.IsNullOrEmpty(file.FileName))
                    //{
                    //    HttpContext.Current.Response.Write("不允许上传" + fileExtension.Replace(".", "") + "类型的文件！");
                    //    HttpContext.Current.Response.End();
                    //}
                    if (file.ContentLength > 0 && (!string.IsNullOrEmpty(file.FileName)))
                    {
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath(TempPath)))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(TempPath));
                        }
                        file.SaveAs(System.Web.HttpContext.Current.Server.MapPath(TempPath + "/" + filepath + fileExtension));
                        str = TempPath + "/" + filepath + fileExtension;
                    }
                    
                    Thread.Sleep(100);
                }
            }
            return str;
        }
    }
}
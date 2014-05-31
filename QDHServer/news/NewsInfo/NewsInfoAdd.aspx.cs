using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;
using CNVP.Data;
using CNVP.Framework.Helper;
using CNVP.Model;
using CNVP.UI;

namespace QDHServer.news.NewsInfo
{
    public partial class NewsInfoAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TxtCreatetime.Text = DateTime.Now.ToString();

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
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            NewsInfoData bll = new NewsInfoData();
            NewsInfoModel model = new NewsInfoModel();
            UpdateModel<NewsInfoModel>.UpdateModels(model, "Txt");
            ////model.AuthorID = GetLoginInfo().AdminID;
            //model.AuthorID = 0;
            //model.NewsContent = TxtNewsContent.Value;
            if (TxtCreatetime.Text == "")
            {
                model.Createtime = DateTime.Now;
            }
            else
            {
                model.Createtime = Convert.ToDateTime((string)TxtCreatetime.Text);
            }
            
            //model.Clicks = 1;
            model.NewsLogourl = FileUpload();
            model.NewsContent = TxtNewsContent.Value;
            bll.AddNewsInfo(model);
            Response.Write("<script>alert('添加成功');window.location.href='NewsInfoList.aspx';</script>");
            Response.End();
            //BasePage basePage = new BasePage();
            //basePage.Message("恭喜，信息添加成功！", "NewsInfoList.aspx", "Success");
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
                    if (file.ContentLength > 0 || (!string.IsNullOrEmpty(file.FileName)))
                    {
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath(TempPath)))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(TempPath));
                        }
                        file.SaveAs(System.Web.HttpContext.Current.Server.MapPath(TempPath + "/" + filepath + fileExtension));
                    }
                    str = TempPath + "/" + filepath + fileExtension;
                    Thread.Sleep(100);
                }
            }
            return str;
        }
    }
}
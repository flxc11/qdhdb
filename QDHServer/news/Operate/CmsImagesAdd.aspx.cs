using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Threading;
using CNVP.Data;
using CNVP.Model;

namespace QDHServer.news.Operate
{
    public partial class CmsImagesAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                string Action = Request.Params["Action"];
                
                switch (Action)
                {
                    case "Save":
                        if (Request.Files.Count != 0)
                        {
                            for (int i = 0; i < Request.Files.Count; i++)
                            {
                                HttpPostedFile file = Request.Files[i];
                                string fileExtension = Path.GetExtension(file.FileName).ToLower();
                                if (!string.IsNullOrEmpty(fileExtension))
                                {
                                    #region "判断扩展名"
                                    if (!CheckExtension(fileExtension))
                                    {
                                        Response.Write("<script>window.parent.Finish('系统不支持上传" + fileExtension + "后缀的文件！');</script>");
                                        Response.End();
                                    }
                                    #endregion
                                    #region "保存文件"
                                    if (file.ContentLength > 0 || (!string.IsNullOrEmpty(file.FileName)))
                                    {
                                        string _Folder = DateTime.Now.ToString("yyyyMM");
                                        if (!Directory.Exists(_Folder))
                                        {
                                            Directory.CreateDirectory(Server.MapPath("/UploadFile/" + _Folder));
                                        }
                                        string _FileName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + fileExtension.ToLower();
                                        file.SaveAs(System.Web.HttpContext.Current.Server.MapPath("/UploadFile/" + _Folder + "/" + _FileName));
                                        //写入数据库
                                        NewsSourceData bll = new NewsSourceData();
                                        NewsSourcesModel Model = new NewsSourcesModel();
                                        Model.FileName = _FileName;
                                        //Edit By Apollo/2010-06-30
                                        //bll.FilePath = UIConfig.InstallDir + "UploadFile/" + _Folder + "/" + _FileName;
                                        Model.FilePath = "/UploadFile/" + _Folder + "/" + _FileName;
                                        Model.FileExtension = fileExtension.Replace(".", "");
                                        //JCms1.4.0
                                        //bll.AdminID = CNVP.Framework.UI.AdminPage.AdminID;
                                        //Model.AdminID = AdminID;
                                        Model.AdminID = 0;
                                        Model.PostTime = DateTime.Now.ToString();
                                        bll.AddSource(Model);
                                    }
                                    #endregion
                                }
                                Thread.Sleep(50);
                            }
                            Response.Write("<script type='text/javascript'>window.parent.Finish('恭喜，文件上传操作成功！');</script>");
                        }
                        else
                        {
                            Response.Write("<script>window.parent.Finish('请指定至少一个文件进行上传操作！');</script>");
                        }
                        break;
                }
            }
        }

        #region "检查文件后缀"
        /// <summary>
        /// 检查文件后缀
        /// </summary>
        /// <param name="FileExtension"></param>
        /// <returns></returns>
        private bool CheckExtension(string FileExtension)
        {
            bool flg = false;
            string[] aryReg = { ".jpg", ".gif", ".png", ".bmp" };
            if (!string.IsNullOrEmpty(FileExtension))
            {
                foreach (string str in aryReg)
                {
                    if (FileExtension == str)
                    {
                        flg = true;
                        break;
                    }
                }
            }
            return flg;
        }
        #endregion
    }
}
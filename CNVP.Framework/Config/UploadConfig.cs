using System;
using System.Data;
using System.Web;
using System.Xml;
using CNVP.Framework.Helper;

namespace CNVP.Config
{
    public class UploadConfig
    {
        public class GetSetting
        {
            public string ItemName { get; set; }
            public string FilePath { get; set; }
            public string AllowExt { get; set; }
            public string MaxSize { get; set; }
        }
        #region "获取上传信息"
        public static GetSetting GetSingleConfig(string ItemName)
        {         
            DataRow[] _DataRow=GetAllowUpload().Select(string.Format("ItemName='{0}'",ItemName));
            GetSetting _GetSetting = new GetSetting();
            if (_DataRow.Length > 0)
            {
                _GetSetting.ItemName = _DataRow[0]["ItemName"].ToString();
                _GetSetting.FilePath = _DataRow[0]["FilePath"].ToString();
                _GetSetting.AllowExt = _DataRow[0]["AllowExt"].ToString();
                _GetSetting.MaxSize = _DataRow[0]["MaxSize"].ToString();
            }
            else 
            {
                _GetSetting.ItemName = "";
                _GetSetting.FilePath = "";
                _GetSetting.AllowExt = "jpg";
                _GetSetting.MaxSize =  "0";
            }
            return _GetSetting;
        }
        /// <summary>
        /// 获取上传信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllowUpload()
        {
            try
            {
                XmlDocument Xml = new XmlDocument();
                Xml.Load(HttpContext.Current.Server.MapPath("~/Config/AllowUpload.config"));
                XmlNodeList RootList = Xml.SelectNodes("Module/Item");

                DataTable _Dt = new DataTable("Result");
                _Dt.Columns.Add("ItemName", typeof(string));
                _Dt.Columns.Add("FilePath", typeof(string));
                _Dt.Columns.Add("AllowExt", typeof(string));
                _Dt.Columns.Add("MaxSize", typeof(long));

                foreach (XmlNode Xn in RootList)
                {
                    DataRow _Rows = _Dt.NewRow();
                    _Rows["ItemName"] = Xn.Attributes["ItemName"].InnerText;
                    _Rows["FilePath"] = Xn.Attributes["FilePath"].InnerText;
                    _Rows["AllowExt"] = Xn.Attributes["AllowExt"].InnerText;
                    _Rows["MaxSize"] = Xn.Attributes["MaxSize"].InnerText;
                    _Dt.Rows.Add(_Rows);
                }
                return _Dt;
            }
            catch (Exception ex)
            {
                LogHelper.Write("上传类型获取失败", ex.ToString());
                return null;
            }
        }
        #endregion
    }
}
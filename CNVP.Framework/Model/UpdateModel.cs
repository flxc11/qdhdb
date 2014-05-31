using System;
using System.Collections;
using System.Web;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CNVP.Framework.Utils;

namespace CNVP.Framework.Helper
{
    /// <summary>
    /// 接收实体类并接收表单数据进行赋值操作
    /// </summary>
    /// <typeparam name="T">实体类</typeparam>
    public sealed class UpdateModel<T> where T : class 
    {
        #region 根据实体类赋值
        /// <summary>
        /// 根据实体类赋值
        /// </summary>
        /// <param name="objectModel"></param>
        public static void UpdateModels(T objectModel)
        {
            UpdateModels(objectModel, string.Empty);
        }
        #endregion
        #region 根据实体类赋值
        /// <summary>
        /// 根据实体类赋值
        /// </summary>
        /// <param name="objectModel">实体对象</param>
        /// <param name="preName">前缀名进行过滤</param>
        public static void UpdateModels(T objectModel,string preName)
        {
            if (objectModel == null)
                return;
            NameValueCollection _nvc = getFormCollection;
            if (_nvc.Count > 0)
            {
                Type type = objectModel.GetType();
                foreach (PropertyInfo p in type.GetProperties())
                {
                    if (!p.CanWrite) continue;
                    //反射实体属性名称
                    string modelName = p.Name;
                    object val = _nvc[modelName];
                    if (!string.IsNullOrEmpty(preName))
                    {
                        if (!modelName.StartsWith(preName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            modelName = string.Format("{0}{1}", preName, modelName);
                            val = _nvc[modelName];
                            //移除前缀
                            modelName = modelName.Remove(0, preName.Length);
                        }
                    }
                    if (val == null) continue;//如果为null跳出本次循环
                    p.SetValue(objectModel, Public.GetDefaultValue(val, p.PropertyType), null);
                }
            }
        }
        #endregion
        #region 获取实体表单值
        private static NameValueCollection getFormCollection
        {
            get
            {
                NameValueCollection nvc = HttpContext.Current.Request.Form;
                return nvc;
            }
        }
        #endregion
        #region "控件赋值"

        /// <summary>
        /// 设置页面控件的值
        /// </summary>
        /// <param name="page"></param>
        /// <param name="model"></param>
        public static void SetWebControls(Control page,T model)
        {
            SetWebControls(page, HashTableHelper.GetModelToHashtable(model));
        }
        /// <summary>
        /// 设置页面控件的值
        /// </summary>
        /// <param name="page"></param>
        /// <param name="ht"></param>
        public static void SetWebControls(Control page, Hashtable ht)
        {
            if (ht.Count != 0)
            {
                int size = ht.Keys.Count;
                foreach (string key in ht.Keys)
                {
                    object val = ht[key];
                    if (val != null)
                    {
                        Control control = page.FindControl("Txt" + key);
                        if (control == null) continue;
                        if (control is HtmlInputText)
                        {
                            HtmlInputText txt = (HtmlInputText)control;
                            txt.Value = val.ToString().Trim();
                        }
                        if (control is TextBox)
                        {
                            TextBox txt = (TextBox)control;
                            txt.Text = val.ToString().Trim();
                        }
                        if (control is HtmlSelect)
                        {
                            HtmlSelect txt = (HtmlSelect)control;
                            txt.Value = val.ToString().Trim();
                        }
                        if (control is HtmlInputHidden)
                        {
                            HtmlInputHidden txt = (HtmlInputHidden)control;
                            txt.Value = val.ToString().Trim();
                        }
                        if (control is HtmlInputPassword)
                        {
                            HtmlInputPassword txt = (HtmlInputPassword)control;
                            txt.Value = val.ToString().Trim();
                        }
                        if (control is Label)
                        {
                            Label txt = (Label)control;
                            txt.Text = val.ToString().Trim();
                        }
                        if (control is HtmlInputCheckBox)
                        {
                            HtmlInputCheckBox chk = (HtmlInputCheckBox)control;
                            //chk.Checked = CommonHelper.GetInt(val) == 1 ? true : false;
                        }
                        if (control is HtmlTextArea)
                        {
                            HtmlTextArea area = (HtmlTextArea)control;
                            area.Value = val.ToString().Trim();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 获取服务器控件值
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static Hashtable GetWebControls(Control page)
        {
            Hashtable ht = new Hashtable();
            int size = HttpContext.Current.Request.Params.Count;
            for (int i = 0; i < size; i++)
            {
                string id = HttpContext.Current.Request.Params.GetKey(i);
                Control control = page.FindControl(id);
                if (control == null) continue;
                control = page.FindControl(id);
                if (control is HtmlInputText)
                {
                    HtmlInputText txt = (HtmlInputText)control;
                    ht[txt.ID] = txt.Value.Trim();
                }
                if (control is HtmlSelect)
                {
                    HtmlSelect txt = (HtmlSelect)control;
                    ht[txt.ID] = txt.Value.Trim();
                }
                if (control is HtmlInputHidden)
                {
                    HtmlInputHidden txt = (HtmlInputHidden)control;
                    ht[txt.ID] = txt.Value.Trim();
                }
                if (control is HtmlInputPassword)
                {
                    HtmlInputPassword txt = (HtmlInputPassword)control;
                    ht[txt.ID] = txt.Value.Trim();
                }
                if (control is HtmlInputCheckBox)
                {
                    HtmlInputCheckBox chk = (HtmlInputCheckBox)control;
                    ht[chk.ID] = chk.Checked ? 1 : 0;
                }
                if (control is HtmlTextArea)
                {
                    HtmlTextArea area = (HtmlTextArea)control;
                    ht[area.ID] = area.Value.Trim();
                }
            }
            return ht;
        }
        #endregion
    }
}

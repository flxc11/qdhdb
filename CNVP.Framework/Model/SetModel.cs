using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CNVP.Framework.Helper;

namespace CNVP.Framework.Model
{
    public class SetModel
    {
        #region "控件赋值"

        /// <summary>
        /// 设置页面控件的值
        /// </summary>
        /// <param name="page"></param>
        public void SetWebControls(Control page)
        {
            SetWebControls(page, HashTableHelper.GetModelToHashtable(this));
        }
        /// <summary>
        /// 设置页面控件的值
        /// </summary>
        /// <param name="page"></param>
        /// <param name="ht"></param>
        public void SetWebControls(Control page, Hashtable ht)
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
        public Hashtable GetWebControls(Control page)
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
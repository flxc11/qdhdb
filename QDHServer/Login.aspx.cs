using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QDHServer.AppCode;
using System.Data;

namespace QDHServer
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
       
        //登陆
        protected void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                string code = Session["CheckCode"].ToString();
                Common sh = new Common();
                if (txtUserName.Text == "")
                {
                    pnl_prompt.Visible = true;
                    lbl_prompt.Text = "登陆名称不能为空";
                    txtUserName.Focus();
                    return;
                }
                if (txtPwd.Text == "")
                {
                    pnl_prompt.Visible = true;
                    lbl_prompt.Text = "密码不能为空";
                    txtPwd.Focus();
                    return;
                }
                if (txtCode.Text == "")
                {
                    pnl_prompt.Visible = true;
                    lbl_prompt.Text = "验证码不能为空";
                    txtCode.Focus();
                    return;
                }
                if (txtCode.Text != code)
                {
                    pnl_prompt.Visible = true;
                    lbl_prompt.Text = "请正确输入验证码";
                    txtCode.Focus();
                    return;
                }
                else
                {
                    string strSQL = "select * from ADUSERS where USERNAME='" + txtUserName.Text + "' and PASSWORD='" + txtPwd.Text + "'";
                    DataSet ds = new DataSet();
                    ds = sh.GetGeneralTable("", "", strSQL, 2);
                    //  string Area_name = HttpUtility.UrlEncode(ds.Tables[0].Rows[0][0].ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Session["name"] = txtUserName.Text;
                        Session["pwd"] = txtPwd.Text;
                        //  Response.Redirect("index.aspx?" + "&Area_name=" + Area_name, false);
                        Response.Redirect("Left.htm");
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alert()</script>");
                    }
                }
            }
            catch (Exception)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script>alertError()</script>");
            }
        }

        //清空
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtCode.Text = "";
            txtPwd.Text = "";
            txtUserName.Text = "";          
        }
    }
}

using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace QDHServer.ad.SetTime
{
    public partial class SetTime : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //设置开机时间
        protected void lnkPowerOnTime_Click(object sender, EventArgs e)
        {
            Response.Redirect("PowerOn.aspx");
        }

        //设置关机时间
        protected void lnkCloseTime_Click(object sender, EventArgs e)
        {
            Response.Redirect("SetCloseTime.aspx");
        }

        //重启时间
        protected void lnkRestartTime_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReStartTime.aspx");
        }

        ////设置开机关机列表
        //protected void lnkOpenCloseList_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("SetStartCloseTime.aspx");
        //}
    }
}
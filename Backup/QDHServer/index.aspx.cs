﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QDHServer
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //转向
        protected void IbtnAD_Click(object sender, EventArgs e)
        {
            Response.Redirect("ad/ADManagements/ScreenAD.aspx");
        }
    }
}
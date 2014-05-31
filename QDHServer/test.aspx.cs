using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QDHServer.AppCode;

namespace QDHServer
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ytSelfHelpSvr yt = new ytSelfHelpSvr();
            // yt.getDelInsert("YTJF0085");
           // yt.updateStatus("YTJF0087");
          yt.picInfo("YTJF0087");
        }
    }
}
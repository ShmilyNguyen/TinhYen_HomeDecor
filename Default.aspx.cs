using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WKS.DMS.WEB
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Ham kiem tra license Su dung
            if (clsCommon.ConvertDateToNumber(DateTime.Now) >= 20160110)
            {
                //Response.Redirect(clsCommon.UrlRoot + "Login.aspx");
            }
        }
    }
}
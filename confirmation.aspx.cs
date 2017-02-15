using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WKS.DMS.WEB
{
    public partial class confirmation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string p = Request.QueryString["p"].ToString();
                if (p=="changedpass")
                {
                    lblMessage.Text = "Cập nhật thông tin tài khoản thành công ! Vui lòng thoát ra và đăng nhập lại vào hệ thống !";
                }
            }
        }

        protected void btnGoBack_Click(object sender, EventArgs e)
        {
            string p = Request.QueryString["p"].ToString();
            if (p == "changedpass")
            {
                Response.Redirect(clsCommon.UrlRoot + "/login.aspx");
            }
        }
    }
}
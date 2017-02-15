using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;

namespace WKS.DMS.WEB
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~/bin"));
            log4net.Config.XmlConfigurator.Configure();
            // Code that runs on application startup
            WebControl.DisabledCssClass = "";
            


        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["CultureInfo"];
            if (cookie != null && cookie.Value != null)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cookie.Value, false);
                Thread.CurrentThread.CurrentCulture = new CultureInfo(cookie.Value, false);

            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            }
            string language = Thread.CurrentThread.CurrentCulture.Name;
            string Uilanguage = Thread.CurrentThread.CurrentUICulture.Name;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

       

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
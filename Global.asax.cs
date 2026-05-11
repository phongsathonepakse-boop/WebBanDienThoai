using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Security.Principal;
using WebBanDienThoai.Models;

namespace WebBanDienThoai
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // This tells EF to stop checking for model changes and just use the database as-is.
            Database.SetInitializer<WebBanDienThoai.Models.AppDbContext>(null);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest()
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null || string.IsNullOrWhiteSpace(authCookie.Value))
            {
                return;
            }

            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
            if (ticket == null || ticket.Expired)
            {
                return;
            }

            var roles = string.IsNullOrWhiteSpace(ticket.UserData) ? new string[0] : ticket.UserData.Split(',');
            Context.User = new GenericPrincipal(new FormsIdentity(ticket), roles);
        }
    }
}

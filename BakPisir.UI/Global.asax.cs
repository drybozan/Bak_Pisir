using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BakPisir.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        protected void Application_Error()
        {

            var exception = Server.GetLastError();
            var action = "Index";
            Response.Clear();

            if (exception is HttpException httpException)
            {

                switch (httpException.GetHttpCode())
                {
                    case 401:
                        // unauthorized
                        action = "Page401";
                        break;
                    case 400:
                        action = "Page400";
                        break;
                    case 404:
                        // page not found
                        action = "Page404";
                        break;
                    case 500:
                        // server error
                        action = "Page500";
                        break;
                    // forbidden error
                    case 403:
                        action = "Page403";
                        break;
                    default:
                        action = "ErrorPage";
                        break;
                }
            }
            Response.Redirect($"~/ErrorPage/{action}");
        }
    }
}

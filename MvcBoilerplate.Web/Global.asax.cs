using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcBoilerplate.Web.Controllers;

namespace MvcBoilerplate.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("apple-touch-icon-114x114-precomposed.png");
            routes.Ignore("apple-touch-icon-72x72-precomposed.png");
            routes.Ignore("apple-touch-icon-57x57-precomposed.png");
            routes.Ignore("apple-touch-icon-precomposed.png");
            routes.Ignore("apple-touch-icon.png");
            routes.Ignore("robots.txt");
            routes.Ignore("favicon.ico");
            routes.Ignore("crossdomain.xml");
            routes.Ignore("humans.txt");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            var httpException = exception as HttpException;

            Response.Clear();
            Server.ClearError();

            var routeData = new RouteData();
            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = "HandleError";
            routeData.Values["exception"] = exception;

            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = 500;

            IController errorsController = new ErrorController();
            var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
            errorsController.Execute(rc);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

    }
}
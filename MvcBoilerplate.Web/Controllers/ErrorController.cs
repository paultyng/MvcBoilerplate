using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBoilerplate.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult HandleError(Exception exception)
        {
            var http = exception as HttpException;

            if (http != null)
            {
                Response.StatusCode = http.GetHttpCode();
            }

            switch (Response.StatusCode)
            {
                case 404:
                    return View("Http404");
                default:
                    return View("General", exception);
            }
        }
    }
}

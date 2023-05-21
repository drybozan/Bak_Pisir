using BakPisir.CORE.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakPisir.UI.Controllers
{
    [LogAction]
    public class ErrorPageController : Controller
    {
        // GET: ErrorPage
        public ActionResult ErrorPage()
        {
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
        public ActionResult Page404()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            ViewBag.text = "Oops!Page Not Found.";
            return View("ErrorPage");
        }
        public ActionResult Page400()
        {
            Response.StatusCode = 400;
            Response.TrySkipIisCustomErrors = true;
            ViewBag.text = "Your request result in error.";
            return View("ErrorPage");
        }
        public ActionResult Page401()
        {
            Response.StatusCode = 401;
            Response.TrySkipIisCustomErrors = true;
            ViewBag.text = "Authorization required.";
            return View("ErrorPage");
        }
        public ActionResult Page403()
        {
            Response.StatusCode = 403;
            Response.TrySkipIisCustomErrors = true;
            ViewBag.text = "You don't have permission to access this resource.";
            return View("ErrorPage");
        }
        public ActionResult Page500()
        {
            Response.StatusCode = 500;
            ViewBag.text = "Oops!Looks like something went wrong.";
            Response.TrySkipIisCustomErrors = true;
            return View("ErrorPage");
        }
    }
}
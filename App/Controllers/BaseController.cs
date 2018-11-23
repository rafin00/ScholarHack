using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace App.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string st = (string)RouteData.Values["controller"];
            string cn = (string)Session["UserTypeName"];
            if (Session["email"] == null || (string)Session["UserTypeName"] != (string)RouteData.Values["controller"])
            {
                //Response.Redirect("/Login");
                filterContext.Result = new RedirectToRouteResult
                (
                    new RouteValueDictionary
                    {
                        {"controller","Login"},{"action","Login"}
                    }
                );
            }
        }
    }
}
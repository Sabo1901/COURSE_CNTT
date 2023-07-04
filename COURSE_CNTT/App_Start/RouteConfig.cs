using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace COURSE_CNTT
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "COURSE_CNTT.Controllers" }
            );
            routes.MapRoute(
               name: "Search",
               url: "tim-kiem",
               defaults: new { controller = "COURSE", action = "Search", id = UrlParameter.Optional },
               namespaces: new[] { "COURSE_CNTT.Controllers" }
           );
        }
    }
}

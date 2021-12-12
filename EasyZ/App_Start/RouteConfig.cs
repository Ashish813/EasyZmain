using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EasyZ
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
           "Create-Resume-name",
           "Create-Resume",
           defaults: new { controller = "ResumeProcess", action = "ResumeProcess" }
         );

            routes.MapRoute(
       "SecondPageReturnPartial",
       "Select-Resume",
       defaults: new { controller = "ResumeProcess", action = "SecondPage" }
     );




            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

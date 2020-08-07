using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PortalDeNoticias
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Para que o Visual Studio abra por padrão uma determinada action de um controller, definir os valores padrão dos argumentos controller e action de acordo com a necessidade

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Noticias", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
using FM.Portal.FrameWork.Localization;
using FM.Portal.FrameWork.MVC.Routes;
using FM.Portal.FrameWork.Seo;
using System.Web.Routing;

namespace Ahora.WebApp.Infrastructure
{
    public class GenericUrlRouteProvider : IRouteProvider
    {
        public int Priority
        {
            get
            {
                return -1000000;
            }
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapGenericPathRoute("GenericUrl",
                                       "{generic_se_name}",
                                       new { controller = "Common", action = "GenericUrl" },
                                       new[] { "Ahora.WebApp.Controllers" });

            //define this routes to use in UI views (in case if you want to customize some of them later)
            routes.MapLocalizedRoute("Farhad",
                                     "{SeName}",
                                     new { controller = "Product", action = "Farhad" },
                                     new[] { $"Ahora.WebApp.Controllers" });
        }
    }
}
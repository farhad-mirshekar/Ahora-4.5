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
                                       "{entity_name}/{generic_se_name}",
                                       new { controller = "Common", action = "GenericUrl" },
                                       new[] { "Ahora.WebApp.Controllers" });

            routes.MapGenericPathRoute("GenericUrlPages",
                                       "{generic_se_name}",
                                       new { controller = "Common", action = "GenericUrl" },
                                       new[] { "Ahora.WebApp.Controllers" });

            //define this routes to use in UI views (in case if you want to customize some of them later)
            routes.MapLocalizedRoute("Product",
                                     "Product/{SeName}",
                                     new { controller = "Product", action = "Index" },

                                     new[] { $"Ahora.WebApp.Controllers" });

            routes.MapLocalizedRoute("NewsDetail",
                                     "News/{SeName}",
                                     new { controller = "News", action = "Detail" },

                                     new[] { $"Ahora.WebApp.Controllers" });

            routes.MapLocalizedRoute("ArticleDetail",
                                     "Article/{SeName}",
                                     new { controller = "Article", action = "Detail" },

                                     new[] { $"Ahora.WebApp.Controllers" });
            routes.MapLocalizedRoute("EventsDetail",
                                     "Events/{SeName}",
                                     new { controller = "Events", action = "Detail" },

                                     new[] { $"Ahora.WebApp.Controllers" });

            routes.MapGenericPathRoute("DynamicPage",
                                       "{SeName}",
                                       new { controller = "DynamicPage", action = "Index" },
                                       new[] { "Ahora.WebApp.Controllers" });

            routes.MapLocalizedRoute("DynamicPageDetail",
                                    "{PageName}/{SeName}",
                                    new { controller = "DynamicPage", action = "Detail" },
                                    new[] { $"Ahora.WebApp.Controllers" });

        }
    }
}
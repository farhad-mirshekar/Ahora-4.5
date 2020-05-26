using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ahora.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               "Contact",
               "Contact",
               new { controller = "Home", action = "Contact", Name = UrlParameter.Optional },
               namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" });
            routes.MapRoute(
               "Download-File-Zip",
               "Download/File/{Token}",
               new { controller = "Download", action = "FileZip", Name = UrlParameter.Optional },
               namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" });
            routes.MapRoute(
                "Tag",
                "Tag/{Name}",
                new { controller = "Tag", action = "Index", Name = UrlParameter.Optional },
                namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" });
            routes.MapRoute(
            "Faq",
            "Faq/{ID}",
              new { controller = "Faq", action = "Index", ID = UrlParameter.Optional },
              namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
           );
            routes.MapRoute(
              "Events",
              "Events/{Page}",
              new { controller = "Events", action = "Index", Page = UrlParameter.Optional },
              namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
              );
            routes.MapRoute(
              "EventsDetail",
              "Events/{TrackingCode}/{Seo}",
              new { controller = "Events", action = "Detail", TrackingCode = UrlParameter.Optional, Seo = UrlParameter.Optional },
              namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
              );
            routes.MapRoute(
              "Faq-Group",
              "FaqGroup",
                new { controller = "FaqGroup", action = "Index" },
                namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
             );
            routes.MapRoute(
              "News",
              "News/{Page}",
              new { controller = "News", action = "Index", Page = UrlParameter.Optional },
              namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
              );
            routes.MapRoute(
              "NewsDetail",
              "News/{TrackingCode}/{Seo}",
              new { controller = "News", action = "Detail", TrackingCode = UrlParameter.Optional, Seo = UrlParameter.Optional },
              namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
              );
            routes.MapRoute(
              "Article",
              "Article",
                new { controller = "Article", action = "Index" },
                namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
             );
            routes.MapRoute(
              "ArticleDetail",
              "Article/{TrackingCode}/{Seo}",
              new { controller = "Article", action = "Detail", TrackingCode = UrlParameter.Optional, Seo = UrlParameter.Optional },
              namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
              );

            routes.MapRoute(
               "Category",
               "Category/{title}/{ID}",
               new { controller = "Category", action = "Index" },
               namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
               );
            routes.MapRoute(
               "CartEmpty",
               "CartEmpty",
               new { controller = "ShoppingCart", action = "CartEmpty" },
               namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
               );
            routes.MapRoute(
               "Shopping",
               "Shopping",
               new { controller = "ShoppingCart", action = "Shopping" },
               namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
               );
            routes.MapRoute(
                "Cart",
                "Cart",
                new { controller = "ShoppingCart", action = "Index" },
                namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
                );
            routes.MapRoute(
                "AddToCart",
                "product/AddToCart/{ProductID}",
                new { controller = "Product", action = "AddToCart", ProductID = UrlParameter.Optional },
                namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
                );
            routes.MapRoute(
                "product",
                "product/{TrackingCode}/{Title}",
                new { controller = "Product", action = "Index", TrackingCode = UrlParameter.Optional, Title = UrlParameter.Optional },
                namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
                );
            routes.MapRoute(
                "SignUp",
                "SignUp",
                new { controller = "Account", action = "Create" },
                namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
                );
            routes.MapRoute(
                "Login",
                "Login",
                new { controller = "Account", action = "Login" },
                namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
                );
            routes.MapRoute(
               name: "Home",
               url: "Home",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
            );
        }
    }
}

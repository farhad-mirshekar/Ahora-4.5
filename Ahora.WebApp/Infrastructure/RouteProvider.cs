using FM.Portal.FrameWork.Localization;
using FM.Portal.FrameWork.MVC.Routes;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ahora.WebApp.Infrastructure
{
    public partial class RouteProvider : IRouteProvider
    {
        public int Priority
        {
            get
            {
                return 0;
            }
        }

        public void RegisterRoutes(RouteCollection routes)
        {

            routes.MapLocalizedRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index" },
                namespaces: new[] { $"Ahora.WebApp.Controllers" }
            );

            routes.MapLocalizedRoute(
              "ChangeLanguage",
              "ChangeLanguage/{LanguageID}",
              new { controller = "Common", action = "ChangeLanguage", LanguageID = UrlParameter.Optional },
              namespaces: new[] { $"Ahora.WebApp.Controllers" });
            routes.MapLocalizedRoute(
              "ClearCompareProducts",
              "ClearCompareProducts",
              new { controller = "Product", action = "ClearCompareProducts" },
              namespaces: new[] { $"Ahora.WebApp.Controllers" });
            routes.MapLocalizedRoute(
              "RemoveProductFromCompareList",
              "RemoveProductFromCompareList/{ProductID}",
              new { controller = "Product", action = "RemoveProductFromCompareList", ProductID = UrlParameter.Optional },
              namespaces: new[] { $"Ahora.WebApp.Controllers" });
            routes.MapLocalizedRoute(
              "CompareProducts",
              "CompareProducts",
              new { controller = "Product", action = "CompareProducts" },
              namespaces: new[] { $"Ahora.WebApp.Controllers" });
            routes.MapLocalizedRoute(
              "AddProductToCompareList",
              "ComparingProduct/{ProductID}",
              new { controller = "Product", action = "AddProductToCompareList", ProductID = UrlParameter.Optional },
              namespaces: new[] { $"Ahora.WebApp.Controllers" });
            routes.MapLocalizedRoute(
              "GalleryImageDetail",
              "Photo/{TrackingCode}/{Seo}",
              new { controller = "Gallery", action = "ImageDetail", TrackingCode = UrlParameter.Optional, Seo = UrlParameter.Optional },
              namespaces: new[] { $"Ahora.WebApp.Controllers" });
            routes.MapLocalizedRoute(
              "Gallery",
              "Gallery/Image/{Page}",
              new { controller = "Gallery", action = "Image", Page = UrlParameter.Optional },
              namespaces: new[] { $"Ahora.WebApp.Controllers" });
            routes.MapLocalizedRoute(
              "StaticPages",
              "Blog/{TrackingCode}/{Seo}",
              new { controller = "StaticPage", action = "Index", TrackingCode = UrlParameter.Optional, Seo = UrlParameter.Optional },
              namespaces: new[] { $"Ahora.WebApp.Controllers" });
            routes.MapLocalizedRoute(
               "DynamicPages",
               "Pages/{TrackingCode}/{Seo}",
               new { controller = "DynamicPage", action = "Index", TrackingCode = UrlParameter.Optional, Seo = UrlParameter.Optional },
               namespaces: new[] { $"Ahora.WebApp.Controllers" });
            routes.MapLocalizedRoute(
               "PagesPriview",
               "PagesPriview/{TrackingCode}/{Seo}",
               new { controller = "DynamicPage", action = "Detail", TrackingCode = UrlParameter.Optional, Seo = UrlParameter.Optional },
               namespaces: new[] { $"Ahora.WebApp.Controllers" });
            routes.MapLocalizedRoute(
               "Contact",
               "Contact",
               new { controller = "Home", action = "Contact", Name = UrlParameter.Optional },
               namespaces: new[] { $"Ahora.WebApp.Controllers" });
            routes.MapLocalizedRoute(
               "Download-File-Zip",
               "Download/File/{Token}",
               new { controller = "Download", action = "FileZip", Name = UrlParameter.Optional },
               namespaces: new[] { $"Ahora.WebApp.Controllers" });
            routes.MapLocalizedRoute(
                "Tag",
                "Tag/{Name}",
                new { controller = "Tag", action = "Index", Name = UrlParameter.Optional },
                namespaces: new[] { $"Ahora.WebApp.Controllers" });
            routes.MapLocalizedRoute(
            "Faq",
            "Faq/{ID}",
              new { controller = "Faq", action = "Index", ID = UrlParameter.Optional },
              namespaces: new[] { $"Ahora.WebApp.Controllers" }
           );
            routes.MapLocalizedRoute(
            "DisLikeEventsComment",
            "Events/DisLike/{CommentID}",
              new { controller = "Events", action = "DisLike", CommentID = UrlParameter.Optional },
              namespaces: new[] { $"Ahora.WebApp.Controllers" }
           );
            routes.MapLocalizedRoute(
            "LikeEventsComment",
            "Events/Like/{CommentID}",
              new { controller = "Events", action = "Like", CommentID = UrlParameter.Optional },
              namespaces: new[] { $"Ahora.WebApp.Controllers" }
           );
            routes.MapLocalizedRoute(
             "AddEventsComment",
             "Events/AddEventsComment/{EventsID}/{ParentID}",
               new { controller = "Events", action = "AddEventsComment", EventsID = UrlParameter.Optional, ParentID = UrlParameter.Optional },
               namespaces: new[] { $"Ahora.WebApp.Controllers" }
            );
            routes.MapLocalizedRoute(
              "Events",
              "Events/{Page}",
              new { controller = "Events", action = "Index", Page = UrlParameter.Optional },
              namespaces: new[] { $"Ahora.WebApp.Controllers" }
              );
            routes.MapLocalizedRoute(
              "EventsDetail",
              "Events/{TrackingCode}/{Seo}",
              new { controller = "Events", action = "Detail", TrackingCode = UrlParameter.Optional, Seo = UrlParameter.Optional },
              namespaces: new[] { $"Ahora.WebApp.Controllers" }
              );
            routes.MapLocalizedRoute(
              "Faq-Group",
              "FaqGroup/{Page}",
                new { controller = "FaqGroup", action = "Index", Page = UrlParameter.Optional },
                namespaces: new[] { $"Ahora.WebApp.Controllers" }
             );
            routes.MapLocalizedRoute(
            "DisLikeNewsComment",
            "News/DisLike/{CommentID}",
              new { controller = "News", action = "DisLike", CommentID = UrlParameter.Optional },
              namespaces: new[] { $"Ahora.WebApp.Controllers" }
           );
            routes.MapLocalizedRoute(
            "LikeNewsComment",
            "News/Like/{CommentID}",
              new { controller = "News", action = "Like", CommentID = UrlParameter.Optional },
              namespaces: new[] { $"Ahora.WebApp.Controllers" }
           );
            routes.MapLocalizedRoute(
             "AddNewsComment",
             "News/AddNewsComment/{NewsID}/{ParentID}",
               new { controller = "News", action = "AddNewsComment", NewsID = UrlParameter.Optional, ParentID = UrlParameter.Optional },
               namespaces: new[] { $"Ahora.WebApp.Controllers" }
            );
            routes.MapLocalizedRoute(
              "News",
              "News/{Page}",
              new { controller = "News", action = "Index", Page = UrlParameter.Optional },
              namespaces: new[] { $"Ahora.WebApp.Controllers" }
              );
            routes.MapLocalizedRoute(
              "NewsDetail",
              "News/{TrackingCode}/{Seo}",
              new { controller = "News", action = "Detail", TrackingCode = UrlParameter.Optional, Seo = UrlParameter.Optional },
              namespaces: new[] { $"Ahora.WebApp.Controllers" }
              );
            routes.MapLocalizedRoute(
            "DisLikeArticleComment",
            "Article/DisLike/{CommentID}",
              new { controller = "Article", action = "DisLike", CommentID = UrlParameter.Optional },
              namespaces: new[] { $"Ahora.WebApp.Controllers" }
           );
            routes.MapLocalizedRoute(
            "LikeArticleComment",
            "Article/Like/{CommentID}",
              new { controller = "Article", action = "Like", CommentID = UrlParameter.Optional },
              namespaces: new[] { $"Ahora.WebApp.Controllers" }
           );
            routes.MapLocalizedRoute(
             "AddArticleComment",
             "Article/AddArticleComment/{ArticleID}/{ParentID}",
               new { controller = "Article", action = "AddArticleComment", ArticleID = UrlParameter.Optional, ParentID = UrlParameter.Optional },
               namespaces: new[] { $"Ahora.WebApp.Controllers" }
            );
            routes.MapLocalizedRoute(
              "Article",
              "Article/{Page}",
                new { controller = "Article", action = "Index", Page = UrlParameter.Optional },
                namespaces: new[] { $"Ahora.WebApp.Controllers" }
             );
            routes.MapLocalizedRoute(
              "ArticleDetail",
              "Article/{TrackingCode}/{Seo}",
              new { controller = "Article", action = "Detail", TrackingCode = UrlParameter.Optional, Seo = UrlParameter.Optional },
              namespaces: new[] { $"Ahora.WebApp.Controllers" }
              );

            routes.MapLocalizedRoute(
               "Category",
               "Category/{title}/{ID}",
               new { controller = "Category", action = "Index" },
               namespaces: new[] { $"Ahora.WebApp.Controllers" }
               );
            routes.MapLocalizedRoute(
               "CartEmpty",
               "CartEmpty",
               new { controller = "ShoppingCart", action = "CartEmpty" },
               namespaces: new[] { $"Ahora.WebApp.Controllers" }
               );
            routes.MapLocalizedRoute(
               "Shopping",
               "Shopping",
               new { controller = "ShoppingCart", action = "Shopping" },
               namespaces: new[] { $"Ahora.WebApp.Controllers" }
               );
            routes.MapLocalizedRoute(
                "Cart",
                "Cart",
                new { controller = "ShoppingCart", action = "Index" },
                namespaces: new[] { $"Ahora.WebApp.Controllers" }
                );
            routes.MapLocalizedRoute(
                "AddToCart",
                "product/AddToCart/{ProductID}",
                new { controller = "Product", action = "AddToCart", ProductID = UrlParameter.Optional },
                namespaces: new[] { $"Ahora.WebApp.Controllers" }
                );
            routes.MapLocalizedRoute(
            "DisLikeProductComment",
            "Product/DisLike/{CommentID}",
              new { controller = "Product", action = "DisLike", CommentID = UrlParameter.Optional },
              namespaces: new[] { $"Ahora.WebApp.Controllers" }
           );
            routes.MapLocalizedRoute(
            "LikeProductComment",
            "Product/Like/{CommentID}",
              new { controller = "Product", action = "Like", CommentID = UrlParameter.Optional },
              namespaces: new[] { $"Ahora.WebApp.Controllers" }
           );
            routes.MapLocalizedRoute(
             "AddProductComment",
             "Product/AddProductComment/{ProductID}/{ParentID}",
               new { controller = "Product", action = "AddProductComment", ProductID = UrlParameter.Optional, ParentID = UrlParameter.Optional },
               namespaces: new[] { $"Ahora.WebApp.Controllers" }
            );
            routes.MapLocalizedRoute(
                "SignUp",
                "SignUp",
                new { controller = "Account", action = "Create" },
                namespaces: new[] { $"Ahora.WebApp.Controllers" }
                );
            routes.MapLocalizedRoute(
                "Login",
                "login",
                new { controller = "Account", action = "Login" },
                namespaces: new[] { $"Ahora.WebApp.Controllers" }
                );
            routes.MapLocalizedRoute(
               "Logout",
               "logout/",
               new { controller = "Account", action = "Logout" },
               namespaces: new[] { $"Ahora.WebApp.Controllers" }
               );
        }
    }
}
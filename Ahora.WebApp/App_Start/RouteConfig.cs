﻿//using FM.Portal.FrameWork.MVC.Routes;
//using FM.Portal.FrameWork.Unity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Routing;
//using Unity;

//namespace Ahora.WebApp
//{
//    public class RouteConfig
//    {
//        public static void RegisterRoutes(RouteCollection routes)
//        {
//            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
//            var routePublisher = (IRoutePublisher)DependencyResolver.Current.GetService(typeof(IRoutePublisher));
//            routePublisher.RegisterRoutes(routes);
//            // routes.MapRoute(
//            //   "ChangeLanguage",
//            //   "ChangeLanguage/{LanguageID}",
//            //   new { controller = "Common", action = "ChangeLanguage", LanguageID = UrlParameter.Optional },
//            //   namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" });
//            // routes.MapRoute(
//            //   "ClearCompareProducts",
//            //   "ClearCompareProducts",
//            //   new { controller = "Product", action = "ClearCompareProducts" },
//            //   namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" });
//            // routes.MapRoute(
//            //   "RemoveProductFromCompareList",
//            //   "RemoveProductFromCompareList/{ProductID}",
//            //   new { controller = "Product", action = "RemoveProductFromCompareList", ProductID = UrlParameter.Optional },
//            //   namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" });
//            // routes.MapRoute(
//            //   "CompareProducts",
//            //   "CompareProducts",
//            //   new { controller = "Product", action = "CompareProducts" },
//            //   namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" });
//            // routes.MapRoute(
//            //   "AddProductToCompareList",
//            //   "ComparingProduct/{ProductID}",
//            //   new { controller = "Product", action = "AddProductToCompareList", ProductID = UrlParameter.Optional },
//            //   namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" });
//            // routes.MapRoute(
//            //   "GalleryImageDetail",
//            //   "Photo/{TrackingCode}/{Seo}",
//            //   new { controller = "Gallery", action = "ImageDetail", TrackingCode = UrlParameter.Optional, Seo = UrlParameter.Optional },
//            //   namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" });
//            // routes.MapRoute(
//            //   "Gallery",
//            //   "Gallery/Image/{Page}",
//            //   new { controller = "Gallery", action = "Image", Page = UrlParameter.Optional },
//            //   namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" });
//            // routes.MapRoute(
//            //   "StaticPages",
//            //   "Blog/{TrackingCode}/{Seo}",
//            //   new { controller = "StaticPage", action = "Index", TrackingCode = UrlParameter.Optional, Seo = UrlParameter.Optional },
//            //   namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" });
//            // routes.MapRoute(
//            //    "DynamicPages",
//            //    "Pages/{TrackingCode}/{Seo}",
//            //    new { controller = "DynamicPage", action = "Index", TrackingCode = UrlParameter.Optional, Seo = UrlParameter.Optional },
//            //    namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" });
//            // routes.MapRoute(
//            //    "PagesPriview",
//            //    "PagesPriview/{TrackingCode}/{Seo}",
//            //    new { controller = "DynamicPage", action = "Detail", TrackingCode = UrlParameter.Optional, Seo = UrlParameter.Optional },
//            //    namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" });
//            // routes.MapRoute(
//            //    "Contact",
//            //    "Contact",
//            //    new { controller = "Home", action = "Contact", Name = UrlParameter.Optional },
//            //    namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" });
//            // routes.MapRoute(
//            //    "Download-File-Zip",
//            //    "Download/File/{Token}",
//            //    new { controller = "Download", action = "FileZip", Name = UrlParameter.Optional },
//            //    namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" });
//            // routes.MapRoute(
//            //     "Tag",
//            //     "Tag/{Name}",
//            //     new { controller = "Tag", action = "Index", Name = UrlParameter.Optional },
//            //     namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" });
//            // routes.MapRoute(
//            // "Faq",
//            // "Faq/{ID}",
//            //   new { controller = "Faq", action = "Index", ID = UrlParameter.Optional },
//            //   namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            //);
//            // routes.MapRoute(
//            // "DisLikeEventsComment",
//            // "Events/DisLike/{CommentID}",
//            //   new { controller = "Events", action = "DisLike", CommentID = UrlParameter.Optional },
//            //   namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            //);
//            // routes.MapRoute(
//            // "LikeEventsComment",
//            // "Events/Like/{CommentID}",
//            //   new { controller = "Events", action = "Like", CommentID = UrlParameter.Optional },
//            //   namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            //);
//            // routes.MapRoute(
//            //  "AddEventsComment",
//            //  "Events/AddEventsComment/{EventsID}/{ParentID}",
//            //    new { controller = "Events", action = "AddEventsComment", EventsID = UrlParameter.Optional, ParentID = UrlParameter.Optional },
//            //    namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            // );
//            // routes.MapRoute(
//            //   "Events",
//            //   "Events/{Page}",
//            //   new { controller = "Events", action = "Index", Page = UrlParameter.Optional },
//            //   namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            //   );
//            // routes.MapRoute(
//            //   "EventsDetail",
//            //   "Events/{TrackingCode}/{Seo}",
//            //   new { controller = "Events", action = "Detail", TrackingCode = UrlParameter.Optional, Seo = UrlParameter.Optional },
//            //   namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            //   );
//            // routes.MapRoute(
//            //   "Faq-Group",
//            //   "FaqGroup/{Page}",
//            //     new { controller = "FaqGroup", action = "Index", Page = UrlParameter.Optional },
//            //     namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            //  );
//            // routes.MapRoute(
//            // "DisLikeNewsComment",
//            // "News/DisLike/{CommentID}",
//            //   new { controller = "News", action = "DisLike", CommentID = UrlParameter.Optional },
//            //   namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            //);
//            // routes.MapRoute(
//            // "LikeNewsComment",
//            // "News/Like/{CommentID}",
//            //   new { controller = "News", action = "Like", CommentID = UrlParameter.Optional },
//            //   namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            //);
//            // routes.MapRoute(
//            //  "AddNewsComment",
//            //  "News/AddNewsComment/{NewsID}/{ParentID}",
//            //    new { controller = "News", action = "AddNewsComment", NewsID = UrlParameter.Optional, ParentID = UrlParameter.Optional },
//            //    namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            // );
//            // routes.MapRoute(
//            //   "News",
//            //   "News/{Page}",
//            //   new { controller = "News", action = "Index", Page = UrlParameter.Optional },
//            //   namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            //   );
//            // routes.MapRoute(
//            //   "NewsDetail",
//            //   "News/{TrackingCode}/{Seo}",
//            //   new { controller = "News", action = "Detail", TrackingCode = UrlParameter.Optional, Seo = UrlParameter.Optional },
//            //   namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            //   );
//            // routes.MapRoute(
//            // "DisLikeArticleComment",
//            // "Article/DisLike/{CommentID}",
//            //   new { controller = "Article", action = "DisLike", CommentID = UrlParameter.Optional },
//            //   namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            //);
//            // routes.MapRoute(
//            // "LikeArticleComment",
//            // "Article/Like/{CommentID}",
//            //   new { controller = "Article", action = "Like", CommentID = UrlParameter.Optional },
//            //   namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            //);
//            // routes.MapRoute(
//            //  "AddArticleComment",
//            //  "Article/AddArticleComment/{ArticleID}/{ParentID}",
//            //    new { controller = "Article", action = "AddArticleComment", ArticleID = UrlParameter.Optional, ParentID = UrlParameter.Optional },
//            //    namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            // );
//            // routes.MapRoute(
//            //   "Article",
//            //   "Article/{Page}",
//            //     new { controller = "Article", action = "Index", Page = UrlParameter.Optional },
//            //     namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            //  );
//            // routes.MapRoute(
//            //   "ArticleDetail",
//            //   "Article/{TrackingCode}/{Seo}",
//            //   new { controller = "Article", action = "Detail", TrackingCode = UrlParameter.Optional, Seo = UrlParameter.Optional },
//            //   namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            //   );

//            // routes.MapRoute(
//            //    "Category",
//            //    "Category/{title}/{ID}",
//            //    new { controller = "Category", action = "Index" },
//            //    namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            //    );
//            // routes.MapRoute(
//            //    "CartEmpty",
//            //    "CartEmpty",
//            //    new { controller = "ShoppingCart", action = "CartEmpty" },
//            //    namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            //    );
//            // routes.MapRoute(
//            //    "Shopping",
//            //    "Shopping",
//            //    new { controller = "ShoppingCart", action = "Shopping" },
//            //    namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            //    );
//            // routes.MapRoute(
//            //     "Cart",
//            //     "Cart",
//            //     new { controller = "ShoppingCart", action = "Index" },
//            //     namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            //     );
//            // routes.MapRoute(
//            //     "AddToCart",
//            //     "product/AddToCart/{ProductID}",
//            //     new { controller = "Product", action = "AddToCart", ProductID = UrlParameter.Optional },
//            //     namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            //     );
//            // routes.MapRoute(
//            // "DisLikeProductComment",
//            // "Product/DisLike/{CommentID}",
//            //   new { controller = "Product", action = "DisLike", CommentID = UrlParameter.Optional },
//            //   namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            //);
//            // routes.MapRoute(
//            // "LikeProductComment",
//            // "Product/Like/{CommentID}",
//            //   new { controller = "Product", action = "Like", CommentID = UrlParameter.Optional },
//            //   namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            //);
//            // routes.MapRoute(
//            //  "AddProductComment",
//            //  "Product/AddProductComment/{ProductID}/{ParentID}",
//            //    new { controller = "Product", action = "AddProductComment", ProductID = UrlParameter.Optional, ParentID = UrlParameter.Optional },
//            //    namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            // );
//            // routes.MapRoute(
//            //     "product",
//            //     "product/{TrackingCode}/{Title}",
//            //     new { controller = "Product", action = "Index", TrackingCode = UrlParameter.Optional, Title = UrlParameter.Optional },
//            //     namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            //     );
//            // routes.MapRoute(
//            //     "SignUp",
//            //     "SignUp",
//            //     new { controller = "Account", action = "Create" },
//            //     namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            //     );
//            // routes.MapRoute(
//            //     "Login",
//            //     "Login",
//            //     new { controller = "Account", action = "Login" },
//            //     namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            //     );
//            // routes.MapRoute(
//            //    name: "Home",
//            //    url: "Home",
//            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
//            //    namespaces: new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
//            //);
//            routes.MapRoute(
//                name: "Default",
//                url: "{controller}/{action}/{id}",
//                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
//                namespaces: new[] { $"Ahora.WebApp.Controllers" }
//            );
//        }
//    }
//}

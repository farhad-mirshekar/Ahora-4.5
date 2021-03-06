using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Domain;
using FM.Portal.FrameWork.AutoMapper;
using FM.Portal.FrameWork.Caching;
using FM.Portal.FrameWork.MVC.Routes;
using FM.Portal.FrameWork.Unity;
using FM.Portal.Infrastructure.DAL;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Unity.Injection;

namespace Ahora.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //register custom routes (plugins, etc)
            var routePublisher = (IRoutePublisher)DependencyResolver.Current.GetService(typeof(IRoutePublisher));
            routePublisher.RegisterRoutes(routes);

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Ahora.WebApp.Controllers" }
            );
        }
        protected void Application_Start(object sender, EventArgs e)
        {
            UnityConfig.RegisterComponents();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            siteOption();
            var autoMapper = new AutoMapperStartupTask();
            autoMapper.Execute();
        }
        private void siteOption()
        {
            var container = new UnityContainer();
            container.RegisterType<IGeneralSettingDataSource, GeneralSettingDataSource>();
            container.RegisterType<IGeneralSettingService, GeneralSettingService>();
            container.RegisterType<ICacheService, CacheService>();
            container.RegisterType<HttpContextBase>(new InjectionFactory(_ =>
                    new HttpContextWrapper(HttpContext.Current)));
            ICacheService _service = container.Resolve<ICacheService>();
            _service.GetSiteSettings();
        }
        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            var app = sender as HttpApplication;

            app?.Context?.Response.Headers.Remove("Server");
        }
        #region Application_AuthenticateRequest
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            if (Context.User == null)
                return;

            var authCookie = Context.Request.Cookies[System.Web.Security.FormsAuthentication.FormsCookieName];
            if (authCookie == null || authCookie.Value == "")
                return;

            var authTicket = System.Web.Security.FormsAuthentication.Decrypt(authCookie.Value);


            // retrieve roles from UserData
            if (authTicket == null) return;
            var roles = authTicket.UserData.Split(',');


            Context.User = new System.Security.Principal.GenericPrincipal(Context.User.Identity, roles);
        }
        #endregion
    }
}

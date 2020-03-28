using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Domain;
using FM.Portal.FrameWork.Caching;
using FM.Portal.FrameWork.Unity;
using FM.Portal.Infrastructure.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
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
        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();
            siteOption();
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

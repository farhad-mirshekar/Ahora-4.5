using FM.Portal.Core.Caching;
using FM.Portal.Core.Common;
using FM.Portal.Core.Common.Serializer;
using FM.Portal.Core.Infrastructure;
using FM.Portal.Core.Owin;
using FM.Portal.FrameWork.Caching;
using FM.Portal.FrameWork.Email;
using FM.Portal.FrameWork.MVC.Routes;
using System.Web;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace FM.Portal.FrameWork.Unity.General
{
   public static class GeneralDependencyRegister
    {
        public static void RegisterType(UnityContainer container)
        {
            container.RegisterType<IWorkContext, WebWorkContext>(new ContainerControlledLifetimeManager());
            container.RegisterType<IWebHelper, WebHelper>(new ContainerControlledLifetimeManager());
            container.RegisterType<ITypeFinder, AppDomainTypeFinder>(new ContainerControlledLifetimeManager());
            container.RegisterSingleton<IRoutePublisher, RoutePublisher>();
            container.RegisterType<ICacheManager, MemoryCacheManager>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmailService, EmailService>();
            container.RegisterType<ICacheService, CacheService>();
            container.RegisterType<HttpContextBase>(new InjectionFactory(_ =>
                new HttpContextWrapper(HttpContext.Current)));
            container.RegisterType<IRequestInfo, RequestInfo>();
            container.RegisterType<IAppSetting, AppSetting>();
            container.RegisterType<IObjectSerializer, ObjectSerializer>();


            
        }
    }
}

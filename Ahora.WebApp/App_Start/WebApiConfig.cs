using FM.Portal.FrameWork.Unity;
using System.Web.Http;
using Unity;

namespace Ahora.WebApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            var container = new UnityContainer();
            var loadServices = new LoadServices(container);
            config.DependencyResolver = new UnityResolver(loadServices.Load());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

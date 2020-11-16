using Ahora.WebApp.Controllers;
using FM.Portal.FrameWork.Unity;
using System.Web.Mvc;
using System.Web.Routing;
using Unity;

namespace FM.Portal.FrameWork.MVC.Controller
{
   public class BaseController<T> : System.Web.Mvc.Controller
        where T: Core.Service.IService
    {
        public BaseController(T service)
        {
            _service = service;
        }
        protected readonly T _service;

        protected virtual ActionResult InvokeHttp404()
        {
            // Call target Controller and pass the routeData.
            var container = new UnityContainer();
            var unityDependencyResolver = new UnityDependencyResolver(container);
            IController errorController = (CommonController)unityDependencyResolver.GetService(typeof(CommonController));

            var routeData = new RouteData();
            routeData.Values.Add("controller", "Common");
            routeData.Values.Add("action", "PageNotFound");

            errorController.Execute(new RequestContext(this.HttpContext, routeData));

            return new EmptyResult();
        }
    }
}

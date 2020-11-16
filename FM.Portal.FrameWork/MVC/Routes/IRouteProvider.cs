using System.Web.Routing;

namespace FM.Portal.FrameWork.MVC.Routes
{
    public interface IRouteProvider
    {
        void RegisterRoutes(RouteCollection routes);

        int Priority { get; }
    }
}

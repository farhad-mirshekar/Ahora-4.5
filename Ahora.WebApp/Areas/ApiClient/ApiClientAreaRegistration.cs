using System.Web.Mvc;

namespace Ahora.WebApp.Areas.ApiClient
{
    public class ApiClientAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ApiClient";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ApiClient_default",
                "ApiClient/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
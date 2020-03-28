using System.Web.Mvc;
using System.Web.Optimization;

namespace Ahora.WebApp.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller="Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Ahora.WebApp.Areas.Admin.Controllers" }
            );
            RegisterBundles();
        }
        private void RegisterBundles()
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
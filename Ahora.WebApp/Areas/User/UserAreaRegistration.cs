using System.Web.Mvc;

namespace Ahora.WebApp.Areas.User
{
    public class UserAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "User";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "User_OrderDetail",
                "User/Home/OrderDetail/{PaymentID}",
                new { controller = "Home", action = "OrderDetail", PaymentID = UrlParameter.Optional }
            );
            context.MapRoute(
                "User_Orders",
                "User/Home/Orders/{Page}",
                new { controller="Home", action = "Orders", Page = UrlParameter.Optional }
            );
            context.MapRoute(
                "User_default",
                "User/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
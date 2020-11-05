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
               "User_PrintToPdf",
               "User/Home/PrintToPdf/{PaymentID}",
               new { controller = "Home", action = "PrintToPdf", PaymentID = UrlParameter.Optional }
           );
            context.MapRoute(
               "User_Profile",
               "User/Home/Profile",
               new { controller = "Profile", action = "Index" }
           );
            context.MapRoute(
               "User_AddressEdit",
               "User/Home/Address/Edit/{ID}",
               new { controller = "Address", action = "Edit", ID = UrlParameter.Optional }
           );
            context.MapRoute(
               "User_AddressCreate",
               "User/Home/Address/Create",
               new { controller = "Address", action = "Create" }
           );
            context.MapRoute(
               "User_AddressList",
               "User/Home/Address/{Page}",
               new { controller = "Address", action = "Index", Page = UrlParameter.Optional }
           );
            context.MapRoute(
                "User_OrderDetail",
                "User/Home/OrderDetail/{PaymentID}",
                new { controller = "Home", action = "OrderDetail", PaymentID = UrlParameter.Optional }
            );
            context.MapRoute(
                "User_Orders",
                "User/Home/Orders/{Page}",
                new { controller = "Home", action = "Orders", Page = UrlParameter.Optional }
            );
            context.MapRoute(
                "User_default",
                "User/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
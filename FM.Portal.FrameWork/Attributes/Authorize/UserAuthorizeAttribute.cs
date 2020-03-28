

using System;
using System.Net;
using System.Web.Mvc;

namespace FM.Portal.FrameWork.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
   public sealed class UserAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
                
            }
            else
            {
                System.Web.Security.FormsAuthentication.SignOut();
                handleAjaxRequest(filterContext);
                base.HandleUnauthorizedRequest(filterContext);
            }
        }

        private static void handleAjaxRequest(AuthorizationContext filterContext)
        {
            var ctx = filterContext.HttpContext;
            if (!ctx.Request.IsAjaxRequest())
                return;

            ctx.Response.StatusCode = (int)HttpStatusCode.Forbidden; //براي درخواست‌هاي اجكسي اعتبار سنجي نشده
            ctx.Response.End();
        }
    }
}

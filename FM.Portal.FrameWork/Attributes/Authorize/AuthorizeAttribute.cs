
using System;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;

namespace FM.Portal.FrameWork.Attributes.Authorize
{
  public class AuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if(actionContext.Request.Headers.Any(x=>x.Key == "Authorization") && HttpContext.Current.User.Identity.AuthenticationType != "Bearer")
            {
                HttpResponseMessage response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.NonAuthoritativeInformation,-397);
                actionContext.Response = response;
                return;
            }
            base.OnAuthorization(actionContext);
        }
    }
}

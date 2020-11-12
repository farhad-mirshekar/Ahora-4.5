using FM.Portal.Core.Infrastructure;
using FM.Portal.FrameWork.Unity;
using System;
using System.Web.Mvc;
using Unity;

namespace FM.Portal.FrameWork.Seo
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class WwwRequirementAttribute : FilterAttribute, IAuthorizationFilter
    {
        public WwwRequirement WwwRequirement { get; set; }
        public WwwRequirementAttribute(WwwRequirement WwwRequirement)
        {
            this.WwwRequirement = WwwRequirement;
        }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("");
            if (filterContext.IsChildAction)
                return;

            // only redirect for GET requests, 
            // otherwise the browser might not propagate the verb and request body correctly.
            if (!String.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                return;

            //ignore this rule for localhost
            if (filterContext.HttpContext.Request.IsLocal)
                return;

            switch (WwwRequirement)
            {
                case WwwRequirement.WithWww:
                    {
                        var container = new UnityContainer();
                        var unityDependencyResolver = new UnityDependencyResolver(container);
                        var webHelper = (IWebHelper)unityDependencyResolver.GetService(typeof(IWebHelper));
                        string url = webHelper.GetThisPageUrl(true);
                        var currentConnectionSecured = webHelper.IsCurrentConnectionSecured();
                        if (currentConnectionSecured)
                        {
                            bool startsWith3W = url.StartsWith("https://www.", StringComparison.OrdinalIgnoreCase);
                            if (!startsWith3W)
                            {
                                url = url.Replace("https://", "https://www.");

                                //301 (permanent) redirection
                                filterContext.Result = new RedirectResult(url, true);
                            }
                        }
                        else
                        {
                            bool startsWith3W = url.StartsWith("http://www.", StringComparison.OrdinalIgnoreCase);
                            if (!startsWith3W)
                            {
                                url = url.Replace("http://", "http://www.");

                                filterContext.Result = new RedirectResult(url, true);
                            }
                        }
                    }
                    break;
                
                case WwwRequirement.NoMatter:
                    {
                        //do nothing
                    }
                    break;
                default:
                    throw new Exception("Not supported WwwRequirement parameter");
            }
        }
    }
}

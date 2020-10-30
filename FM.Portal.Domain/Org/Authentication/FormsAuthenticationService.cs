using FM.Portal.Core;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using System;
using System.Web;
using System.Web.Security;

namespace FM.Portal.Domain
{
    public class FormsAuthenticationService : IAuthenticationService
    {
        private readonly TimeSpan _expirationTimeSpan;
        private readonly HttpContextBase _httpContext;
        public FormsAuthenticationService(HttpContextBase httpContext)
        {
            _httpContext = httpContext;
            _expirationTimeSpan = FormsAuthentication.Timeout;
        }
        public Result SignIn(User user, bool createPersistentCookie)
        {
            try
            {
                var now = DateTime.UtcNow.ToLocalTime();

                var ticket = new FormsAuthenticationTicket(
                    1 /*version*/,
                    user.Username,
                    now,
                    now.Add(_expirationTimeSpan),
                    createPersistentCookie,
                    user.Type == UserType.کاربر_درون_سازمانی ? "Admin":"User",
                    FormsAuthentication.FormsCookiePath);

                var encryptedTicket = FormsAuthentication.Encrypt(ticket);

                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                cookie.HttpOnly = true;
                if (ticket.IsPersistent)
                {
                    cookie.Expires = ticket.Expiration;
                }
                cookie.Secure = FormsAuthentication.RequireSSL;
                cookie.Path = FormsAuthentication.FormsCookiePath;
                if (FormsAuthentication.CookieDomain != null)
                {
                    cookie.Domain = FormsAuthentication.CookieDomain;
                }

                _httpContext.Response.Cookies.Add(cookie);
                return Result.Successful();
            }
            catch(Exception e) { return Result.Failure(); }
        }

        public Result SignOut()
        {
            try
            {
                FormsAuthentication.SignOut();
                return Result.Successful();
            }
            catch(Exception e) { return Result.Failure(); }
        }
    }
}

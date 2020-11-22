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
        private readonly IUserService _userService;
        public FormsAuthenticationService(HttpContextBase httpContext
                                        , IUserService userService)
        {
            _httpContext = httpContext;
            _expirationTimeSpan = FormsAuthentication.Timeout;
            _userService = userService;
        }

        private static User _cachedUser;

        public Result<User> GetAuthenticatedCustomer()
        {
            if (_cachedUser != null)
                return Result<User>.Successful(data: _cachedUser);

            if (_httpContext == null ||
                _httpContext.Request == null ||
                !_httpContext.Request.IsAuthenticated ||
                !(_httpContext.User.Identity is FormsIdentity))
            {
                return Result<User>.Successful(data: null);
            }

            var formsIdentity = (FormsIdentity)_httpContext.User.Identity;
            var userResult = GetAuthenticatedCustomerFromTicket(formsIdentity.Ticket);
            if (!userResult.Success)
                return Result<User>.Failure();
            var user = userResult.Data;

            if (user != null && user.Enabled)
                _cachedUser = user;
            return Result<User>.Successful(data: user);
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
                    user.Type == UserType.کاربر_درون_سازمانی ? "Admin" : "User",
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
                _cachedUser = user;
                return Result.Successful();
            }
            catch (Exception e) { return Result.Failure(); }
        }

        public Result SignOut()
        {
            try
            {
                _cachedUser = null;
                FormsAuthentication.SignOut();
                return Result.Successful();
            }
            catch (Exception e) { return Result.Failure(); }
        }

        public virtual Result<User> GetAuthenticatedCustomerFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            var usernameOrEmail = ticket.Name;

            if (String.IsNullOrWhiteSpace(usernameOrEmail))
                return null;
            var userResult = _userService.Get(usernameOrEmail, null, null, UserType.Unknown);
            return userResult;
        }
    }
}

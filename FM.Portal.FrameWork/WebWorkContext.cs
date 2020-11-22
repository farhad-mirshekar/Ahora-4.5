using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using System;
using System.Web;

namespace FM.Portal.FrameWork
{
    public class WebWorkContext : IWorkContext
    {
        private readonly ILanguageService _languageService;
        private readonly IAuthenticationService _authenticationService;
        private readonly HttpContextBase _httpContext;
        public WebWorkContext(ILanguageService languageService
                             , IAuthenticationService authenticationService
                             , HttpContextBase httpContext)
        {
            _languageService = languageService;
            _authenticationService = authenticationService;
            _httpContext = httpContext;
        }
        private User _cachedUser;
        public Language WorkingLanguage { get; set; }
        public User User
        {
            get
            {
                
                if (_cachedUser != null)
                {
                    _IsAdmin(_cachedUser);
                    return _cachedUser;
                }

                User user = null;
                if (user == null || !user.Enabled)
                {
                    var userResult = _authenticationService.GetAuthenticatedCustomer();
                    if (!userResult.Success)
                        return null;
                    user = userResult.Data;
                    _cachedUser = user;
                    _IsAdmin(_cachedUser);
                }

                return user;
            }
            set
            {
                _cachedUser = value;
            }
        }
        public bool IsAdmin { get; set; }
        public Guid? ShoppingID
        {
            get
            {
                if (_httpContext.Request.Cookies["ShoppingID"] != null)
                    return SQLHelper.CheckGuidNull(_httpContext.Request.Cookies["ShoppingID"].Value);
                return null;
            }
        }

        private void _IsAdmin(User user)
        {
            if (user != null && user.Type == UserType.کاربر_درون_سازمانی)
                IsAdmin = true;
        }
    }
}

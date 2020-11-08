using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using System;

namespace FM.Portal.FrameWork
{
    public class WebWorkContext : IWorkContext
    {
        private readonly ILanguageService _languageService;
        private readonly IAuthenticationService _authenticationService;
        public WebWorkContext(ILanguageService languageService
                             , IAuthenticationService authenticationService)
        {
            _languageService = languageService;
            _authenticationService = authenticationService;
        }
        private User _cachedUser;
        public Language WorkingLanguage { get; set; }
        public User User
        {
            get
            {
                if (_cachedUser != null)
                    return _cachedUser;

                User user = null;
                if (user == null ||!user.Enabled)
                {
                   var userResult = _authenticationService.GetAuthenticatedCustomer();
                    if (!userResult.Success)
                        return null;
                    user = userResult.Data;
                    _cachedUser = user;
                }

                return user;
            }
            set
            {
                _cachedUser = value;
            }
        }
        public bool IsAdmin { get; set; }
    }
}

using FM.Portal.Core.Model;

namespace FM.Portal.Core.Service
{
   public interface IAuthenticationService
    {
        Result SignIn(User user, bool createPersistentCookie);
        Result SignOut();
        Result<User> GetAuthenticatedCustomer();
    }
}

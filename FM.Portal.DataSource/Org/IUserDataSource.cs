using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;

namespace FM.Portal.DataSource
{
    public interface IUserDataSource : IDataSource
    {
        Result<User> Insert(User model);
        Result<User> Update(User model);
        Result<User> Get(Guid? ID, string Username, string Password, string NationalCode);
        Result SetPassword(SetPasswordVM model);
    }
}

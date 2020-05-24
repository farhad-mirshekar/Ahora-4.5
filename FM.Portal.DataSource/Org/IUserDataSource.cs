using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;
using System.Data;

namespace FM.Portal.DataSource
{
    public interface IUserDataSource : IDataSource
    {
        Result<User> Insert(User model);
        Result<User> Update(User model);
        Result<User> Get(Guid? ID, string Username, string Password, string NationalCode , UserType userType);
        Result SetPassword(SetPasswordVM model);
        DataTable List();
    }
}

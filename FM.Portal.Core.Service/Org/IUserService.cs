﻿using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IUserService : IService
    {
        Result<User> Add(User model);
        Result<User> Edit(User model);
        Result<User> Get(Guid ID);
        Result<User> Get(string Username , string Password,string NationalCode , UserType userType);
        Result SetPassword(SetPasswordVM model);
        Result<List<User>> List();
        Result ResetPassword(Guid UserID);
    }
}

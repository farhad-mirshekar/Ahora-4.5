using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IUserAddressService : IService
    {
        Result<UserAddress> Add(UserAddress model);
        Result<UserAddress> Edit(UserAddress model);
        Result<UserAddress> Get(Guid ID);
        Result<List<UserAddress>> List(Guid ID);
        Result.Result Remove(Guid ID);
    }
}

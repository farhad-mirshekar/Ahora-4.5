using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IUserAddressService : IService
    {
        Result<UserAddress> Add(UserAddress model);
        Result<UserAddress> Edit(UserAddress model);
        Result<UserAddress> Get(Guid ID);
        Result<List<UserAddress>> List(UserAddressListVM listVM);
        Result Remove(Guid ID);
    }
}

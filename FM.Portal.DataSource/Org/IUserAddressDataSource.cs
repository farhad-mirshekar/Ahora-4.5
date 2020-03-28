using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IUserAddressDataSource : IDataSource
    {
        Result<UserAddress> Insert(UserAddress model);
        Result<UserAddress> Update(UserAddress model);
        Result<UserAddress> Get(Guid ID);
        Result Delete(Guid ID);
        DataTable List(Guid ID);
    }
}

using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;

namespace FM.Portal.DataSource
{
   public interface IRefreshTokenDataSource:IDataSource
    {
        Result<RefreshToken> Create(RefreshToken model);

        Result Delete(Guid ID);

        Result<RefreshToken> Get(Guid ID);
    }
}

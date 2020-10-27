using FM.Portal.Core.Model;
using System;

namespace FM.Portal.Core.Service
{
   public interface IRefreshTokenService : IService
    {
        Result<RefreshToken> Add(RefreshToken model);

        Result Delete(Guid ID);

        Result<RefreshToken> Get(Guid ID);
    }
}

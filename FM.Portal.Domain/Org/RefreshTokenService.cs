using System;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;

namespace FM.Portal.Domain
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenDataSource _dataSource;
        public RefreshTokenService(IRefreshTokenDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<RefreshToken> Add(RefreshToken model)
        => _dataSource.Create(model);

        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<RefreshToken> Get(Guid ID)
        => _dataSource.Get(ID);
    }
}

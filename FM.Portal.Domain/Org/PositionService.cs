
using System;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core.Result;

namespace FM.Portal.Domain
{
    public class PositionService : IPositionService
    {
        private readonly IPositionDataSource _dataSource;
        public PositionService(IPositionDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public Result<Position> Add(Position model)
        {
            return _dataSource.Insert(model);
        }

        public Result<Position> Edit(Position model)
        {
            return _dataSource.Update(model);
        }

        public Result<Position> Get(Guid ID)
        {
            return _dataSource.Get(ID);
        }

        public Result<PositionDefaultVM> PositionDefault(Guid userID)
        => _dataSource.PositionDefault(userID);
    }
}

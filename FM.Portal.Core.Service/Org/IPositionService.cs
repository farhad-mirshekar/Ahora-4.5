using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
    public interface IPositionService : IService
    {
        Result<Position> GetDefaultPosition(Guid userID);
        Result<Position> Add(Position model);
        Result<Position> Edit(Position model);
        Result<Position> Get(Guid ID);
        Result<List<Position>> List(PositionListVM model);
        Result<List<Position>> ListByUser(PositionListVM model);
        Result.Result SetDefault(Guid ID);
    }
}

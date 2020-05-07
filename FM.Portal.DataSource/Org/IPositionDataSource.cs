using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IPositionDataSource : IDataSource
    {
        Result<PositionDefaultVM> PositionDefault(Guid userID);
        Result<Position> Insert(Position model);
        Result<Position> Update(Position model);
        Result<Position> Get(Guid ID);
        DataTable List(PositionListVM listVM);
    }
}

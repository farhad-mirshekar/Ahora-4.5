
using System;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core.Result;
using System.Collections.Generic;
using FM.Portal.Core.Common;
using FM.Portal.Core.Owin;

namespace FM.Portal.Domain
{
    public class PositionService : IPositionService
    {
        private readonly IPositionDataSource _dataSource;
        private readonly IRequestInfo _requestInfo;
        public PositionService(IPositionDataSource dataSource
                               , IRequestInfo requestInfo)
        {
            _dataSource = dataSource;
            _requestInfo = requestInfo;
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

        public Result<List<Position>> List(PositionListVM model)
        {
            var table = ConvertDataTableToList.BindList<Position>(_dataSource.List(model));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Position>>.Successful(data: table);
            return Result<List<Position>>.Failure();
        }

        public Result<List<Position>> ListByUser(PositionListVM model)
        {
            var table = ConvertDataTableToList.BindList<Position>(_dataSource.List(model));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Position>>.Successful(data: table);
            return Result<List<Position>>.Failure();
        }

        public Result<PositionDefaultVM> PositionDefault(Guid userID)
        => _dataSource.PositionDefault(userID);
    }
}

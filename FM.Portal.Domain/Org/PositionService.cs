using System;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core;
using System.Collections.Generic;
using FM.Portal.Core.Common;
using System.Linq;

namespace FM.Portal.Domain
{
    public class PositionService : IPositionService
    {
        private readonly IPositionDataSource _dataSource;
        private readonly IRoleService _roleService;
        public PositionService(IPositionDataSource dataSource
                               , IRoleService roleService)
        {
            _dataSource = dataSource;
            _roleService = roleService;
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
            var positionResult= _dataSource.Get(ID);
            if (!positionResult.Success)
                return Result<Position>.Failure(message: positionResult.Message);

            var rolesResult = _roleService.List(new RoleListVM() {PositionID = ID });
            if (!rolesResult.Success)
                return Result<Position>.Failure(message: rolesResult.Message);
            var roles = rolesResult.Data;

            positionResult.Data.Roles = roles;
            return positionResult;
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

        public Result<Position> GetDefaultPosition(Guid userID)
        {
            try
            {
                var positionsResult = List(new PositionListVM() {UserID = userID });
                if (!positionsResult.Success)
                    return Result<Position>.Failure(message: "دریافت اطلاعات با خطا مواجه شده است");
                var positions = positionsResult.Data.Where(x=>x.Enabled);
                if(positions == null || !positions.Any())
                    return Result<Position>.Failure(message: "شما مجوز دسترسی به سامانه را ندارید");

                var defaultPositions = positions.Where(p => p.Default == true).ToList();
                var defaultPosition = new Position();

                if (defaultPositions.Count == 1)
                    defaultPosition = defaultPositions.First();
                else
                    defaultPosition = defaultPositions.FirstOrDefault();

                var setDefaultResult = SetDefault(defaultPosition.ID);
                if (!setDefaultResult.Success)
                    return Result<Position>.Failure(message: "عملیات با شکست مواجه شده است");

                return Result<Position>.Successful(data: defaultPosition);
            }
            catch(Exception e) { throw; }
        }

        public Result SetDefault(Guid ID)
        => _dataSource.SetDefault(ID);
    }
}

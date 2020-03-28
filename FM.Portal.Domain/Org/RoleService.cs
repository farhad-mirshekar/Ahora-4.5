using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;
using FM.Portal.Core.Result;

namespace FM.Portal.Domain
{
    public class RoleService : IRoleService
    {
        private readonly IRoleDataSource _dataSource;

        public RoleService(IRoleDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result Delete(Guid ID)
        {
            return _dataSource.Delete(ID);
        }

        public Result<Role> Get(Guid id)
        {
            return _dataSource.Get(id);
        }

        public Result<Role> Add(Role model)
        {
            return _dataSource.Insert(model);
        }

        public Result<List<Role>> List()
        {
            var table = ConvertDataTableToList.BindList<Role>(_dataSource.List());
            if (table.Count > 0)
                return Result<List<Role>>.Successful(data: table);

            return Result<List<Role>>.Failure();
        }

        public Result<Role> Edit(Role model)
        {
            return _dataSource.Update(model);
        }
    }
}


using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;
using FM.Portal.Core.Result;

namespace FM.Portal.Domain
{
    public class CommandService : ICommandService
    {
        private readonly ICommandDataSource _dataSource;
        public CommandService(ICommandDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<Command> Get(Guid id)
        {
            return _dataSource.Get(id);
        }

        public Result<Command> Add(Command model)
        {
            return _dataSource.Insert(model);
        }

        public Result<List<Command>> List()
        {
            var table =ConvertDataTableToList.BindList<Command>(_dataSource.List());
            if (table.Count > 0)
                return Result<List<Command>>.Successful(data: table);
            return Result<List<Command>>.Failure();
        }

        public Result<List<Command>> ListByNode(string Node)
        {
            var table = ConvertDataTableToList.BindList<Command>(_dataSource.ListByNode(Node));
            if (table.Count > 0)
                return Result<List<Command>>.Successful(data: table);
            return Result<List<Command>>.Failure();
        }

        public Result<List<Command>> ListForRole(CommandListVM model)
        {
            var table = ConvertDataTableToList.BindList<Command>(_dataSource.ListForRole(model));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Command>>.Successful(data: table);
            return Result<List<Command>>.Failure();
        }

        public Result<Command> Edit(Command model)
        {
            return _dataSource.Update(model);
        }

        public Result<List<GetPermission>> GetPermission()
        {
            var table = ConvertDataTableToList.BindList<GetPermission>(_dataSource.GetPermission());
            if (table.Count > 0)
                return Result<List<GetPermission>>.Successful(data: table);
            return Result<List<GetPermission>>.Failure();
        }
    }
}

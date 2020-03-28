using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
    public interface ICommandDataSource : IDataSource
    {
        Result<Command> Insert(Command model);
        Result<Command> Update(Command model);
        Result Delete(Guid ID);
        DataTable List();
        DataTable ListByNode(string Node);
        Result<Command> Get(Guid id);
        DataTable ListForRole(CommandListVM model);
        DataTable GetPermission();
    }
}

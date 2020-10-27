using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
    public interface ICommandService : IService
    {
        Result<Command> Add(Command model);
        Result<Command> Edit(Command model);
        Result Delete(Guid ID);
        Result<List<Command>> List();
        Result<List<Command>> ListByNode(string Node);
        Result<Command> Get(Guid id);
        Result<List<Command>> ListForRole(CommandListVM model);
        Result<List<GetPermission>> GetPermission();
    }
}

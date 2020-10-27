using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
    public interface IRoleService : IService
    {
        Result<Role> Add(Role model);
        Result<Role> Edit(Role model);
        Result Delete(Guid ID);
        Result<List<Role>> List(RoleListVM listVM);
        Result<Role> Get(Guid id);
    }
}

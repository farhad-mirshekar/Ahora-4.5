using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IDepartmentService : IService
    {
        Result<Department> Add(Department model);
        Result<Department> Edit(Department model);
        Result<int> Delete(Guid ID);
        Result<List<Department>> List();
        Result<Department> Get(Guid ID);
        Result<List<Department>> ListByNode(string Node);
    }
}

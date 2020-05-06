using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IDepartmentDataSource:IDataSource
    {
        Result<Department> Insert(Department model);
        Result<Department> Update(Department model);
        Result<int> Delete(Guid ID);
        DataTable List();
        Result<Department> Get(Guid ID);
        DataTable ListByNode(string Node);
    }
}

using FM.Portal.Core.Model.Ptl;
using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource.Ptl
{
   public interface IPagesDataSource:IDataSource
    {
        Result<Pages> Insert(Pages model);
        Result<Pages> Update(Pages model);
        Result<Pages> Get(Guid ID);
        DataTable List();
        Result Delete(Guid ID);
    }
}

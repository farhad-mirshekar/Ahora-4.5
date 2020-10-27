using FM.Portal.Core.Model.Ptl;
using FM.Portal.Core;
using System;
using System.Data;

namespace FM.Portal.DataSource.Ptl
{
   public interface IPagesDataSource:IDataSource
    {
        Result<Pages> Insert(Pages model);
        Result<Pages> Update(Pages model);
        Result<Pages> Get(Guid ID);
        Result<Pages> Get(string TrackingCode);
        DataTable List(PagesListVM listVM);
        Result Delete(Guid ID);
    }
}

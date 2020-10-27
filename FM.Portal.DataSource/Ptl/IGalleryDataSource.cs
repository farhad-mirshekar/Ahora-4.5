using FM.Portal.Core.Model;
using FM.Portal.Core;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IGalleryDataSource:IDataSource
    {
        Result<Gallery> Insert(Gallery model);
        Result<Gallery> Update(Gallery model);
        Result<Gallery> Get(Guid ID);
        Result<Gallery> Get(string TrackingCode);
        DataTable List(GalleryListVM listVM);
        Result Delete(Guid ID);
    }
}

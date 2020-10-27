using FM.Portal.Core.Model;
using FM.Portal.Core;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IBannerDataSource: IDataSource
    {
        Result<Banner> Insert(Banner model);
        Result<Banner> Update(Banner model);
        Result<Banner> Get(Guid ID);
        DataTable List(BannerListVM listVM);
        Result Delete(Guid ID);
    }
}

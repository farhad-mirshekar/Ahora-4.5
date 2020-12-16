using FM.Portal.Core.Model.Ptl;
using FM.Portal.Core;
using System;
using System.Data;

namespace FM.Portal.DataSource.Ptl
{
    public interface ICategoryDataSource : IDataSource
    {
        Result<Category> Insert(Category model);
        Result<Category> Update(Category model);
        DataTable List();
        Result<Category> Get(Guid ID);
        Result Delete(Guid ID);
    }
}

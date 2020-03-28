using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface ICategoryDataSource : IDataSource
    {
        Result<Category> Insert(Category model);
        Result<Category> Update(Category model);
        DataTable List();
        Result<Category> Get(Guid ID);
        Result<Category> GetByParent(Guid ID);
    }
}

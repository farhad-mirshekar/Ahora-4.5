using FM.Portal.Core.Model.Ptl;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service.Ptl
{
   public interface ICategoryService : IService
    {
        Result<Category> Add(Category model);
        Result<Category> Edit(Category model);
        Result<List<Category>> List();
        Result<Category> Get(Guid ID);
        Result<List<GetCountCategoryVM>> GetCountCategory();
        Result Delete(Guid ID);
        
    }
}

using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IRelatedProductService:IService
    {
        Result<RelatedProduct> Add(RelatedProduct model);
        Result<RelatedProduct> Edit(RelatedProduct model);
        Result<List<RelatedProduct>> List(RelatedProductListVM listVM);
        Result<RelatedProduct> Get(Guid ID);
        Result.Result Delete(Guid ID);
    }
}

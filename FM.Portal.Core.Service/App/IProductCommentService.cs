using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IProductCommentService:IService
    {
        Result<ProductComment> Add(ProductComment model);
        Result<ProductComment> Edit(ProductComment model);
        Result<ProductComment> Get(Guid ID);
        Result<List<ProductComment>> List(ProductCommentListVM listVM);
        Result Delete(Guid ID);
        Result<ProductComment> Like(Guid ID, Guid UserID);
        Result<ProductComment> DisLike(Guid ID, Guid UserID);

        /// <summary>
        /// For Table ProductComment_User_Mapping
        /// </summary>
        /// <param name="commentMapUser"></param>
        /// <returns></returns>
        Result UserCanLike(ProductCommentMapUser commentMapUser);
    }
}

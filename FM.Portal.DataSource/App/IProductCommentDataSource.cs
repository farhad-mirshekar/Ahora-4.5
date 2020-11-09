using FM.Portal.Core;
using FM.Portal.Core.Model;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IProductCommentDataSource:IDataSource
    {
        Result<ProductComment> Insert(ProductComment model);
        Result<ProductComment> Update(ProductComment model);
        Result<ProductComment> Get(Guid ID);
        DataTable List(ProductCommentListVM listVM);
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

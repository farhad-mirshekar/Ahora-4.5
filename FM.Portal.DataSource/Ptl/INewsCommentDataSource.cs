using FM.Portal.Core;
using FM.Portal.Core.Model;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface INewsCommentDataSource:IDataSource
    {
        Result<NewsComment> Insert(NewsComment model);
        Result<NewsComment> Update(NewsComment model);
        Result<NewsComment> Get(Guid ID);
        DataTable List(NewsCommentListVM listVM);
        Result Delete(Guid ID);
        Result<NewsComment> Like(Guid ID, Guid UserID);
        Result<NewsComment> DisLike(Guid ID, Guid UserID);

        /// <summary>
        /// For Table NewsComment_User_Mapping
        /// </summary>
        /// <param name="commentMapUser"></param>
        /// <returns></returns>
        Result UserCanLike(NewsCommentMapUser commentMapUser);
    }
}

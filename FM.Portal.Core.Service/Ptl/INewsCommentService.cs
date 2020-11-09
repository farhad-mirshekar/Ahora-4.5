using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface INewsCommentService:IService
    {
        Result<NewsComment> Add(NewsComment model);
        Result<NewsComment> Edit(NewsComment model);
        Result<NewsComment> Get(Guid ID);
        Result<List<NewsComment>> List(NewsCommentListVM listVM);
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

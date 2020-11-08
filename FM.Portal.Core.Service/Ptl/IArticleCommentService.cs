using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IArticleCommentService:IService
    {
        Result<ArticleComment> Add(ArticleComment model);
        Result<ArticleComment> Edit(ArticleComment model);
        Result<ArticleComment> Get(Guid ID);
        Result<List<ArticleComment>> List(ArticleCommentListVM listVM);
        Result Delete(Guid ID);
        Result<ArticleComment> Like(Guid ID, Guid UserID);
        Result<ArticleComment> DisLike(Guid ID, Guid UserID);

        /// <summary>
        /// For Table ArticleComment_User_Mapping
        /// </summary>
        /// <param name="commentMapUser"></param>
        /// <returns></returns>
        Result UserCanLike(ArticleCommentMapUser commentMapUser);
    }
}

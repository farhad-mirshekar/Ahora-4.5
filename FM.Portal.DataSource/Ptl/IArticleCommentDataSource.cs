using FM.Portal.Core;
using FM.Portal.Core.Model;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IArticleCommentDataSource:IDataSource
    {
        Result<ArticleComment> Insert(ArticleComment model);
        Result<ArticleComment> Update(ArticleComment model);
        Result<ArticleComment> Get(Guid ID);
        DataTable List(ArticleCommentListVM listVM);
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

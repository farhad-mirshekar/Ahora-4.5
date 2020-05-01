using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface ICommentDataSource:IDataSource
    {
        Result<Comment> Insert(Comment model);
        Result<Comment> Update(Comment model);
        Result<Comment> Get(Guid ID);
        Result<Comment> CanUserComment(Guid DocumentID , Guid UserID);
        DataTable List(CommentForType commentForType);
        DataTable List(CommentListVM listVM);
        DataTable List(Guid ParentID,Guid DocumentID);
        Result<int> Like(Guid ID);
        Result<int> DisLike(Guid ID);
    }
}

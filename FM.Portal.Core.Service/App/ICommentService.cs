using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface ICommentService:IService
    {
        Result<Comment> Add(Comment model);
        Result<Comment> Edit(Comment model);
        Result<Comment> Get(Guid ID);
        Result<Comment> CanUserComment(Guid DocumentID , Guid UserID);
        Result<List<CommentForProductAdminListVM>> List(CommentForType commentForType);
        Result<List<Comment>> List(CommentListVM listVM);
        Result<List<Comment>> List(Guid ParentID , Guid DocumentID);
        Result<int> Like(Guid ID);
        Result<int> DisLike(Guid ID);
    }
}

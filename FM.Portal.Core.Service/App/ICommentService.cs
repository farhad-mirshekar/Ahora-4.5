﻿using FM.Portal.Core.Model;
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
        Result<List<Comment>> List(CommentListVM listVM);
        Result<int> Like(Guid ID);
        Result<int> DisLike(Guid ID);
    }
}

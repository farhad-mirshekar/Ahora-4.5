using System;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core.Owin;
using System.Collections.Generic;
using FM.Portal.Core.Common;
using System.Linq;

namespace FM.Portal.Domain
{
    public class CommentService : ICommentService
    {
        private readonly ICommentDataSource _dataSource;
        private readonly IRequestInfo _requestIfo;
        public CommentService(ICommentDataSource dataSource, IRequestInfo requestInfo)
        {
            _dataSource = dataSource;
            _requestIfo = requestInfo;
        }
        public Result<Comment> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<Comment> Add(Comment model)
        {
            if (model.Body == string.Empty)
                return Result<Comment>.Failure(message: "متن نظر را وارد نمایید");
            var userID = model.UserID;
            if (userID == Guid.Empty)
                userID = _requestIfo.UserId.Value;

            model.UserID = userID;
            return _dataSource.Insert(model);
        }

        public Result<Comment> Edit(Comment model)
        {
            if (model.Body == string.Empty)
                return Result<Comment>.Failure(message: "متن نظر را وارد نمایید");
            var userID = model.UserID;
            if (userID == Guid.Empty)
                userID = _requestIfo.UserId.Value;

            model.UserID = userID;
            return _dataSource.Update(model);
        }

        public Result<List<Comment>> List(CommentListVM listVM)
        {
            List<Comment> comment = new List<Comment>();
            var model = ConvertDataTableToList.BindList<Comment>(_dataSource.List(listVM));
            if (model.Count > 0)
            {
                for (int i = 0; i < model.Count; i++)
                {
                    comment.Add(new Comment { ID = model[i].ID, Body = model[i].Body, CreationDate = model[i].CreationDate, DisLikeCount = model[i].DisLikeCount, CommentType = model[i].CommentType, LikeCount = model[i].LikeCount, Children = List(model[i].ID, model[i].DocumentID).Data,ParentID=model[i].ParentID });
                }
                var list = comment.Where(x => x.ParentID == Guid.Empty).Select(x => x).ToList();
                return Result<List<Comment>>.Successful(data: list);
            }
            else
                return Result<List<Comment>>.Failure(data:comment);
        }

        public Result<List<Comment>> List(Guid ParentID, Guid DocumentID)
        {
            var model = ConvertDataTableToList.BindList<Comment>(_dataSource.List(ParentID, DocumentID));
            if (model.Count > 0)
            {
                for (int i = 0; i < model.Count; i++)
                {
                    var list= List(model[i].ID, model[i].DocumentID);
                    model[i].Children = list.Data;
                }
                return Result<List<Comment>>.Successful(data: model);
            }
            else
                return Result<List<Comment>>.Failure();
        }

        public Result<int> Like(Guid ID)
        => _dataSource.Like(ID);
        public Result<int> DisLike(Guid ID)
        => _dataSource.DisLike(ID);

        public Result<List<CommentForProductAdminListVM>> List(CommentForType commentForType)
        {
            var model = ConvertDataTableToList.BindList<CommentForProductAdminListVM>(_dataSource.List(commentForType));
            if (model.Count > 0 || model.Count == 0)
            {
                return Result<List<CommentForProductAdminListVM>>.Successful(data: model);
            }
            else
                return Result<List<CommentForProductAdminListVM>>.Failure();
        }

        public Result<Comment> CanUserComment(Guid DocumentID, Guid UserID)
        => _dataSource.CanUserComment(DocumentID, UserID);
    }
}

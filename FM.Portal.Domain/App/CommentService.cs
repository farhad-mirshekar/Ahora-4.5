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
            if (model.Count > 0 || model.Count == 0)
            {
                if(model.Count == 0)
                    return Result<List<Comment>>.Successful(data:model);

                if (listVM.ShowChildren)
                {
                    model.ForEach(x =>
                    {
                        var child = model.Where(c => c.ParentID == x.ID && c.CommentType == CommentType.تایید).ToList();
                        x.Children = child;
                    });
                    var list = model.Where(x => x.ParentID == Guid.Empty).Select(x => x).ToList();
                    return Result<List<Comment>>.Successful(data: list);
                }
                else
                    return Result<List<Comment>>.Successful(data: model);
            }
            else
                return Result<List<Comment>>.Failure(data: comment);
        }
        public Result<int> Like(Guid ID)
        => _dataSource.Like(ID);
        public Result<int> DisLike(Guid ID)
        => _dataSource.DisLike(ID);
        public Result<Comment> CanUserComment(Guid DocumentID, Guid UserID)
        => _dataSource.CanUserComment(DocumentID, UserID);
    }
}

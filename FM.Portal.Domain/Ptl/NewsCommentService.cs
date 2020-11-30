using FM.Portal.Core;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FM.Portal.Domain
{
    public class NewsCommentService : INewsCommentService
    {
        private readonly INewsCommentDataSource _dataSource;
        public NewsCommentService(INewsCommentDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<NewsComment> Add(NewsComment model)
        {
            var validateResult = ValidateModel(model);
            if (!validateResult.Success)
                return Result<NewsComment>.Failure(message: validateResult.Message);

            model.ID = Guid.NewGuid();
            return _dataSource.Insert(model);
        }

        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<NewsComment> DisLike(Guid ID, Guid UserID)
        => _dataSource.DisLike(ID, UserID);

        public Result<NewsComment> Edit(NewsComment model)
        {
            var validateResult = ValidateModel(model);
            if (!validateResult.Success)
                return Result<NewsComment>.Failure(message: validateResult.Message);

            return _dataSource.Update(model);
        }

        public Result<NewsComment> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<NewsComment> Like(Guid ID, Guid UserID)
        => _dataSource.Like(ID, UserID);

        public Result<List<NewsComment>> List(NewsCommentListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<NewsComment>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
            {
                if (table.Count > 0)
                {
                    if (listVM.ShowChildren)
                    {
                        table.ForEach(x =>
                        {
                            var child = table.Where(c => c.ParentID == x.ID && c.CommentType == CommentType.تایید).ToList();
                            x.Children = child;
                        });
                        var list = table.Where(x => x.ParentID == null).Select(x => x).ToList();
                        return Result<List<NewsComment>>.Successful(data: list);
                    }
                }
                return Result<List<NewsComment>>.Successful(data: table);
            }
            return Result<List<NewsComment>>.Failure();
        }

        public Result UserCanLike(NewsCommentMapUser commentMapUser)
        => _dataSource.UserCanLike(commentMapUser);

        private Result ValidateModel(NewsComment comment)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(comment.Body))
                errors.Add("متن نظر را مشخص نمایید");

            return Result.Successful();
        }
    }
}

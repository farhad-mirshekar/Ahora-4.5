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
    public class ProductCommentService : IProductCommentService
    {
        private readonly IProductCommentDataSource _dataSource;
        public ProductCommentService(IProductCommentDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<ProductComment> Add(ProductComment model)
        {
            model.ID = Guid.NewGuid();
            return _dataSource.Insert(model);
        }

        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<ProductComment> DisLike(Guid ID, Guid UserID)
        => _dataSource.DisLike(ID, UserID);

        public Result<ProductComment> Edit(ProductComment model)
        {
            return _dataSource.Update(model);
        }

        public Result<ProductComment> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<ProductComment> Like(Guid ID, Guid UserID)
        => _dataSource.Like(ID, UserID);

        public Result<List<ProductComment>> List(ProductCommentListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<ProductComment>(_dataSource.List(listVM));
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
                        return Result<List<ProductComment>>.Successful(data: list);
                    }
                }
                return Result<List<ProductComment>>.Successful(data: table);
            }
            return Result<List<ProductComment>>.Failure();
        }

        public Result UserCanLike(ProductCommentMapUser commentMapUser)
        => _dataSource.UserCanLike(commentMapUser);

        private Result ValidateModel(ProductComment comment)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(comment.Body))
                errors.Add("متن نظر را نشخص نمایید");

            return Result.Successful();
        }
    }
}

using FM.Portal.Core;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FM.Portal.Domain
{
    public class ArticleCommentService : IArticleCommentService
    {
        private readonly IArticleCommentDataSource _dataSource;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        public ArticleCommentService(IArticleCommentDataSource dataSource
                                    , ILocaleStringResourceService localeStringResourceService)
        {
            _dataSource = dataSource;
            _localeStringResourceService = localeStringResourceService;
        }
        public Result<ArticleComment> Add(ArticleComment model)
        {
            var validateResult = ValidateModel(model);
            if (!validateResult.Success)
                return Result<ArticleComment>.Failure(message: validateResult.Message);

            model.ID = Guid.NewGuid();
            return _dataSource.Insert(model);
        }

        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<ArticleComment> DisLike(Guid ID, Guid UserID)
        => _dataSource.DisLike(ID, UserID);

        public Result<ArticleComment> Edit(ArticleComment model)
        {
            var validateResult = ValidateModel(model);
            if (!validateResult.Success)
                return Result<ArticleComment>.Failure(message: validateResult.Message);

            return _dataSource.Update(model);
        }

        public Result<ArticleComment> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<ArticleComment> Like(Guid ID, Guid UserID)
        => _dataSource.Like(ID, UserID);

        public Result<List<ArticleComment>> List(ArticleCommentListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<ArticleComment>(_dataSource.List(listVM));
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
                        return Result<List<ArticleComment>>.Successful(data: list);
                    }
                }
                return Result<List<ArticleComment>>.Successful(data: table);
            }
            return Result<List<ArticleComment>>.Failure();
        }

        public Result UserCanLike(ArticleCommentMapUser commentMapUser)
        => _dataSource.UserCanLike(commentMapUser);

        private Result ValidateModel(ArticleComment comment)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(comment.Body))
            {
                var lngResult = _localeStringResourceService.GetResource("ArticleComment.Field.Body");
                var lng = lngResult.Data;

                errors.Add(lng);
            }

            if (errors.Any())
                return Result.Failure(message: string.Join("&&", errors));
            return Result.Successful();
        }
    }
}

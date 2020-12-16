using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;
using FM.Portal.Core.Extention.ReadingTime;
using System.Linq;
using FM.Portal.FrameWork.MVC.Helpers.Files;

namespace FM.Portal.Domain
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleDataSource _dataSource;
        private readonly ITagsService _tagsService;
        private readonly IActivityLogService _activityLogService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IAttachmentService _attachmentService;
        public ArticleService(IArticleDataSource dataSource
                             , ITagsService tagsService
                             , IActivityLogService activityLogService
                             , ILocaleStringResourceService localeStringResourceService
                             , IUrlRecordService urlRecordService
                             , IAttachmentService attachmentService)
        {
            _dataSource = dataSource;
            _tagsService = tagsService;
            _activityLogService = activityLogService;
            _localeStringResourceService = localeStringResourceService;
            _urlRecordService = urlRecordService;
            _attachmentService = attachmentService;
        }
        public Result<Article> Add(Article model)
        {
            var validateResult = ValidationModel(model);
            if (!validateResult.Success)
                return Result<Article>.Failure(message: validateResult.Message);

            model.ID = Guid.NewGuid();
            if (model.Tags != null && model.Tags.Count > 0)
            {
                var tags = new List<Tags>();
                foreach (var item in model.Tags)
                {
                    tags.Add(new Tags { Name = item, DocumentID = model.ID });
                }
                _tagsService.Add(tags);
            }
            model.ReadingTime = CalculateReadingTime.MinReadTime(model.Body);
            var result = _dataSource.Insert(model);
            if (result.Success)
            {
                _urlRecordService.Add(new UrlRecord()
                {
                    UrlDesc = model.UrlDesc,
                    EntityID = model.ID,
                    EntityName = model.GetType().Name,
                    Enabled = EnableMenuType.فعال
                });
            }
            return result;
        }

        public Result Delete(Guid ID)
        {
            //var attachmentsRsult = _attachmentService.List(ID);
            //if (!attachmentsRsult.Success)
            //    return Result.Failure();

            //var attachments = attachmentsRsult.Data;

            //if (attachments.Count > 0)
            //{
            //    _tagsService.Delete(ID);

            //    foreach (var item in attachments)
            //    {
            //        string path = $"{Enum.GetName(typeof(PathType), item.PathType)}/{item.FileName}";
            //        _attachmentService.Delete(item.ID);
            //        FileHelper.DeleteFile(path);
            //    }
            //}

            return _dataSource.Delete(ID);
        }

        public Result<Article> Edit(Article model)
        {
            var validateResult = ValidationModel(model);
            if (!validateResult.Success)
                return Result<Article>.Failure(message: validateResult.Message);

            if (model.Tags != null && model.Tags.Count > 0)
            {
                var tags = new List<Tags>();
                foreach (var item in model.Tags)
                {
                    tags.Add(new Tags { Name = item, DocumentID = model.ID });
                }
                _tagsService.Add(tags);
            }
            else
            {
                _tagsService.Delete(model.ID);
            }
            model.ReadingTime = CalculateReadingTime.MinReadTime(model.Body);
            var result = _dataSource.Update(model);
            _activityLogService.Add(new ActivityLog()
            {
                Comment = _localeStringResourceService.GetResource("ActivityLog.UpdateArticle").Data ?? "ویرایش مقاله",
                EntityID = model.ID,
                EntityName = model.GetType().Name,
                IpAddress = "12",
                SystemKeyword = "UpdateArticle"
            });
            if (result.Success)
            {
                var urlRecordResult = _urlRecordService.Get(null, model.ID);
                if (urlRecordResult.Success)
                {
                    urlRecordResult.Data.UrlDesc = model.UrlDesc;
                    _urlRecordService.Edit(urlRecordResult.Data);
                }
            }
            return result;
        }

        public Result<Article> Get(Guid ID)
        {
            var articleResult = _dataSource.Get(ID);
            if (!articleResult.Success)
                return Result<Article>.Failure(message: articleResult.Message);

            var article = articleResult.Data;
            if (article != null)
            {
                var resultTag = _tagsService.List(ID);
                if (resultTag.Success && resultTag.Data.Count > 0)
                {
                    List<string> tags = new List<string>();
                    foreach (var item in resultTag.Data)
                    {
                        tags.Add(item.Name);
                    }
                    article.Tags = tags;
                }

                return Result<Article>.Successful(data: article);
            }
            else
                return Result<Article>.Failure();
        }

        public Result<List<Article>> List(ArticleListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<Article>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Article>>.Successful(data: table);
            return Result<List<Article>>.Failure();
        }

        private Result ValidationModel(Article article)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(article.Body))
                errors.Add("متن اصلی مقاله الزامی می باشد");

            if (string.IsNullOrEmpty(article.Description))
                errors.Add("توضیحات کوتاه مقاله الزامی می باشد");

            if (string.IsNullOrEmpty(article.Title))
                errors.Add("عنوان مقاله الزامی می باشد");

            if (string.IsNullOrEmpty(article.UrlDesc))
                errors.Add("سئو مقاله الزامی می باشد");

            if (string.IsNullOrEmpty(article.MetaKeywords))
                errors.Add("متاتگ مقاله الزامی می باشد");

            if (article.CategoryID == Guid.Empty)
                errors.Add("انتخاب موضوع مقاله الزامی می باشد");

            if (article.LanguageID == null || article.LanguageID == Guid.Empty)
                errors.Add("انتخاب زبان مقاله الزامی می باشد");


            if (errors.Any())
                return Result.Failure(message: string.Join("&&", errors));

            return Result.Successful();
        }
    }
}

using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;
using FM.Portal.Core.Extention.ReadingTime;

namespace FM.Portal.Domain
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleDataSource _dataSource;
        private readonly ITagsService _tagsService;
        private readonly IActivityLogService _activityLogService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly IUrlRecordService _urlRecordService;
        public ArticleService(IArticleDataSource dataSource
                             , ITagsService tagsService
                             , IActivityLogService activityLogService
                             , ILocaleStringResourceService localeStringResourceService
                             , IUrlRecordService urlRecordService)
        {
            _dataSource = dataSource;
            _tagsService = tagsService;
            _activityLogService = activityLogService;
            _localeStringResourceService = localeStringResourceService;
            _urlRecordService = urlRecordService;
        }
        public Result<Article> Add(Article model)
        {
            model.ID = Guid.NewGuid();
            if(model.Tags != null && model.Tags.Count > 0)
            {
                var tags =new List<Tags>();
                foreach (var item in model.Tags)
                {
                    tags.Add(new Tags { Name = item });
                }
                _tagsService.Insert(tags,model.ID);
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

        public Result<int> Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<Article> Edit(Article model)
        {
            if (model.Tags.Count > 0)
            {
                var tags = new List<Tags>();
                foreach (var item in model.Tags)
                {
                    tags.Add(new Tags { Name = item });
                }
                _tagsService.Insert(tags,model.ID);
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
                SystemKeyword= "UpdateArticle"
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
            var article = _dataSource.Get(ID);
            if (article.Success)
            {
                var resultTag = _tagsService.List(ID);
                if (resultTag.Success)
                {
                    List<string> tags = new List<string>();
                    foreach (var item in resultTag.Data)
                    {
                        tags.Add(item.Name);
                    }
                    article.Data.Tags = tags;
                }
            }
            return article;
        }

        public Result<List<Article>> List(ArticleListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<Article>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Article>>.Successful(data: table);
            return Result<List<Article>>.Failure();
        }
    }
}

using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;
using System.Globalization;
using FM.Portal.Core.Extention.ReadingTime;

namespace FM.Portal.Domain
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleDataSource _dataSource;
        private readonly ITagsService _tagsService;
        private readonly IActivityLogService _activityLogService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        public ArticleService(IArticleDataSource dataSource
                             , ITagsService tagsService
                             , IActivityLogService activityLogService
                             , ILocaleStringResourceService localeStringResourceService)
        {
            _dataSource = dataSource;
            _tagsService = tagsService;
            _activityLogService = activityLogService;
            _localeStringResourceService = localeStringResourceService;
        }
        public Result<Article> Add(Article model)
        {
            var dt = DateTime.Now; 
            var pc = new PersianCalendar();
            string trackingCode = pc.GetYear(dt).ToString().Substring(2, 2) +
                                  pc.GetMonth(dt).ToString();
            model.TrackingCode = trackingCode;
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
            return _dataSource.Insert(model);
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

        public Result<Article> Get(string TrackingCode)
        {
            var article = _dataSource.Get(TrackingCode);
            if (article.Success)
            {
                var resultTag = _tagsService.List(article.Data.ID);
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

using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
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
        public ArticleService(IArticleDataSource dataSource
                             ,ITagsService tagsService)
        {
            _dataSource = dataSource;
            _tagsService = tagsService;
        }
        public Result<Article> Add(Article model)
        {
            var dt = DateTime.Now; 
            var pc = new PersianCalendar();
            string trackingCode = pc.GetYear(dt).ToString().Substring(2, 2) +
                                  pc.GetMonth(dt).ToString();
            model.TrackingCode = trackingCode;
            model.ID = Guid.NewGuid();
            if(model.Tags.Count > 0)
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
            return _dataSource.Update(model);
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

        public Result<List<Article>> List()
        {
            var table = ConvertDataTableToList.BindList<Article>(_dataSource.List());
            if (table.Count > 0)
                return Result<List<Article>>.Successful(data: table);
            return Result<List<Article>>.Failure();
        }

        public Result<List<ArticleListVM>> List(int count)
        {
            var table = ConvertDataTableToList.BindList<ArticleListVM>(_dataSource.List(count));
            if (table.Count > 0)
                return Result<List<ArticleListVM>>.Successful(data: table);
            return Result<List<ArticleListVM>>.Failure();
        }
    }
}

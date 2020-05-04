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
    public class NewsService : INewsService
    {
        private readonly INewsDataSource _dataSource;
        private readonly ITagsService _tagsService;
        public NewsService(INewsDataSource dataSource
                           , ITagsService tagsService)
        {
            _dataSource = dataSource;
            _tagsService = tagsService;
        }
        public Result<News> Add(News model)
        {
            var dt = DateTime.Now;
            var pc = new PersianCalendar();
            string trackingCode = pc.GetYear(dt).ToString().Substring(2, 2) +
                                  pc.GetMonth(dt).ToString()+
                                  pc.GetDayOfMonth(dt).ToString();
            model.TrackingCode = trackingCode;
            model.ID = Guid.NewGuid();
            if (model.Tags.Count > 0)
            {
                var tags = new List<Tags>();
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
        public Result<News> Edit(News model)
        {
            if (model.Tags.Count > 0)
            {
                var tags = new List<Tags>();
                foreach (var item in model.Tags)
                {
                    tags.Add(new Tags { Name = item });
                }
                _tagsService.Insert(tags, model.ID);
            }
            else
            {
                _tagsService.Delete(model.ID);
            }
            model.ReadingTime = CalculateReadingTime.MinReadTime(model.Body);
            return _dataSource.Update(model); 
        }

        public Result<News> Get(Guid ID)
        {
            var news = _dataSource.Get(ID);
            if (news.Success)
            {
                var resultTag = _tagsService.List(ID);
                if (resultTag.Success)
                {
                    List<string> tags = new List<string>();
                    foreach (var item in resultTag.Data)
                    {
                        tags.Add(item.Name);
                    }
                    news.Data.Tags = tags;
                }
            }
            return news;
        }

        public Result<News> Get(string TrackingCode)
        {
            var news = _dataSource.Get(TrackingCode);
            if (news.Success)
            {
                var resultTag = _tagsService.List(news.Data.ID);
                if (resultTag.Success)
                {
                    List<string> tags = new List<string>();
                    foreach (var item in resultTag.Data)
                    {
                        tags.Add(item.Name);
                    }
                    news.Data.Tags = tags;
                }
            }
            return news;
        }

        public Result<List<News>> List()
        {
            var table = ConvertDataTableToList.BindList<News>(_dataSource.List());
            if (table.Count > 0)
                return Result<List<News>>.Successful(data: table);
            return Result<List<News>>.Failure();
        }

        public Result<List<NewsListVM>> List(int count)
        {
            var table = ConvertDataTableToList.BindList<NewsListVM>(_dataSource.List(count));
            if (table.Count > 0)
                return Result<List<NewsListVM>>.Successful(data: table);
            return Result<List<NewsListVM>>.Failure();
        }
    }
}

using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;
using System.Globalization;

namespace FM.Portal.Domain
{
    public class NewsService : INewsService
    {
        private readonly INewsDataSource _dataSource;
        public NewsService(INewsDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<News> Add(News model)
        {
            var dt = DateTime.Now;
            var pc = new PersianCalendar();
            string trackingCode = pc.GetYear(dt).ToString().Substring(2, 2) +
                                  pc.GetMonth(dt).ToString()+
                                  pc.GetDayOfMonth(dt).ToString();
            model.TrackingCode = trackingCode;
            return _dataSource.Insert(model);
        }

        public Result<int> Delete(Guid ID)
       => _dataSource.Delete(ID);
        public Result<News> Edit(News model)
        => _dataSource.Update(model);

        public Result<News> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<News> Get(string TrackingCode)
        => _dataSource.Get(TrackingCode);

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

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
    public class ArticleService : IArticleService
    {
        private readonly IArticleDataSource _dataSource;
        public ArticleService(IArticleDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<Article> Add(Article model)
        {
            var dt = DateTime.Now; 
            var pc = new PersianCalendar();
            string trackingCode = pc.GetYear(dt).ToString().Substring(2, 2) +
                                  pc.GetMonth(dt).ToString();
            model.TrackingCode = trackingCode;
            return _dataSource.Insert(model);
        }

        public Result<int> Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<Article> Edit(Article model)
        => _dataSource.Update(model);

        public Result<Article> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<Article> Get(string TrackingCode)
        => _dataSource.Get(TrackingCode);

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

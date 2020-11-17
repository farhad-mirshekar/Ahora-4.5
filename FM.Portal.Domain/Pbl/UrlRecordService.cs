using FM.Portal.Core;
using FM.Portal.Core.Common;
using FM.Portal.Core.Extention.ReadingTime;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System;
using System.Collections.Generic;

namespace FM.Portal.Domain
{
    public class UrlRecordService : IUrlRecordService
    {
        private readonly IUrlRecordDataSource _dataSource;
        public UrlRecordService(IUrlRecordDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public Result<UrlRecord> Add(UrlRecord model)
        {
            model.ID = Guid.NewGuid();
            model.UrlDesc = CalculateWordsCount.CleanSeoUrl(model.UrlDesc);
            return _dataSource.Insert(model);
        }

        public Result<UrlRecord> Edit(UrlRecord model)
        {
            model.UrlDesc = CalculateWordsCount.CleanSeoUrl(model.UrlDesc);
            return _dataSource.Update(model);
        }

        public Result<UrlRecord> Get(string UrlDesc)
        => _dataSource.Get(UrlDesc);

        public Result<UrlRecord> Get(Guid? ID, Guid? EntityID)
        => _dataSource.Get(ID, EntityID);

        public Result<List<UrlRecord>> List(UrlRecordListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<UrlRecord>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<UrlRecord>>.Successful(data: table);
            return Result<List<UrlRecord>>.Failure();
        }
    }
}

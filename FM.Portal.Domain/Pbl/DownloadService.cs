using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace FM.Portal.Domain
{
    public class DownloadService : IDownloadService
    {
        private readonly IDownloadDataSource _dataSource;
        public DownloadService(IDownloadDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public Result<Download> Add(Download model)
        {
            model.ID = Guid.NewGuid();
            return _dataSource.Insert(model);
        }

        public Result<Download> Edit(Download model)
        => _dataSource.Update(model);

        public Result<Download> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<List<Download>> List(Guid PaymentID)
        {
            var table = ConvertDataTableToList.BindList<Download>(_dataSource.List(PaymentID));
            if (table.Count > 0)
                return Result<List<Download>>.Successful(data: table);
            return Result<List<Download>>.Failure();
        }
    }
}

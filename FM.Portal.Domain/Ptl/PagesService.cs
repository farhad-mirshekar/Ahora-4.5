using FM.Portal.Core.Common;
using FM.Portal.Core.Model.Ptl;
using FM.Portal.Core;
using FM.Portal.Core.Service.Ptl;
using FM.Portal.DataSource.Ptl;
using System;
using System.Collections.Generic;

namespace FM.Portal.Domain.Ptl
{
    public class PagesService : IPagesService
    {
        private readonly IPagesDataSource _dataSource;
        public PagesService(IPagesDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public Result<Pages> Add(Pages model)
        {
            model.ID = Guid.NewGuid();
            return _dataSource.Insert(model);
        }

        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<Pages> Edit(Pages model)
        => _dataSource.Update(model);

        public Result<Pages> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<Pages> Get(string TrackingCode)
        => _dataSource.Get(TrackingCode);

        public Result<List<Pages>> List(PagesListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<Pages>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Pages>>.Successful(data: table);
            return Result<List<Pages>>.Failure();
        }
    }
}

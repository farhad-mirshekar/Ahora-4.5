using System;
using System.Collections.Generic;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;

namespace FM.Portal.Domain
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
            if (model.Node == string.Empty || model.Node == "")
                model.Node = null;
           return _dataSource.Create(model);
        }

        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);
        public Result<Pages> Edit(Pages model)
        => _dataSource.Update(model);

        public Result<Pages> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<List<Pages>> List()
        {
            var result = ConvertDataTableToList.BindList<Pages>(_dataSource.List());
            if (result.Count > 0 || result.Count == 0)
                return Result<List<Pages>>.Successful(data: result);
            else
                return Result<List<Pages>>.Failure();
        }
    }
}

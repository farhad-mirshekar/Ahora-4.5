using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;

namespace FM.Portal.Domain
{
    public class ProductMapAttributeService : IProductMapAttributeService
    {
        private readonly IProductMapAttributeDataSource _dataSource;
        public ProductMapAttributeService(IProductMapAttributeDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<ProductMapAttribute> Add(ProductMapAttribute model)
        {
            return _dataSource.Insert(model);
        }

        public Result<ProductMapAttribute> Edit(ProductMapAttribute model)
        {
            return _dataSource.Update(model);
        }

        public Result<ProductMapAttribute> Get(Guid ID)
        {
            return _dataSource.Get(ID);
        }

        public Result<List<ListAttributeForProductVM>> List(Guid ProductID)
        {
            var table = ConvertDataTableToList.BindList<ListAttributeForProductVM>(_dataSource.List(ProductID));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<ListAttributeForProductVM>>.Successful(data: table);
            return Result<List<ListAttributeForProductVM>>.Failure();
        }
        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);

    }
}

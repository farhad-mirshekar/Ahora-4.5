using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;

namespace FM.Portal.Domain
{
    public class ProductAttributeService : IProductAttributeService
    {
        private readonly IProductAttributeDataSource _dataSource;
        public ProductAttributeService(IProductAttributeDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<ProductAttribute> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<ProductAttribute> Add(ProductAttribute model)
        => _dataSource.Insert(model);

        public Result<List<ProductAttribute>> List()
        {
            var table = ConvertDataTableToList.BindList<ProductAttribute>(_dataSource.List());
            if (table.Count > 0)
                return Result<List<ProductAttribute>>.Successful(data: table);
            return Result<List<ProductAttribute>>.Failure();
        }

        public Result<ProductAttribute> Edit(ProductAttribute model)
        => _dataSource.Update(model);
    }
}

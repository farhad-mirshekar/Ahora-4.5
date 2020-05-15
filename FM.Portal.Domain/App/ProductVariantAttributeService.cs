using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;

namespace FM.Portal.Domain
{
    public class ProductVariantAttributeService : IProductVariantAttributeService
    {
        private readonly IProductVariantAttributeDataSource _dataSource;
        public ProductVariantAttributeService(IProductVariantAttributeDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<ProductVariantAttribute> Add(ProductVariantAttribute model)
        {
            return _dataSource.Insert(model);
        }

        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<ProductVariantAttribute> Edit(ProductVariantAttribute model)
        {
            return _dataSource.Update(model);
        }

        public Result<ProductVariantAttribute> Get(Guid ID)
        {
            return _dataSource.Get(ID);
        }

        public Result<List<ProductVariantAttribute>> List(Guid ProductVariantAttributeID)
        {
            var table = ConvertDataTableToList.BindList<ProductVariantAttribute>(_dataSource.List(ProductVariantAttributeID));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<ProductVariantAttribute>>.Successful(data: table);
            return Result<List<ProductVariantAttribute>>.Failure();
        }
    }
}

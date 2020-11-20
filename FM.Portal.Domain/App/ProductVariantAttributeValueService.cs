using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;

namespace FM.Portal.Domain
{
    public class ProductVariantAttributeValueService : IProductVariantAttributeValueService
    {
        private readonly IProductVariantAttributeValueDataSource _dataSource;
        public ProductVariantAttributeValueService(IProductVariantAttributeValueDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<ProductVariantAttributeValue> Add(ProductVariantAttributeValue model)
        {
            model.ID = Guid.NewGuid();
            return _dataSource.Insert(model);
        }

        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<ProductVariantAttributeValue> Edit(ProductVariantAttributeValue model)
        {
            return _dataSource.Update(model);
        }

        public Result<ProductVariantAttributeValue> Get(Guid ID)
        {
            return _dataSource.Get(ID);
        }

        public Result<List<ProductVariantAttributeValue>> List(Guid ProductVariantAttributeID)
        {
            var table = ConvertDataTableToList.BindList<ProductVariantAttributeValue>(_dataSource.List(ProductVariantAttributeID));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<ProductVariantAttributeValue>>.Successful(data: table);
            return Result<List<ProductVariantAttributeValue>>.Failure();
        }
    }
}

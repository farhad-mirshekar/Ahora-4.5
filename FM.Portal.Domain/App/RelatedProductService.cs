using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System;
using System.Collections.Generic;

namespace FM.Portal.Domain
{
    public class RelatedProductService : IRelatedProductService
    {
        private readonly IRelatedProductDataSource _dataSource;
        public RelatedProductService(IRelatedProductDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public Result<RelatedProduct> Add(RelatedProduct model)
        {
            model.ID = Guid.NewGuid();
            return _dataSource.Insert(model);
        }

        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<RelatedProduct> Edit(RelatedProduct model)
        => _dataSource.Update(model);

        public Result<RelatedProduct> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<List<RelatedProduct>> List(RelatedProductListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<RelatedProduct>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<RelatedProduct>>.Successful(data: table);
            return Result<List<RelatedProduct>>.Failure();
        }
    }
}

using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;

namespace FM.Portal.Domain
{
    public class ShoppingCartItemService : IShoppingCartItemService
    {
        private readonly IShoppingCartItemDataSource _dataSource;
        public ShoppingCartItemService(IShoppingCartItemDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<List<ShoppingCartItem>> Add(ShoppingCartItem model)
        {
            var table = ConvertDataTableToList.BindList<ShoppingCartItem>(_dataSource.Insert(model));
            if (table.Count > 0)
                return Result<List<ShoppingCartItem>>.Successful(data: table);
            return Result<List<ShoppingCartItem>>.Failure();
        }

        public Result<List<ShoppingCartItem>> Delete(DeleteCartItemVM model)
        {
            var table = ConvertDataTableToList.BindList<ShoppingCartItem>(_dataSource.Delete(model));
            if (table.Count > 0)
                return Result<List<ShoppingCartItem>>.Successful(data: table);
            return Result<List<ShoppingCartItem>>.Failure();
        }

        public Result Delete(Guid ShoppingID)
        => _dataSource.Delete(ShoppingID);

        public Result<List<ShoppingCartItem>> Edit(ShoppingCartItem model)
        {
            var table = ConvertDataTableToList.BindList<ShoppingCartItem>(_dataSource.Update(model));
            if (table.Count > 0)
                return Result<List<ShoppingCartItem>>.Successful(data: table);
            return Result<List<ShoppingCartItem>>.Failure();
        }

        public Result<ShoppingCartItem> Get(Guid ShoppingID, Guid ProductID)
        => _dataSource.Get(ShoppingID, ProductID);

        public Result<List<ShoppingCartItem>> List(Guid ShoppingID)
        {
            var table = ConvertDataTableToList.BindList<ShoppingCartItem>(_dataSource.List(ShoppingID));
            if (table.Count > 0)
                return Result<List<ShoppingCartItem>>.Successful(data: table);
            return Result<List<ShoppingCartItem>>.Failure();
        }
    }
}

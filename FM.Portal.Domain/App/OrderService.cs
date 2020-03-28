using System;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;

namespace FM.Portal.Domain
{
    public class OrderService : IOrderService
    {
        private IOrderDataSource _dataSource;
        private IOrderDetailDataSource _detailDataSource;
        public OrderService(IOrderDataSource dataSource , IOrderDetailDataSource detaildataSource)
        {
            _dataSource = dataSource;
            _detailDataSource = detaildataSource;
        }

        public Result Add(Order model, OrderDetail detail)
        {
            model.ID = Guid.NewGuid();
            _dataSource.Insert(model);
            detail.ID = Guid.NewGuid();
            detail.OrderID = model.ID;
           return _detailDataSource.Insert(detail);
        }
        public Result<Order> Get(Guid ID)
        => _dataSource.Get(ID);
    }
}

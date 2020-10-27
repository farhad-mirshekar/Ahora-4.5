using System;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;

namespace FM.Portal.Domain
{
    public class OrderService : IOrderService
    {
        private IOrderDataSource _dataSource;
        private IOrderDetailDataSource _detailDataSource;
        public OrderService(IOrderDataSource dataSource, IOrderDetailDataSource detaildataSource)
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

        public Result<Order> Edit(Order model)
        {
            //model.TrackingCode += "-" + Helper.GetPersianYear(DateTime.Now).ToString() + Helper.GetPersianMonth(DateTime.Now).ToString() + Helper.GetPersianDay(DateTime.Now).ToString();
            return _dataSource.Update(model);
        }

        public Result<Order> Get(GetOrderVM model)
        => _dataSource.Get(model);
    }
}

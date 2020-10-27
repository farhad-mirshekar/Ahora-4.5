using FM.Portal.Core.Model;
using System;

namespace FM.Portal.Core.Service
{
    public interface IOrderService : IService
    {
        Result Add(Order model,OrderDetail detail);
        Result<Order> Get(GetOrderVM model);
        Result<Order> Edit(Order model);
    }
}

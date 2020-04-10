using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;

namespace FM.Portal.DataSource
{
   public interface IOrderDetailDataSource : IDataSource
    {
        Result Insert(OrderDetail model);
        Result<OrderDetail> Get(Guid ID);
    }
}

using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;

namespace FM.Portal.DataSource
{
   public interface IOrderDataSource:IDataSource
    {
        Result Insert(Order model);
        Result<Order> Get(Guid ID);
    }
}

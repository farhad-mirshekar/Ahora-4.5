using FM.Portal.Core.Model;
using FM.Portal.Core;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IShippingCostDataSource : IDataSource
    {
        Result<ShippingCost> Insert(ShippingCost model);
        Result<ShippingCost> Update(ShippingCost model);
        DataTable List(ShippingCostListVM listVM);
        Result<ShippingCost> Get(Guid ID);
        Result Delete(Guid ID);
    }
}

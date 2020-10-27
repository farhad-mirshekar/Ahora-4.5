using FM.Portal.Core.Model;
using FM.Portal.Core;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IDeliveryDateDataSource:IDataSource
    {
        Result<DeliveryDate> Insert(DeliveryDate model);
        Result<DeliveryDate> Update(DeliveryDate model);
        DataTable List(DeliveryDateListVM listVM);
        Result<DeliveryDate> Get(Guid ID);
        Result Delete(Guid ID);
    }
}

using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IDeliveryDateService:IService
    {
        Result<DeliveryDate> Add(DeliveryDate model);
        Result<DeliveryDate> Edit(DeliveryDate model);
        Result<List<DeliveryDate>> List(DeliveryDateListVM listVM);
        Result<DeliveryDate> Get(Guid ID);
        Result Delete(Guid ID);
    }
}

using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface ISalesService:IService
    {
        Result<Sales> Add(Sales model);
        Result<Sales> Get(Guid? ID , Guid? PaymentID);
        Result<Sales> Edit(Sales model);
        Result<List<Sales>> List(SalesListVM listVM);
        Result.Result Confirm(FlowConfirmVM confirmVM);
        Result<List<SalesFlow>> ListFlow(Guid ID);
    }
}

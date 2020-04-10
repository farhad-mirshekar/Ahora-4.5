using FM.Bank.Core.Model;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IPaymentService : IService
    {
        Result<Payment> Update(Payment model);
        Result<Payment> Get(Guid ID);
        Result<PaymentDetailVM> GetDetail(Guid ID);
        Result.Result FirstStepPayment(Core.Model.Order order, Core.Model.OrderDetail detail, Core.Model.Payment payment);
        Result<List<PaymentListVM>> List(ResCode resCode);
    }
}

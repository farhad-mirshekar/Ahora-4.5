using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;

namespace FM.Portal.Core.Service
{
   public interface IPaymentService : IService
    {
        Result<Payment> Update(Payment model);
        Result<Payment> Get(Guid ID, Guid OrderID, Guid UserID);
        Result.Result FirstStepPayment(Core.Model.Order order, Core.Model.OrderDetail detail, Core.Model.Payment payment);
    }
}

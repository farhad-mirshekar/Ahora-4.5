using FM.Bank.Core.Model;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IPaymentService : IService
    {
        Result<Payment> Edit(Payment model);
        Result<Payment> Get(Guid ID);
        Result<Payment> GetByShoppingID(Guid ShoppingID);
        Result<Payment> GetByToken(string Token , BankName BankName);
        Result<PaymentDetailVM> GetDetail(Guid ID);
        Result.Result FirstStepPayment(Core.Model.Order order, Core.Model.OrderDetail detail, Core.Model.Payment payment);
        Result<List<PaymentListVM>> List();
        // user dashboard
        Result<List<PaymentListForUserVM>> ListPaymentForUser(Guid UserID);
    }
}

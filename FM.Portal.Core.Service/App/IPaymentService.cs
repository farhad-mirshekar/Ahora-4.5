using FM.Portal.Core.Model;
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
        Result FirstStepPayment(Core.Model.Order order, Core.Model.OrderDetail detail, Core.Model.Payment payment);
        Result<List<PaymentListVM>> List();
        Result<byte[]> GetExcel();
        // user dashboard
        Result<List<Payment>> ListPaymentForUser(PaymentListForUserVM listVm);
    }
}

﻿using FM.Portal.Core.Model;
using FM.Portal.Core;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IPaymentDataSource : IDataSource
    {
        Result FirstStepPayment(Core.Model.Order order, Core.Model.OrderDetail detail, Core.Model.Payment payment);
        Result<Core.Model.Payment> Update(Core.Model.Payment model);
        Result<Core.Model.Payment> Get(Guid ID);
        Result<Payment> GetByShoppingID(Guid ShoppingID);
        Result<Payment> GetByToken(string Token , BankName bankName);
        DataTable List();
        //for user
        DataTable ListPaymentForUser(PaymentListForUserVM listVm);

    }
}

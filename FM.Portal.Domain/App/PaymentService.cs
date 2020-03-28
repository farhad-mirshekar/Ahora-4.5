using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;
using FM.Portal.Core.Result;

namespace FM.Portal.Domain
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentDataSource _dataSource;
        public PaymentService(IPaymentDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result FirstStepPayment(Order order, OrderDetail detail, Payment payment)
        => _dataSource.FirstStepPayment(order, detail, payment);

        public Result<Payment> Get(Guid ID, Guid OrderID, Guid UserID)
        {
            throw new NotImplementedException();
        }

        public Result<Payment> Update(Payment model)
        {
            throw new NotImplementedException();
        }
    }
}

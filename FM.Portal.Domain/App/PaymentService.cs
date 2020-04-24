﻿using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;
using FM.Portal.Core.Result;
using Newtonsoft.Json;

namespace FM.Portal.Domain
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentDataSource _dataSource;
        private readonly IOrderDetailDataSource _orderDetailDataSource;
        private readonly IOrderDataSource _orderDataSource;
        private readonly IUserAddressDataSource _userAddressDataSource;
        public PaymentService(IPaymentDataSource dataSource
                             , IOrderDetailDataSource orderDetailDataSource
                             , IUserAddressDataSource userAddressDataSource
                             , IOrderDataSource orderDataSource)
        {
            _dataSource = dataSource;
            _orderDetailDataSource = orderDetailDataSource;
            _userAddressDataSource = userAddressDataSource;
            _orderDataSource = orderDataSource;
        }
        public Result FirstStepPayment(Order order, OrderDetail detail, Payment payment)
        => _dataSource.FirstStepPayment(order, detail, payment);

        public Result<Payment> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<Payment> GetByShoppingID(Guid ShoppingID)
        => _dataSource.GetByShoppingID(ShoppingID);

        public Result<PaymentDetailVM> GetDetail(Guid ID)
        {
            var paymentResult = Get(ID);
            if (!paymentResult.Success)
                return Result<PaymentDetailVM>.Failure();
            var payment = paymentResult.Data;

            var orderDetailResult = _orderDetailDataSource.Get(payment.OrderID);
            if (!orderDetailResult.Success)
                return Result<PaymentDetailVM>.Failure();
            var orderDetail = orderDetailResult.Data;

            var orderResult = _orderDataSource.Get(new GetOrderVM { ID = orderDetail.OrderID });
            if(!orderResult.Success)
                return Result<PaymentDetailVM>.Failure();
            var order = orderResult.Data;
            var userAddressResult = _userAddressDataSource.Get(order.AddressID);
            var obj = new PaymentDetailVM
            {
                Attributes=JsonConvert.DeserializeObject<List<AttributeJsonVM>>(orderDetail.AttributeJson),
                ID=payment.ID,
                CreationDate=payment.CreationDate,
                Payment=payment,
                Products=JsonConvert.DeserializeObject<List<Product>>(orderDetail.ProductJson),
                User = JsonConvert.DeserializeObject<User>(orderDetail.UserJson),
                UserAddress = userAddressResult.Data
            };
            return Result<PaymentDetailVM>.Successful(data:obj);
        }

        public Result<List<PaymentListVM>> List()
        { 
           var table = ConvertDataTableToList.BindList<PaymentListVM>(_dataSource.List());
            if (table.Count > 0 || table.Count == 0)
                return Result<List<PaymentListVM>>.Successful(data: table);
            return Result<List<PaymentListVM>>.Failure();
        }

        public Result<Payment> Edit(Payment model)
        => _dataSource.Update(model);

        public Result<List<PaymentListForUserVM>> ListPaymentForUser(Guid UserID)
        {
            var table = ConvertDataTableToList.BindList<PaymentListForUserVM>(_dataSource.ListPaymentForUser(UserID));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<PaymentListForUserVM>>.Successful(data: table);
            return Result<List<PaymentListForUserVM>>.Failure();
        }

        public Result<Payment> GetByToken(string Token, BankName BankName)
        => _dataSource.GetByToken(Token.Trim(), BankName);
    }
}

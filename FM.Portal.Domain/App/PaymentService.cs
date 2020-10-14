using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;
using FM.Portal.Core.Result;
using Newtonsoft.Json;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;

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

        public Result<List<Payment>> ListPaymentForUser(PaymentListForUserVM listVm)
        {
            var table = ConvertDataTableToList.BindList<Payment>(_dataSource.ListPaymentForUser(listVm));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Payment>>.Successful(data: table);
            return Result<List<Payment>>.Failure();
        }

        public Result<Payment> GetByToken(string Token, BankName BankName)
        => _dataSource.GetByToken(Token.Trim(), BankName);

        public Result<byte[]> GetExcel()
        {
            try
            {
                var result = List();
                if (!result.Success)
                    return Result<byte[]>.Failure(message: "دریافت اکسل لیست فروش با خطا مواجه شده است.");

                result.Data = result.Data.OrderBy(x => x.CreationDate).ToList();

                byte[] bytes = null;
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    ExcelWorksheet sheet = excelPackage.Workbook.Worksheets.Add("sheet1");
                    sheet.View.RightToLeft = true;
                    var rowIndex = 1;
                    var colIndex = 1;

                    sheet.Cells[rowIndex, colIndex++].Value = "اطلاعات کاربر";
                    sheet.Cells[rowIndex, colIndex++].Value = "شماره همراه";
                    sheet.Cells[rowIndex, colIndex++].Value = "تعداد خرید";
                    sheet.Cells[rowIndex, colIndex++].Value = "مبلغ خرید (تومان)";
                    sheet.Cells[rowIndex, colIndex++].Value = "نام درگاه پرداخت";
                    sheet.Cells[rowIndex, colIndex++].Value = "تاریخ خرید";

                    foreach (var item in result.Data)
                    {
                        colIndex = 1;
                        rowIndex++;
                        sheet.Cells[rowIndex, colIndex++].Value = item.BuyerInfo;
                        sheet.Cells[rowIndex, colIndex++].Value = item.BuyerPhone;
                        sheet.Cells[rowIndex, colIndex++].Value = item.CountBuy;
                        sheet.Cells[rowIndex, colIndex++].Value = item.Price;
                        sheet.Cells[rowIndex, colIndex++].Value = item.BankNameString;
                        sheet.Cells[rowIndex, colIndex++].Value = item.CreationDatePersian;

                    }
                    using (ExcelRange range = sheet.Cells)
                    {
                        range.Style.Font.SetFromFont(new System.Drawing.Font("Tahoma", 9));
                        range.Style.Font.Bold = true;
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        range.Style.ReadingOrder = ExcelReadingOrder.RightToLeft;
                    }

                    const double minWidth = 0.00;
                    const double maxWidth = 50.00;
                    sheet.Cells.AutoFitColumns(minWidth, maxWidth);

                    bytes = excelPackage.GetAsByteArray();
                }
                return Result<byte[]>.Successful(data: bytes);
            }
            catch(Exception e) { throw; }
        }
    }
}

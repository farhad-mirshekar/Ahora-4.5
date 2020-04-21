using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using Payment.Bank.Melli;
using Payment.Bank.Melli.Model;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace Ahora.WebApp.Controllers
{
    public class RedirectController : BaseController<IPaymentService>
    {
        private readonly IOrderService _orderService;
        public RedirectController(IPaymentService service
                                 , IOrderService orderService) : base(service)
        {
            _orderService = orderService;
        }

        // GET: Redirect
        public async Task<ActionResult> Melli(PurchaseResult purchaseResult)
        {
            var _bankMelli = new BankMelli();
            var melliTemp = (string)TempData["melli"];
            var paymentVerifyResult = await _bankMelli.PaymentVerify(purchaseResult.Token, melliTemp, purchaseResult);
            var shoppingID = HttpContext.Request.Cookies.Get("ShoppingID").Value;
            var paymentResult = _service.GetByShoppingID(SQLHelper.CheckGuidNull(shoppingID));
            var payment = paymentResult.Data;
            var ResCode = (ResCodeVerify)SQLHelper.CheckIntNull(paymentVerifyResult.ResCode);
            switch (ResCode)
            {
                case ResCodeVerify.نتیجه_تراکنش_موفق_است:
                    {
                        payment.RetrivalRefNo = paymentVerifyResult.RetrivalRefNo;
                        payment.SystemTraceNo = paymentVerifyResult.SystemTraceNo;
                        payment.TransactionStatus = (int)ResCodeVerify.نتیجه_تراکنش_موفق_است;
                        payment.TransactionStatusMessage = ResCodeVerify.نتیجه_تراکنش_موفق_است.ToString();
                        _service.Edit(payment);
                        var order = _orderService.Get(new GetOrderVM { ShoppingID = SQLHelper.CheckGuidNull(shoppingID) });
                        order.Data.TrackingCode = paymentVerifyResult.OrderId;
                        _orderService.Edit(order.Data);
                        break;
                    }
                case ResCodeVerify.مهلت_ارسال_تراکنش_به_پایان_رسیده_است:
                    {
                        payment.TransactionStatus = (int)ResCodeVerify.مهلت_ارسال_تراکنش_به_پایان_رسیده_است;
                        payment.TransactionStatusMessage = ResCodeVerify.مهلت_ارسال_تراکنش_به_پایان_رسیده_است.ToString();
                        _service.Edit(payment);
                        break;
                    }
                case ResCodeVerify.پارامترهای_ارسالی_صحیح_نیست_و_یا_تراکنش_در_سیستم_وجود_ندارد:
                    {
                        payment.TransactionStatus = (int)ResCodeVerify.پارامترهای_ارسالی_صحیح_نیست_و_یا_تراکنش_در_سیستم_وجود_ندارد;
                        payment.TransactionStatusMessage = ResCodeVerify.پارامترهای_ارسالی_صحیح_نیست_و_یا_تراکنش_در_سیستم_وجود_ندارد.ToString();
                        _service.Edit(payment);
                        break;
                    }
                default:
                    {
                        payment.TransactionStatusMessage = paymentVerifyResult.Description;
                        _service.Edit(payment);
                        break;
                    }
            }
            return View();
        }
    }
}
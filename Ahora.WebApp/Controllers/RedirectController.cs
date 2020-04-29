using FM.Payment.Bank.Melli;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace Ahora.WebApp.Controllers
{
    public class RedirectController : BaseController<IPaymentService>
    {
        private readonly IOrderService _orderService;
        private readonly IShoppingCartItemService _shoppingCartItemService;
        public RedirectController(IPaymentService service
                                 , IOrderService orderService
                                 , IShoppingCartItemService shoppingCartItemService) : base(service)
        {
            _orderService = orderService;
            _shoppingCartItemService = shoppingCartItemService;
        }

        // GET: Redirect
        public async Task<ActionResult> Melli(PurchaseResult purchaseResult)
        {
            try
            {
                var _bankMelli = new BankMelli();
                var melliTemp = Request.Cookies["Data"].Value;
                var paymentVerifyResult = await _bankMelli.PaymentVerify(purchaseResult.Token, melliTemp, purchaseResult);
                var paymentResult = _service.GetByToken(SQLHelper.CheckStringNull(purchaseResult.Token), BankName.بانک_ملی);
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
                            var order = _orderService.Get(new GetOrderVM { ID = SQLHelper.CheckGuidNull(payment.OrderID) });
                            order.Data.TrackingCode = paymentVerifyResult.OrderId;
                            _orderService.Edit(order.Data);
                            break;
                        }
                    case ResCodeVerify.مهلت_ارسال_تراکنش_به_پایان_رسیده_است:
                        {
                            payment.TransactionStatus = (int)ResCodeVerify.مهلت_ارسال_تراکنش_به_پایان_رسیده_است;
                            payment.TransactionStatusMessage = ResCodeVerify.مهلت_ارسال_تراکنش_به_پایان_رسیده_است.ToString();
                            break;
                        }
                    case ResCodeVerify.پارامترهای_ارسالی_صحیح_نیست_و_یا_تراکنش_در_سیستم_وجود_ندارد:
                        {
                            payment.TransactionStatus = (int)ResCodeVerify.پارامترهای_ارسالی_صحیح_نیست_و_یا_تراکنش_در_سیستم_وجود_ندارد;
                            payment.TransactionStatusMessage = ResCodeVerify.پارامترهای_ارسالی_صحیح_نیست_و_یا_تراکنش_در_سیستم_وجود_ندارد.ToString();
                            break;
                        }
                    case ResCodeVerify.آدرس_بازگشت_پذیرنده_نامعتبر_است:
                        {
                            payment.TransactionStatus = (int)ResCodeVerify.آدرس_بازگشت_پذیرنده_نامعتبر_است;
                            payment.TransactionStatusMessage = ResCodeVerify.آدرس_بازگشت_پذیرنده_نامعتبر_است.ToString();
                            break;
                        }
                    case ResCodeVerify.آی_پی_پذیرنده_اشتباه_است:
                        {
                            payment.TransactionStatus = (int)ResCodeVerify.آی_پی_پذیرنده_اشتباه_است;
                            payment.TransactionStatusMessage = ResCodeVerify.آی_پی_پذیرنده_اشتباه_است.ToString();
                            break;
                        }
                    case ResCodeVerify.اشکال_در_تاریخ_و_زمان_سیستم:
                        {
                            payment.TransactionStatus = (int)ResCodeVerify.اشکال_در_تاریخ_و_زمان_سیستم;
                            payment.TransactionStatusMessage = ResCodeVerify.اشکال_در_تاریخ_و_زمان_سیستم.ToString();
                            break;
                        }
                    case ResCodeVerify.امکان_پرداخت_از_طریق_سیستم_شتاب_برای_این_پذیرنده_امکان_پذیر_نیست:
                        {
                            payment.TransactionStatus = (int)ResCodeVerify.امکان_پرداخت_از_طریق_سیستم_شتاب_برای_این_پذیرنده_امکان_پذیر_نیست;
                            payment.TransactionStatusMessage = ResCodeVerify.امکان_پرداخت_از_طریق_سیستم_شتاب_برای_این_پذیرنده_امکان_پذیر_نیست.ToString();
                            break;
                        }
                    case ResCodeVerify.انجام_تراکنش_مربوطه_توسط_پایانه_ی_انجام_دهنده_مجاز_نمیباشد:
                        {
                            payment.TransactionStatus = (int)ResCodeVerify.انجام_تراکنش_مربوطه_توسط_پایانه_ی_انجام_دهنده_مجاز_نمیباشد;
                            payment.TransactionStatusMessage = ResCodeVerify.انجام_تراکنش_مربوطه_توسط_پایانه_ی_انجام_دهنده_مجاز_نمیباشد.ToString();
                            break;
                        }
                    case ResCodeVerify.ترتیب_پارامترهای_ارسالی_اشتباه_است:
                        {
                            payment.TransactionStatus = (int)ResCodeVerify.ترتیب_پارامترهای_ارسالی_اشتباه_است;
                            payment.TransactionStatusMessage = ResCodeVerify.ترتیب_پارامترهای_ارسالی_اشتباه_است.ToString();
                            break;
                        }
                    case ResCodeVerify.توکن_ارسالی_نامعتبر_است:
                        {
                            payment.TransactionStatus = (int)ResCodeVerify.توکن_ارسالی_نامعتبر_است;
                            payment.TransactionStatusMessage = ResCodeVerify.توکن_ارسالی_نامعتبر_است.ToString();
                            break;
                        }
                    case ResCodeVerify.خطا_در_سیستم:
                        {
                            payment.TransactionStatus = (int)ResCodeVerify.خطا_در_سیستم;
                            payment.TransactionStatusMessage = ResCodeVerify.خطا_در_سیستم.ToString();
                            break;
                        }
                    case ResCodeVerify.خطا_در_سیستم_تراکنش_ناموفق:
                        {
                            payment.TransactionStatus = (int)ResCodeVerify.خطا_در_سیستم_تراکنش_ناموفق;
                            payment.TransactionStatusMessage = ResCodeVerify.خطا_در_سیستم_تراکنش_ناموفق.ToString();
                            break;
                        }
                    case ResCodeVerify.خطای_دسترسی:
                        {
                            payment.TransactionStatus = (int)ResCodeVerify.خطای_دسترسی;
                            payment.TransactionStatusMessage = ResCodeVerify.خطای_دسترسی.ToString();
                            break;
                        }
                    case ResCodeVerify.درخواست_تکراری_شماره_سفارش_تکراری_میباشد:
                        {
                            payment.TransactionStatus = (int)ResCodeVerify.درخواست_تکراری_شماره_سفارش_تکراری_میباشد;
                            payment.TransactionStatusMessage = ResCodeVerify.درخواست_تکراری_شماره_سفارش_تکراری_میباشد.ToString();
                            break;
                        }
                    case ResCodeVerify.شماره_سفارش_تراکنش_نامعتبر_است:
                        {
                            payment.TransactionStatus = (int)ResCodeVerify.شماره_سفارش_تراکنش_نامعتبر_است;
                            payment.TransactionStatusMessage = ResCodeVerify.شماره_سفارش_تراکنش_نامعتبر_است.ToString();
                            break;
                        }
                    case ResCodeVerify.مبلغ_تراکنش_از_حد_مجاز_بالاتر_است:
                        {
                            payment.TransactionStatus = (int)ResCodeVerify.مبلغ_تراکنش_از_حد_مجاز_بالاتر_است;
                            payment.TransactionStatusMessage = ResCodeVerify.مبلغ_تراکنش_از_حد_مجاز_بالاتر_است.ToString();
                            break;
                        }
                    case ResCodeVerify.مبلغ_تراکنش_نامعتبر_است:
                        {
                            payment.TransactionStatus = (int)ResCodeVerify.مبلغ_تراکنش_نامعتبر_است;
                            payment.TransactionStatusMessage = ResCodeVerify.مبلغ_تراکنش_نامعتبر_است.ToString();
                            break;
                        }
                    case ResCodeVerify.پذیرنده_غیر_فعال_شده_است:
                        {
                            payment.TransactionStatus = (int)ResCodeVerify.پذیرنده_غیر_فعال_شده_است;
                            payment.TransactionStatusMessage = ResCodeVerify.پذیرنده_غیر_فعال_شده_است.ToString();
                            break;
                        }
                    case ResCodeVerify.پذیرنده_کارت_فعال_نیست:
                        {
                            payment.TransactionStatus = (int)ResCodeVerify.پذیرنده_کارت_فعال_نیست;
                            payment.TransactionStatusMessage = ResCodeVerify.پذیرنده_کارت_فعال_نیست.ToString();
                            break;
                        }
                    case ResCodeVerify.پذیرنده_کارت_نامعتبر_است:
                        {
                            payment.TransactionStatus = (int)ResCodeVerify.پذیرنده_کارت_نامعتبر_است;
                            payment.TransactionStatusMessage = ResCodeVerify.پذیرنده_کارت_نامعتبر_است.ToString();
                            break;
                        }
                    default:
                        {
                            payment.TransactionStatusMessage = paymentVerifyResult.Description;
                            payment.TransactionStatus = -100;
                            _service.Edit(payment);
                            break;
                        }
                }
                _service.Edit(payment);

                var result = _service.GetDetail(SQLHelper.CheckGuidNull(payment.ID));
                ClearCookie("ShoppingID");
                //ClearCookie("Data");
                return View(paymentVerifyResult);
            }
            catch (Exception e) { return View("Error"); }
        }
        private void ClearCookie(string param)
        {
            try
            {
                var shoppingID = HttpContext.Request.Cookies.Get("ShoppingID").Value;
                _shoppingCartItemService.Delete(SQLHelper.CheckGuidNull(shoppingID));
                if (HttpContext.Request.Cookies[param] != null)
                {
                    var myCookie = new HttpCookie("ShoppingID", null);
                    myCookie.Expires = DateTime.Now.AddYears(-1);
                    HttpContext.Response.Cookies.Add(myCookie);
                }
            }
            catch (Exception e)
            {
                if (HttpContext.Request.Cookies[param] != null)
                {
                    var myCookie = new HttpCookie("ShoppingID", null);
                    myCookie.Expires = DateTime.Now.AddYears(-1);
                    HttpContext.Response.Cookies.Add(myCookie);
                }
            }

        }
    }
}
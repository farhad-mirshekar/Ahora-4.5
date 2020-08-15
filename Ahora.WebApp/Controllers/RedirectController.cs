using FM.Payment.Bank.Melli;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace Ahora.WebApp.Controllers
{
    public class RedirectController : BaseController<IPaymentService>
    {
        private readonly IOrderService _orderService;
        private readonly IShoppingCartItemService _shoppingCartItemService;
        private readonly IDownloadService _downloadService;
        private readonly IAttachmentService _attachmentService;
        private readonly IProductService _productService;
        public RedirectController(IPaymentService service
                                 , IOrderService orderService
                                 , IShoppingCartItemService shoppingCartItemService
                                 , IDownloadService downloadService
                                 , IAttachmentService attachmentService
                                 , IProductService productService) : base(service)
        {
            _orderService = orderService;
            _shoppingCartItemService = shoppingCartItemService;
            _downloadService = downloadService;
            _attachmentService = attachmentService;
            _productService = productService;
        }

        // GET: Redirect
        public async Task<ActionResult> Melli(PurchaseResult purchaseResult)
        {
            try
            {
                AddBaseDocument(SQLHelper.CheckGuidNull("D9E242B7-4140-4E08-8450-167FFAF7F7F9"));
                return null;
                //var paymentResult = _service.GetByToken(SQLHelper.CheckStringNull(purchaseResult.Token), BankName.بانک_ملی);
                //var payment = paymentResult.Data;
                //if (string.IsNullOrEmpty(payment.RetrivalRefNo) && string.IsNullOrEmpty(payment.SystemTraceNo))
                //{
                //    var _bankMelli = new BankMelli();
                //    var melliTemp = Request.Cookies["Data"].Value;
                //    var paymentVerifyResult = await _bankMelli.PaymentVerify(purchaseResult.Token, melliTemp, purchaseResult);
                //    var ResCode = (ResCodeVerify)SQLHelper.CheckIntNull(paymentVerifyResult.ResCode);
                //    switch (ResCode)
                //    {
                //        case ResCodeVerify.نتیجه_تراکنش_موفق_است:
                //            {
                //                payment.RetrivalRefNo = paymentVerifyResult.RetrivalRefNo;
                //                payment.SystemTraceNo = paymentVerifyResult.SystemTraceNo;
                //                payment.TransactionStatus = (int)ResCodeVerify.نتیجه_تراکنش_موفق_است;
                //                payment.TransactionStatusMessage = ResCodeVerify.نتیجه_تراکنش_موفق_است.ToString();
                //                var order = _orderService.Get(new GetOrderVM { ID = SQLHelper.CheckGuidNull(payment.OrderID) });
                //                order.Data.TrackingCode = paymentVerifyResult.OrderId;
                //                _orderService.Edit(order.Data);
                //                var detailResult = _service.GetDetail(SQLHelper.CheckGuidNull(payment.ID));
                //                var DownloadResult = await CreateDownload(detailResult.Data.Products, payment.ID);
                //                if (DownloadResult != null && DownloadResult.Count > 0)
                //                    ViewBag.Download = DownloadResult;
                //                var shoppingID = HttpContext.Request.Cookies.Get("ShoppingID").Value;
                //                var shoppingCartItem = _shoppingCartItemService.List(SQLHelper.CheckGuidNull(shoppingID));
                //                foreach (var item in shoppingCartItem.Data)
                //                {
                //                    item.Product.StockQuantity -= item.Quantity;
                //                    _productService.Edit(item.Product);
                //                }
                //                break;
                //            }
                //        case ResCodeVerify.مهلت_ارسال_تراکنش_به_پایان_رسیده_است:
                //            {
                //                payment.TransactionStatus = (int)ResCodeVerify.مهلت_ارسال_تراکنش_به_پایان_رسیده_است;
                //                payment.TransactionStatusMessage = ResCodeVerify.مهلت_ارسال_تراکنش_به_پایان_رسیده_است.ToString();
                //                break;
                //            }
                //        case ResCodeVerify.پارامترهای_ارسالی_صحیح_نیست_و_یا_تراکنش_در_سیستم_وجود_ندارد:
                //            {
                //                payment.TransactionStatus = (int)ResCodeVerify.پارامترهای_ارسالی_صحیح_نیست_و_یا_تراکنش_در_سیستم_وجود_ندارد;
                //                payment.TransactionStatusMessage = ResCodeVerify.پارامترهای_ارسالی_صحیح_نیست_و_یا_تراکنش_در_سیستم_وجود_ندارد.ToString();
                //                break;
                //            }
                //        case ResCodeVerify.آدرس_بازگشت_پذیرنده_نامعتبر_است:
                //            {
                //                payment.TransactionStatus = (int)ResCodeVerify.آدرس_بازگشت_پذیرنده_نامعتبر_است;
                //                payment.TransactionStatusMessage = ResCodeVerify.آدرس_بازگشت_پذیرنده_نامعتبر_است.ToString();
                //                break;
                //            }
                //        case ResCodeVerify.آی_پی_پذیرنده_اشتباه_است:
                //            {
                //                payment.TransactionStatus = (int)ResCodeVerify.آی_پی_پذیرنده_اشتباه_است;
                //                payment.TransactionStatusMessage = ResCodeVerify.آی_پی_پذیرنده_اشتباه_است.ToString();
                //                break;
                //            }
                //        case ResCodeVerify.اشکال_در_تاریخ_و_زمان_سیستم:
                //            {
                //                payment.TransactionStatus = (int)ResCodeVerify.اشکال_در_تاریخ_و_زمان_سیستم;
                //                payment.TransactionStatusMessage = ResCodeVerify.اشکال_در_تاریخ_و_زمان_سیستم.ToString();
                //                break;
                //            }
                //        case ResCodeVerify.امکان_پرداخت_از_طریق_سیستم_شتاب_برای_این_پذیرنده_امکان_پذیر_نیست:
                //            {
                //                payment.TransactionStatus = (int)ResCodeVerify.امکان_پرداخت_از_طریق_سیستم_شتاب_برای_این_پذیرنده_امکان_پذیر_نیست;
                //                payment.TransactionStatusMessage = ResCodeVerify.امکان_پرداخت_از_طریق_سیستم_شتاب_برای_این_پذیرنده_امکان_پذیر_نیست.ToString();
                //                break;
                //            }
                //        case ResCodeVerify.انجام_تراکنش_مربوطه_توسط_پایانه_ی_انجام_دهنده_مجاز_نمیباشد:
                //            {
                //                payment.TransactionStatus = (int)ResCodeVerify.انجام_تراکنش_مربوطه_توسط_پایانه_ی_انجام_دهنده_مجاز_نمیباشد;
                //                payment.TransactionStatusMessage = ResCodeVerify.انجام_تراکنش_مربوطه_توسط_پایانه_ی_انجام_دهنده_مجاز_نمیباشد.ToString();
                //                break;
                //            }
                //        case ResCodeVerify.ترتیب_پارامترهای_ارسالی_اشتباه_است:
                //            {
                //                payment.TransactionStatus = (int)ResCodeVerify.ترتیب_پارامترهای_ارسالی_اشتباه_است;
                //                payment.TransactionStatusMessage = ResCodeVerify.ترتیب_پارامترهای_ارسالی_اشتباه_است.ToString();
                //                break;
                //            }
                //        case ResCodeVerify.توکن_ارسالی_نامعتبر_است:
                //            {
                //                payment.TransactionStatus = (int)ResCodeVerify.توکن_ارسالی_نامعتبر_است;
                //                payment.TransactionStatusMessage = ResCodeVerify.توکن_ارسالی_نامعتبر_است.ToString();
                //                break;
                //            }
                //        case ResCodeVerify.خطا_در_سیستم:
                //            {
                //                payment.TransactionStatus = (int)ResCodeVerify.خطا_در_سیستم;
                //                payment.TransactionStatusMessage = ResCodeVerify.خطا_در_سیستم.ToString();
                //                break;
                //            }
                //        case ResCodeVerify.خطا_در_سیستم_تراکنش_ناموفق:
                //            {
                //                payment.TransactionStatus = (int)ResCodeVerify.خطا_در_سیستم_تراکنش_ناموفق;
                //                payment.TransactionStatusMessage = ResCodeVerify.خطا_در_سیستم_تراکنش_ناموفق.ToString();
                //                break;
                //            }
                //        case ResCodeVerify.خطای_دسترسی:
                //            {
                //                payment.TransactionStatus = (int)ResCodeVerify.خطای_دسترسی;
                //                payment.TransactionStatusMessage = ResCodeVerify.خطای_دسترسی.ToString();
                //                break;
                //            }
                //        case ResCodeVerify.درخواست_تکراری_شماره_سفارش_تکراری_میباشد:
                //            {
                //                payment.TransactionStatus = (int)ResCodeVerify.درخواست_تکراری_شماره_سفارش_تکراری_میباشد;
                //                payment.TransactionStatusMessage = ResCodeVerify.درخواست_تکراری_شماره_سفارش_تکراری_میباشد.ToString();
                //                break;
                //            }
                //        case ResCodeVerify.شماره_سفارش_تراکنش_نامعتبر_است:
                //            {
                //                payment.TransactionStatus = (int)ResCodeVerify.شماره_سفارش_تراکنش_نامعتبر_است;
                //                payment.TransactionStatusMessage = ResCodeVerify.شماره_سفارش_تراکنش_نامعتبر_است.ToString();
                //                break;
                //            }
                //        case ResCodeVerify.مبلغ_تراکنش_از_حد_مجاز_بالاتر_است:
                //            {
                //                payment.TransactionStatus = (int)ResCodeVerify.مبلغ_تراکنش_از_حد_مجاز_بالاتر_است;
                //                payment.TransactionStatusMessage = ResCodeVerify.مبلغ_تراکنش_از_حد_مجاز_بالاتر_است.ToString();
                //                break;
                //            }
                //        case ResCodeVerify.مبلغ_تراکنش_نامعتبر_است:
                //            {
                //                payment.TransactionStatus = (int)ResCodeVerify.مبلغ_تراکنش_نامعتبر_است;
                //                payment.TransactionStatusMessage = ResCodeVerify.مبلغ_تراکنش_نامعتبر_است.ToString();
                //                break;
                //            }
                //        case ResCodeVerify.پذیرنده_غیر_فعال_شده_است:
                //            {
                //                payment.TransactionStatus = (int)ResCodeVerify.پذیرنده_غیر_فعال_شده_است;
                //                payment.TransactionStatusMessage = ResCodeVerify.پذیرنده_غیر_فعال_شده_است.ToString();
                //                break;
                //            }
                //        case ResCodeVerify.پذیرنده_کارت_فعال_نیست:
                //            {
                //                payment.TransactionStatus = (int)ResCodeVerify.پذیرنده_کارت_فعال_نیست;
                //                payment.TransactionStatusMessage = ResCodeVerify.پذیرنده_کارت_فعال_نیست.ToString();
                //                break;
                //            }
                //        case ResCodeVerify.پذیرنده_کارت_نامعتبر_است:
                //            {
                //                payment.TransactionStatus = (int)ResCodeVerify.پذیرنده_کارت_نامعتبر_است;
                //                payment.TransactionStatusMessage = ResCodeVerify.پذیرنده_کارت_نامعتبر_است.ToString();
                //                break;
                //            }
                //        default:
                //            {
                //                payment.TransactionStatusMessage = paymentVerifyResult.Description;
                //                payment.TransactionStatus = -100;
                //                _service.Edit(payment);
                //                break;
                //            }
                //    }
                //    _service.Edit(payment);
                //    AddBaseDocument(payment.ID);
                //    ClearCookie("ShoppingID");
                //    ClearCookie("Data");
                //    var detailResults = _service.GetDetail(SQLHelper.CheckGuidNull(payment.ID));
                //    var DownloadResults = await CreateDownload(detailResults.Data.Products, payment.ID);
                //    if (DownloadResults != null && DownloadResults.Count > 0)
                //        ViewBag.Download = DownloadResults;
                //    return View(paymentVerifyResult);
                //}
                //else
                //{
                //    var detailResult = _service.GetDetail(SQLHelper.CheckGuidNull(payment.ID));
                //    var DownloadResult = await CreateDownload(detailResult.Data.Products, payment.ID);
                //    if (DownloadResult != null && DownloadResult.Count > 0)
                //        ViewBag.Download = DownloadResult;

                //    var paymentVerifyResult = new VerifyResultData()
                //    {
                //        Amount = payment.Price.ToString(),
                //        Description = payment.TransactionStatusMessage,
                //        RetrivalRefNo = payment.RetrivalRefNo,
                //        SystemTraceNo = payment.SystemTraceNo,
                //        ResCode = payment.TransactionStatus.ToString()

                //    };
                //    return View(paymentVerifyResult);
                //}

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
        private async Task<List<Download>> CreateDownload(List<Product> products, Guid PaymentID)
        {
            try
            {
                var downloads = new List<Download>();
                var hasDownloadResult = _downloadService.List(PaymentID);
                if (!hasDownloadResult.Success)
                {
                    foreach (var item in products)
                    {
                        if (item.IsDownload)
                        {
                            var attachmentResult = _attachmentService.List(item.ID);
                            if (!attachmentResult.Success)
                                return null;
                            var attachment = attachmentResult.Data.Where(x => x.PathType == PathType.file).First();
                            var attachmentByDb = _attachmentService.Get(attachment.ID);
                            downloads.Add(new Download() { Data = attachmentByDb.Data.Data, PaymentID = PaymentID, UserID = SQLHelper.CheckGuidNull(User.Identity.Name) , Comment = item.Name });
                        }
                    }
                    if (downloads.Count > 0)
                    {
                        foreach (var item in downloads)
                        {
                            _downloadService.Add(item);
                        }

                        return await Task.FromResult(downloads);
                    }
                }
                else
                    return hasDownloadResult.Data;
                return null;
            }
            catch (Exception e) { return null; }
        }

        private bool AddBaseDocument(Guid PaymentID)
        {
            return true;
        }
        #region Test
        public ActionResult RedirectMelliTest()
        {
            //var purchase = new PurchaseResult()
            //{
            //    ResCode = "0",
            //    Token= "SADAD-5eabccebd8a3c"
            //};
            return RedirectToAction("Melli", "Redirect",new
            {
                ResCode = "0",
                Token = "SADAD-5eb04fd72681f"
            });
        }
        #endregion
    }
}
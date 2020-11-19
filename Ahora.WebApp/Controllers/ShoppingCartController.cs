using Ahora.WebApp.Factories;
using Ahora.WebApp.Models;
using FM.Payment.Bank.Melli;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using System;
using System.Web.Mvc;

namespace Ahora.WebApp.Controllers
{
    public class ShoppingCartController : BaseController<IShoppingCartItemService>
    {
        private readonly IProductService _productService;
        private readonly IAttachmentService _attachmentService;
        private readonly IProductVariantAttributeService _attributeService;
        private readonly IPaymentService _paymentService;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IUserAddressService _addressService;
        private readonly IBankService _bankService;
        private readonly IWorkContext _workContext;
        private readonly IShoppingCartModelFactory _shoppingCartModelFactory;
        public ShoppingCartController(IShoppingCartItemService service
                                     , IProductService productService
                                     , IAttachmentService attachmentService
                                     , IProductVariantAttributeService attributeService
                                     , IPaymentService paymentService
                                     , IUserService userService
                                     , IOrderService orderService
                                     , IUserAddressService addressService
                                     , IBankService bankService
                                     , IWorkContext workContext
                                     , IShoppingCartModelFactory shoppingCartModelFactory) : base(service)
        {
            _productService = productService;
            _attachmentService = attachmentService;
            _attributeService = attributeService;
            _paymentService = paymentService;
            _userService = userService;
            _orderService = orderService;
            _addressService = addressService;
            _bankService = bankService;
            _workContext = workContext;
            _shoppingCartModelFactory = shoppingCartModelFactory;

        }
        #region Cart
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult Index()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Delete(Guid ProductID)
        {
            try
            {
                var deleteResult = _shoppingCartModelFactory.DeleteShoppingCartItem(ProductID);
                if (!deleteResult.Success)
                    return Json(deleteResult , JsonRequestBehavior.AllowGet);
                return RedirectToAction("Cart");
            }
            catch { throw; }
        }

        [HttpPost]
        public ActionResult QuantityPlus(Guid ProductID)
        {
            try
            {
                var quantityPlusResult = _shoppingCartModelFactory.QuantityPlus(ProductID);
                if (!quantityPlusResult.Success)
                    return Json(new { error = quantityPlusResult.Message }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Cart");

            }
            catch { throw; }
        }
        public ActionResult Cart()
        {
            try
            {
                if (_workContext.ShoppingID == null)
                    return View("Error", new Error { ClassCss = "alert alert-danger", ErorrDescription = "خطا در بازیابی سبد خرید" });

                var CartDetailResult = _shoppingCartModelFactory.CartDetail();
                if (CartDetailResult == null || CartDetailResult.AvailableShoppingCartItem.Count == 0)
                    return View("~/Views/ShoppingCart/Partial/_PartialCartEmpty.cshtml");

                return PartialView("_PartialCart", CartDetailResult);
            }
            catch (Exception e) { return View("error"); }
        }
        [HttpPost]
        public ActionResult QuantityMinus(Guid ProductID)
        {
            var quantityMinusResult = _shoppingCartModelFactory.QuantityMinus(ProductID);
            if (!quantityMinusResult.Success)
                return Json(new { error = quantityMinusResult.Message }, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Cart");
        }
        public ActionResult CartEmpty()
        {
            return View();
        }
        #endregion
        //        public ActionResult Shopping()
        //        {
        //            try
        //            {
        //                var shoppingID = HttpContext.Request.Cookies.Get("ShoppingID").Value;
        //                var result = _service.List(SQLHelper.CheckGuidNull(shoppingID));
        //                var address = _addressService.List(new UserAddressListVM() { UserID = _workContext.User.ID });

        //                if (!result.Success || result.Data.Count == 0)
        //                    return RedirectToAction("CartEmpty");

        //                ViewBag.StateAddress = false;
        //                if (address.Success)
        //                {
        //                    ViewBag.StateAddress = true;
        //                    ViewBag.drpAddress = address.Data;
        //                }

        //                var cart = new List<ShoppingItemVM>();
        //                foreach (var item in result.Data)
        //                {
        //                    var product = item.Product;
        //                    var picUrl = $"{product.Attachments.Select(x => x.Path).First()}/{product.Attachments.Where(x => x.Type == AttachmentType.اصلی).Select(x => x.FileName).FirstOrDefault()}";
        //                    var attribute = new List<AttributeJsonVM>();
        //                    if (item.AttributeJson != "" && item.AttributeJson != null)
        //                    {
        //                        var json = JsonConvert.DeserializeObject<List<AttributeJsonVM>>(item.AttributeJson);
        //                        json.ForEach(x =>
        //                        {
        //                            attribute.Add(x);
        //                        });
        //                    }
        //                    cart.Add(new ShoppingItemVM
        //                    {
        //                        Product = product,
        //                        ShoppingID = SQLHelper.CheckGuidNull(shoppingID),
        //                        ImageUrl = picUrl,
        //                        Attribute = attribute,
        //                        Quantity = item.Quantity,
        //                        DiscountAmount = item.DiscountAmount,
        //                        DiscountName = item.DiscountName,
        //                        DiscountType = item.DiscountType
        //                    });

        //                }
        //                return View(cart);
        //            }
        //            catch (Exception e) { return View("error"); }

        //        }

        //        [HttpPost]
        //        public async Task<JsonResult> Shopping(UserAddress model)
        //        {
        //            try
        //            {
        //                var bankActiveResult = _bankService.GetActiveBank();
        //                if (!bankActiveResult.Success)
        //                    return Json(new { status = false, type = 1, url = Url.RouteUrl("Error") });
        //                var bankActive = bankActiveResult.Data;

        //                var shoppingID = HttpContext.Request.Cookies.Get("ShoppingID").Value;
        //                var shoppingCartItemResult = _service.List(SQLHelper.CheckGuidNull(shoppingID));

        //                if (!shoppingCartItemResult.Success)
        //                    return Json(new { status = false, type = 1, url = Url.RouteUrl("Error") });

        //                var shoppingCartItem = shoppingCartItemResult.Data;

        //                var order = new Order();
        //                var orderDetail = new OrderDetail();
        //                var payment = new FM.Portal.Core.Model.Payment();

        //                decimal amount = 0;
        //                if (shoppingCartItem == null)
        //                    return Json(new { status = false, type = 1, url = Url.RouteUrl("CartEmpty") });

        //                string attributeJson = null;
        //                string productJson = null;
        //                var listAttribute = new List<AttributeJsonVM>();
        //                var productList = new List<Product>();
        //                foreach (var item in shoppingCartItem)
        //                {
        //                    if (CheckQuantity(item))
        //                    {
        //                        decimal temp1 = 0;
        //                        decimal temp2 = 0;
        //                        decimal attributePrice = 0;
        //                        var productResult =_productService.Get(item.Product.ID);
        //                        var product = productResult.Data;

        //                        var price = product.Price;
        //                        if (product.HasDiscount)
        //                        {
        //                            if (product.DiscountType != DiscountType.نامشخص)
        //                            {
        //                                switch (product.DiscountType)
        //                                {
        //                                    case DiscountType.درصدی:
        //                                        temp1 = (product.Price * product.Discount) / 100;
        //                                        break;
        //                                    case DiscountType.مبلغی:
        //                                        temp1 = product.Discount;
        //                                        break;
        //                                }
        //                            }
        //                        }

        //                        if (product.Category.HasDiscountsApplied)
        //                        {
        //                            switch (item.DiscountType)
        //                            {
        //                                case DiscountType.درصدی:
        //                                    temp2 = (product.Price * item.DiscountAmount) / 100;
        //                                    break;
        //                                case DiscountType.مبلغی:
        //                                    temp2 = item.DiscountAmount;
        //                                    break;
        //                            }
        //                        }

        //                        if (item.AttributeJson != "" && item.AttributeJson != null)
        //                        {
        //                            listAttribute = JsonConvert.DeserializeObject<List<AttributeJsonVM>>(item.AttributeJson);
        //                            var attributes = JsonConvert.DeserializeObject<List<AttributeJsonVM>>(item.AttributeJson);
        //                            foreach (var attribute in attributes)
        //                            {
        //                                if (attribute.Price > 0)
        //                                    attributePrice += attribute.Price;
        //                            }

        //                        }
        //                        product.CountSelect = item.Quantity;

        //                        amount += attributePrice + (price - (temp1 + temp2)) * item.Quantity;
        //                        productList.Add(product);
        //                    }
        //                    else
        //                        return Json(new { status = false, type = 1, message = "خطا" });
        //                }

        //                attributeJson = JsonConvert.SerializeObject(listAttribute);
        //                productJson = JsonConvert.SerializeObject(productList);

        //                var shippingCost = shoppingCartItem.Where(x => x.Product.ShippingCost != null).OrderByDescending(x => x.Product.ShippingCost.Priority).Select(x => x.Product.ShippingCost).FirstOrDefault();
        //                if (shippingCost != null && shippingCost.Price > 0)
        //                {
        //                    amount += shippingCost.Price;
        //                }
        //                else
        //                {
        //                    if (amount < Helper.ShoppingCartRate)
        //                    {
        //                        amount += Helper.ShoppingCartRate;
        //                    }
        //                }
        //                if (model.ID == Guid.Empty)
        //                {
        //                    _addressService.Add(model);
        //                }

        //                order.AddressID = model.ID;

        //                order.SendType = SendType.آنلاین;
        //                order.ShoppingID = SQLHelper.CheckGuidNull(shoppingID);
        //                order.UserID = _workContext.User.ID;
        //                order.Price = amount;
        //                orderDetail.ProductJson = productJson;
        //                orderDetail.AttributeJson = attributeJson;
        //                orderDetail.ShoppingCartJson = JsonConvert.SerializeObject(shoppingCartItem);
        //                orderDetail.Quantity = shoppingCartItem.Count;
        //                orderDetail.UserJson = JsonConvert.SerializeObject(_userService.Get(_workContext.User.ID).Data);

        //                payment.UserID = SQLHelper.CheckGuidNull(_workContext.User.ID);
        //                payment.Price = amount;


        //                return await Melli((long)amount, payment, order, bankActive, orderDetail);
        //            }
        //            catch (Exception e) { return Json(new { status = false, type = 1, url = Url.RouteUrl("CartEmpty") }); }

        //        }



        //        #region Bank Melli
        //        private async Task<JsonResult> Melli(long amount, FM.Portal.Core.Model.Payment payment, Order order, Bank bankActive, OrderDetail orderDetail)
        //        {
        //            var _bankMelli = new BankMelli();

        //            var bankResult = await _bankMelli.PaymentRequest((long)amount);
        //            switch (bankResult.ResCode)
        //            {
        //                case "0":
        //                    {
        //                        payment.Token = bankResult.Token;
        //                        order.BankID = bankActive.ID;
        //                        var firstStep = _paymentService.FirstStepPayment(order, orderDetail, payment);
        //                        if (!firstStep.Success)
        //                        {
        //                            return Json(new { status = false, type = 1, url = Url.RouteUrl("Error") });
        //                        }
        //                        else
        //                        {
        //                            HttpCookie merchantTerminalKeyCookie = new HttpCookie("Data", bankResult.Request);
        //                            Response.Cookies.Add(merchantTerminalKeyCookie);

        //                            return Json(new { status = true, url = MelliPurchase(bankResult.Token) });
        //                        }
        //                    }
        //                default:
        //                    return Json(new { status = false, type = 1, url = Url.RouteUrl("Error") });
        //            }
        //        }
        //        private string MelliPurchase(string Token)
        //        {
        //            var _bankMelli = new BankMelli();
        //            return _bankMelli.PaymentPurchase(Token);
        //        }
        //        #endregion

        //        #region Shopping
        //        private bool CheckQuantity(ShoppingCartItem shoppingCartItem)
        //        {
        //            if (shoppingCartItem.Product.StockQuantity > 0 && shoppingCartItem.Product.StockQuantity >= shoppingCartItem.Quantity)
        //                return true;
        //            return false;
        //        }
        //        #endregion
    }
}
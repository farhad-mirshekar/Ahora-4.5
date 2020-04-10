﻿//using FM.Melli.Service;
//using FM.Payment.Melli.Model;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ahora.WebApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartItemService _service;
        private readonly IProductService _productService;
        private readonly IAttachmentService _attachmentService;
        private readonly IProductVariantAttributeService _attributeService;
        private readonly IPaymentService _paymentService;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IUserAddressService _addressService;
        public ShoppingCartController(IShoppingCartItemService service
                                     , IProductService productService
                                     , IAttachmentService attachmentService
                                     , IProductVariantAttributeService attributeService
                                     , IPaymentService paymentService
                                     , IUserService userService
                                     , IOrderService orderService
                                     , IUserAddressService addressService)
        {
            _service = service;
            _productService = productService;
            _attachmentService = attachmentService;
            _attributeService = attributeService;
            _paymentService = paymentService;
            _userService = userService;
            _orderService = orderService;
            _addressService = addressService;

        }
        // GET: ShoppingCart
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult Index()
        {
            return View();

        }

        public ActionResult Shopping()
        {
            try
            {
                var shoppingID = HttpContext.Request.Cookies.Get("ShoppingID").Value;
                var result = _service.List(SQLHelper.CheckGuidNull(shoppingID));
                var address = _addressService.List(SQLHelper.CheckGuidNull(User.Identity.Name));
                ViewBag.StateAddress = false;
                if (address.Success)
                {
                    ViewBag.StateAddress = true;
                    ViewBag.drpAddress = address.Data;
                }

                if (result.Data == null)
                    return View("~/Views/ShoppingCart/_PartialCartEmpty.cshtml");
                if (!result.Success)
                    return View("error");

                List<ShoppingItemVM> cart = new List<ShoppingItemVM>();
                foreach (var item in result.Data)
                {
                    var product = _productService.Get(item.ProductID);
                    if (product.Success)
                    {
                        var attachmentResult = _attachmentService.List(product.Data.ID);
                        var attachment = attachmentResult.Data;
                        var picUrl = $"{attachment.Select(x => x.Path).First()}/{attachment.Where(x => x.Type == AttachmentType.اصلی).Select(x => x.FileName).FirstOrDefault()}";
                        List<AttributeJsonVM> attribute = new List<AttributeJsonVM>();
                        if (item.AttributeJson != "")
                        {
                            var json = JsonConvert.DeserializeObject<AttributeJsonVM>(item.AttributeJson);
                            attribute.Add(json);
                        }
                        cart.Add(new ShoppingItemVM
                        {
                            ProductID = product.Data.ID,
                            ProductName = product.Data.Name,
                            ProductPrice = product.Data.Price,
                            ShoppingID = SQLHelper.CheckGuidNull(shoppingID),
                            ImageUrl = picUrl,
                            Attribute = attribute,
                            Quantity = item.Quantity
                        });
                    }

                }
                return View(cart);
            }
            catch (Exception e) { return View("error"); }

        }

        [HttpPost]
        public async Task<JsonResult> Shopping(UserAddress model)
        {
            try
            {
                var shoppingID = HttpContext.Request.Cookies.Get("ShoppingID").Value;
                var result = _service.List(SQLHelper.CheckGuidNull(shoppingID));
                if(!result.Success)
                    return Json(new { status = false, type = 2, message = "دوباره امتحان کنید" });

                var orderResult = _orderService.Get(new GetOrderVM {ShoppingID = SQLHelper.CheckGuidNull(shoppingID) });
                if(!orderResult.Success)
                    return Json(new { status = false, type = 2, message = "دوباره امتحان کنید" });

                if(orderResult.Data.ShoppingID != Guid.Empty)
                {
                    return Json(new { status = false, type = 2, message = "دوباره امتحان کنید" });
                }
                else
                {
                    Order order = new Order();
                    OrderDetail orderDetail = new OrderDetail();
                    Payment payment = new Payment();

                    decimal amount = 0;
                    if (result.Data == null)
                        return Json(new { status = false, type = 1, url = Url.RouteUrl("CartEmpty") });
                    if (!result.Success)
                        return Json(new { status = false, type = 1, url = Url.RouteUrl("CartEmpty") });

                    string attributeJson = null;
                    string productJson = null;
                    var listAttribute = new List<AttributeJsonVM>();
                    var productList = new List<Product>();
                    foreach (var item in result.Data)
                    {
                        var product = _productService.Get(item.ProductID);
                        if (product.Success)
                        {
                            amount += product.Data.Price * item.Quantity;
                            if(item.AttributeJson != "")
                                listAttribute.Add(JsonConvert.DeserializeObject<AttributeJsonVM>(item.AttributeJson));
                            productList.Add(product.Data);
                        }
                    }
                    attributeJson = JsonConvert.SerializeObject(listAttribute);
                    productJson = JsonConvert.SerializeObject(productList);
                    if (model.ID == Guid.Empty)
                    {
                        _addressService.Add(model);
                    }

                    order.AddressID = model.ID;

                    order.SendType = SendType.آنلاین;
                    order.ShoppingID = SQLHelper.CheckGuidNull(shoppingID);
                    order.UserID = SQLHelper.CheckGuidNull(HttpContext.User.Identity.Name);
                    order.Price = amount;
                    orderDetail.ProductJson = productJson;
                    orderDetail.AttributeJson = attributeJson;
                    orderDetail.UserJson = JsonConvert.SerializeObject(_userService.Get(SQLHelper.CheckGuidNull(HttpContext.User.Identity.Name)).Data);

                    payment.UserID = SQLHelper.CheckGuidNull(HttpContext.User.Identity.Name);
                    payment.Price = amount;

                    var firstStep = _paymentService.FirstStepPayment(order, orderDetail, payment);
                    if (!firstStep.Success)
                        return Json(new { status = false, type = 2, message = "دوباره امتحان کنید" });

                    ////var orders = _orderService.Get(order.ID);
                    ////var bank = await Melli((long)amount, orders.Data.TrackingCode.ToString());

                    ////switch (bank.ResCode)
                    ////{
                    ////    case "1":
                    ////        return Json(new { status = true,url="http://www.google.com" });
                    ////}
                    return Json(new { status = false, type = 2, message = "دوباره امتحان کنید" });
                }
               
            }
            catch (Exception e) { return Json(new { status = false, type = 1, url = Url.RouteUrl("CartEmpty") }); }

        }

        [HttpPost]
        public ActionResult Delete(DeleteCartItemVM model)
        {
            try
            {
                _service.Delete(model);
                return RedirectToAction("Index");
            }
            catch { throw; }
        }

        [HttpPost]
        public ActionResult QuantityPlus(Guid ProductID)
        {
            try
            {
                var shoppingID = HttpContext.Request.Cookies.Get("ShoppingID").Value;
                var result = _service.Get(SQLHelper.CheckGuidNull(shoppingID), ProductID);

                result.Data.Quantity += 1;
                _service.Edit(result.Data);
                return RedirectToAction("Cart");
            }
            catch { throw; }
        }
        public ActionResult Cart()
        {
            try
            {
                var shoppingID = HttpContext.Request.Cookies.Get("ShoppingID").Value;
                var result = _service.List(SQLHelper.CheckGuidNull(shoppingID));
                if (result.Data == null)
                    return View("~/Views/ShoppingCart/_PartialCartEmpty.cshtml");
                if (!result.Success)
                    return View("error");

                List<ShoppingItemVM> cart = new List<ShoppingItemVM>();
                foreach (var item in result.Data)
                {
                    var product = _productService.Get(item.ProductID);
                    if (product.Success)
                    {
                        var attachmentResult = _attachmentService.List(product.Data.ID);
                        var attachment = attachmentResult.Data;
                        var picUrl = $"{attachment.Select(x => x.Path).First()}/{attachment.Where(x => x.Type == AttachmentType.اصلی).Select(x => x.FileName).FirstOrDefault()}";
                        List<AttributeJsonVM> attribute = new List<AttributeJsonVM>();
                        if (item.AttributeJson != "")
                        {
                            var json = JsonConvert.DeserializeObject<AttributeJsonVM>(item.AttributeJson);
                            attribute.Add(json);
                        }
                        cart.Add(new ShoppingItemVM
                        {
                            ProductID = product.Data.ID,
                            ProductName = product.Data.Name,
                            ProductPrice = product.Data.Price,
                            ShoppingID = SQLHelper.CheckGuidNull(shoppingID),
                            ImageUrl = picUrl,
                            Attribute = attribute,
                            Quantity = item.Quantity,
                            HasDiscountsApplied = item.HasDiscountsApplied,
                            DiscountAmount = item.DiscountAmount,
                            DiscountName = item.DiscountName
                        });
                    }

                }
                return PartialView("_PartialCart",cart);
            }
            catch (Exception e) { return View("error"); }
        }
        [HttpPost]
        public ActionResult QuantityMinus(Guid ProductID)
        {
            try
            {
                var shoppingID = HttpContext.Request.Cookies.Get("ShoppingID").Value;
                var result = _service.Get(SQLHelper.CheckGuidNull(shoppingID), ProductID);

                result.Data.Quantity -= 1;
                if (result.Data.Quantity == 0)
                    result.Data.Quantity = 1;
                _service.Edit(result.Data);
                return RedirectToAction("Cart");
            }
            catch { throw; }
        }
        public ActionResult CartEmpty()
        {
            return View();
        }

        //private async Task<PayResultData> Melli(long amount, string OrderID)
        //{
        //    BankMelli _bankMelli = new BankMelli(ConfigurationManager.AppSettings["MerchantIdMelli"].ToString()
        //                            , ConfigurationManager.AppSettings["MerchantKeyMelli"].ToString()
        //                            , ConfigurationManager.AppSettings["TerminalIdMelli"].ToString()
        //                            , ConfigurationManager.AppSettings["RedirectPath"].ToString()
        //                            , ConfigurationManager.AppSettings["PurchasePageMelli"].ToString());

        //    var bankResult = await _bankMelli.PaymentResult((long)amount, OrderID);
        //    return bankResult;
        //}

    }
}
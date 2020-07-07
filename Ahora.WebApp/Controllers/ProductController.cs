﻿using model = FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using System;
using System.Web.Mvc;
using System.Web;
using FM.Portal.Core.Common;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Ahora.WebApp.Models;
using FM.Portal.Core.Model;

namespace Ahora.WebApp.Controllers
{
    public class ProductController : BaseController<IProductService>
    {
        private readonly IAttachmentService _attachmentService;
        private readonly IShoppingCartItemService _shoppingCartService;
        private readonly IProductMapAttributeService _productMapattributeService;
        private readonly IProductVariantAttributeService _productVariantAttributeService;
        private readonly ICompareProductService _compareProductService;
        public ProductController(IProductService service
                                 , IAttachmentService attachmentService
                                 , IShoppingCartItemService shoppingCartService
                                 , IProductMapAttributeService productMapattributeService
                                 , IProductVariantAttributeService productVariantAttributeService
                                 , ICompareProductService compareProductService) : base(service)
        {
            _attachmentService = attachmentService;
            _shoppingCartService = shoppingCartService;
            _productMapattributeService = productMapattributeService;
            _productVariantAttributeService = productVariantAttributeService;
            _compareProductService = compareProductService;
        }

        #region Product
        // GET: Product
        public ActionResult Index(string TrackingCode, String Title)
        {
            var result = _service.Get(TrackingCode);
            ViewBag.attribute = new List<model.ListAttributeForSelectCustomerVM>();
            var resultPic = _attachmentService.List(result.Data.ID);
            var attribute = _service.SelectAttributeForCustomer(result.Data.ID);
            if (attribute.Success && attribute.Data.Count > 0)
                ViewBag.attribute = attribute.Data;
            ViewBag.pic = resultPic.Data.Where(p => p.PathType == model.PathType.product).ToList();
            SetCookie("ShoppingID");
            return View(result.Data);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddToCart(Guid ProductID, FormCollection form)
        {


            var product = _service.Get(ProductID);
            if (!product.Success)
                return Json(new
                {
                    success = false,
                    message = "خطایی اتفاق افتاده است. دوباره امتحان کنید"
                });
            if (product.Data.StockQuantity == 0)
                return Json(new
                {
                    success = false,
                    message = "موجودی محصول تمام شده است"
                });
            //var attributes = _service.SelectAttributeForCustomer(ProductID);

            if (User.Identity.Name == "")
                return Json(new
                {
                    success = false,
                    message = "ابتدا باید وارد شوید"
                });
            JsonResult result = _AddToCart(product.Data, form);
            return result;
        }

        private JsonResult _AddToCart(model.Product product, FormCollection form)
        {
            try
            {
                var attributes = _service.SelectAttributeForCustomer(product.ID);
                if (!attributes.Success)
                    return Json(new
                    {
                        success = false,
                        message = "خطایی اتفاق افتاده است. دوباره امتحان کنید"
                    });

                var attribute = attributes.Data;
                if (attribute.Count > 0)
                {
                    return _AddToCartWithAttribute(product, form, attribute);
                }
                else
                    return _AddToCartWithOutAttribute(product);

            }
            catch (Exception e)
            {
                return Json(new
                {
                    success = false,
                    message = "خطایی اتفاق افتاده است. دوباره امتحان کنید"
                });
            }
        }

        private void SetCookie(string param)
        {
            if (HttpContext.Request.Cookies[param] == null)
            {
                HttpContext.Response.Cookies.Remove(param);
                HttpContext.Response.Cookies[param].Value = Guid.NewGuid().ToString();
                HttpContext.Response.Cookies[param].Expires = DateTime.Now.AddYears(50);
            }
            if (HttpContext.Request.Cookies[param].Value == null ||
                HttpContext.Request.Cookies[param].Value == string.Empty)
            {
                HttpContext.Response.Cookies.Remove(param);
                HttpContext.Response.Cookies[param].Value = Guid.NewGuid().ToString();
                HttpContext.Response.Cookies[param].Expires = DateTime.Now.AddYears(50);

            }
        }
        private JsonResult _AddToCartWithAttribute(model.Product product, FormCollection form, List<model.ListAttributeForSelectCustomerVM> attributes)
        {
            try
            {
                var attributeChosen = new List<model.AttributeJsonVM>();
                if (attributes.Count > 0)
                {
                    foreach (var item in attributes)
                    {
                        string controlId = string.Format("product_attribute_{0}_{1}_{2}_{3}", item.ProductID, item.AttributeID, item.ID, item.TextPrompt);

                        switch (item.AttributeControlType)
                        {
                            case model.AttributeControlType.کشویی:
                                {
                                    var ctrlAttributes = form[controlId];
                                    if (ctrlAttributes == "-1")
                                    {
                                        return Json(new
                                        {
                                            success = false,
                                            message = $"{item.TextPrompt} را انتخاب نمایید"
                                        });
                                    }
                                    if (!String.IsNullOrEmpty(ctrlAttributes))
                                    {
                                        var selectedAttributeId = SQLHelper.CheckGuidNull(ctrlAttributes);
                                        var attributeMain = _productMapattributeService.Get(item.ID);
                                        var attributeDetail = _productVariantAttributeService.Get(selectedAttributeId);
                                        if (!attributeMain.Success || !attributeDetail.Success || attributeDetail.Data.ID == Guid.Empty || attributeDetail.Data == null)
                                            return Json(new
                                            {
                                                success = false,
                                                message = $"{item.TextPrompt} را انتخاب نمایید"
                                            });

                                        attributeChosen.Add(new model.AttributeJsonVM()
                                        {
                                            AttributeName = attributeMain.Data.TextPrompt,
                                            ID = selectedAttributeId,
                                            Name = attributeDetail.Data.Name,
                                            Price = attributeDetail.Data.Price,
                                            ProductVariantAttributeID = attributeMain.Data.ID
                                        });
                                    }
                                    break;
                                }
                        }
                    }

                    var cart = new model.ShoppingCartItem();
                    cart.ProductID = product.ID;
                    cart.UserID = SQLHelper.CheckGuidNull(User.Identity.Name);
                    cart.ShoppingID = SQLHelper.CheckGuidNull(Request.Cookies["ShoppingID"].Value);
                    if (attributeChosen.Count > 0)
                        cart.AttributeJson = JsonConvert.SerializeObject(attributeChosen);
                    else
                        cart.AttributeJson = null;

                    var shopResult = _shoppingCartService.Get(cart.ShoppingID, cart.ProductID);
                    if (shopResult.Data != null && shopResult.Data.ID != Guid.Empty)
                    {
                        cart.Quantity = shopResult.Data.Quantity + 1;
                        var result = _shoppingCartService.Edit(cart);
                        if (!result.Success)
                            return Json(new
                            {
                                success = false,
                                message = result.Message
                            });
                    }
                    else
                    {
                        cart.Quantity = 1;
                        var result = _shoppingCartService.Add(cart);
                        if (!result.Success)
                            return Json(new
                            {
                                success = false,
                                message = result.Message
                            });
                    }
                }

                return Json(new
                {
                    success = true,
                    message = "محصول با موفقیت درج گردید",
                    redirect = Url.RouteUrl("Cart")
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    success = false,
                    message = "خطایی اتفاق افتاده است. دوباره امتحان کنید"
                });
            }
        }
        private JsonResult _AddToCartWithOutAttribute(model.Product product)
        {
            try
            {
                var cart = new model.ShoppingCartItem();
                cart.ProductID = product.ID;
                cart.UserID = SQLHelper.CheckGuidNull(User.Identity.Name);
                cart.ShoppingID = SQLHelper.CheckGuidNull(Request.Cookies["ShoppingID"].Value);
                cart.AttributeJson = null;
                var shopResult = _shoppingCartService.Get(cart.ShoppingID, cart.ProductID);

                if (shopResult.Data != null && shopResult.Data.ID != Guid.Empty)
                {
                    cart.Quantity = shopResult.Data.Quantity + 1;
                    var result = _shoppingCartService.Edit(cart);
                    if (!result.Success)
                        return Json(new
                        {
                            success = false,
                            message = result.Message
                        });
                }
                else
                {
                    cart.Quantity = 1;
                    var result = _shoppingCartService.Add(cart);
                    if (!result.Success)
                        return Json(new
                        {
                            success = false,
                            message = result.Message
                        });
                }
                return Json(new
                {
                    success = true,
                    message = "محصول با موفقیت درج گردید",
                    redirect = Url.RouteUrl("Cart")
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    success = false,
                    message = "خطایی اتفاق افتاده است. دوباره امتحان کنید"
                });
            }
        }
        #endregion

        #region Product-Compare
        public ActionResult AddProductToCompareList(Guid ProductID)
        {
            var productResult = _service.Get(ProductID);
            if (!productResult.Success)
                return Json(new
                {
                    success = false,
                    message = "محصولی پبدا نشد"
                });
            _compareProductService.AddProductToCompareList(ProductID);
            return Json(new
            {
                success = true,
                message = "محصول با موفقیت به لیست مقایسه اضافه گردید"
            });
        }
        [Route("CompareProducts")]
        public ActionResult CompareProducts()
        {
            var compareProductsResult = _compareProductService.GetComparedProducts();
            if (!compareProductsResult.Success)
                return View("Error", new Error {ClassCss="alert alert-danger" , ErorrDescription="خطا در بازیابی داده" });

            var compareProducts = compareProductsResult.Data;
            foreach (var item in compareProducts)
            {
                var attachmentResult = _attachmentService.List(item.ID);
                if (attachmentResult.Data.Count > 0)
                item.PicUrl = $"{attachmentResult.Data.Select(x => x.Path).First()}/{attachmentResult.Data.Where(x => x.Type == AttachmentType.اصلی).Select(x => x.FileName).FirstOrDefault()}";

            }
            return View(compareProducts);
        }
        #endregion
    }
}
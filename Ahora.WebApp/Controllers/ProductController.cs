using model = FM.Portal.Core.Model;
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
        private readonly IWorkContext _workContext;
        public ProductController(IProductService service
                                 , IAttachmentService attachmentService
                                 , IShoppingCartItemService shoppingCartService
                                 , IProductMapAttributeService productMapattributeService
                                 , IProductVariantAttributeService productVariantAttributeService
                                 , ICompareProductService compareProductService
                                 , IWorkContext workContext) : base(service)
        {
            _attachmentService = attachmentService;
            _shoppingCartService = shoppingCartService;
            _productMapattributeService = productMapattributeService;
            _productVariantAttributeService = productVariantAttributeService;
            _compareProductService = compareProductService;
            _workContext = workContext;
        }

        #region Product
        // GET: Product
        public ActionResult Index(string TrackingCode, String Title)
        {
            var result = _service.Get(TrackingCode);
            if (!result.Success)
                RedirectToRoute("Home");

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

            if (_workContext.User == null)
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
                var attribute = product.Attributes;
                if (attribute.Count > 0)
                {
                    return _AddToCartWithAttribute(product, form);
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
        private JsonResult _AddToCartWithAttribute(model.Product product, FormCollection form)
        {
            try
            {
                var attributeChosen = new List<model.AttributeJsonVM>();
                if (product.Attributes.Count > 0)
                {
                    foreach (var item in product.Attributes)
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
                    cart.UserID = _workContext.User.ID;
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
                cart.UserID = _workContext.User.ID;
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
                message = $"محصول با موفقیت به لیست مقایسه اضافه گردید - <a href='{Url.RouteUrl("CompareProducts")}'>مشاهده</a>"
            });
        }
        public ActionResult CompareProducts()
        {
            var compareProductsResult = _compareProductService.GetComparedProducts();
            if (!compareProductsResult.Success)
                return View("Error", new Error { ClassCss = "alert alert-danger", ErorrDescription = "خطا در بازیابی داده" });

            var compareProducts = compareProductsResult.Data;
            if (compareProducts.Count > 0)
                return View(compareProducts);

            return RedirectToRoute("Home");
        }
        public ActionResult RemoveProductFromCompareList(Guid ProductID)
        {
            var productResult = _service.Get(ProductID);
            if (!productResult.Success)
                return RedirectToRoute("Home");

            _compareProductService.RemoveProductFromCompareList(ProductID);
            return RedirectToRoute("CompareProducts");
        }
        public ActionResult ClearCompareProducts()
        {
            _compareProductService.ClearCompareProducts();
            return RedirectToRoute("Home");
        }
        #endregion
    }
}
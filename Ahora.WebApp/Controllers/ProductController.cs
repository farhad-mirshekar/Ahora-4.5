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

namespace Ahora.WebApp.Controllers
{
    public class ProductController : BaseController<IProductService>
    {
        private readonly IAttachmentService _attachmentService;
        private readonly IShoppingCartItemService _shoppingCartService;
        private readonly IProductMapAttributeService _productMapattributeService;
        private readonly IProductVariantAttributeService _productVariantAttributeService;
        public ProductController(IProductService service
                                 , IAttachmentService attachmentService
                                 , IShoppingCartItemService shoppingCartService
                                 , IProductMapAttributeService productMapattributeService
                                 , IProductVariantAttributeService productVariantAttributeService) : base(service)
        {
            _attachmentService = attachmentService;
            _shoppingCartService = shoppingCartService;
            _productMapattributeService = productMapattributeService;
            _productVariantAttributeService = productVariantAttributeService;
        }

        // GET: Product
        public ActionResult Index(string TrackingCode, String Title)
        {
            var result = _service.Get(TrackingCode);
            ViewBag.attribute = new List<model.ListAttributeForSelectCustomerVM>();
            var resultPic = _attachmentService.List(result.Data.ID);
            var attribute = _service.SelectAttributeForCustomer(result.Data.ID);
            if (attribute.Success && attribute.Data.Count > 0)
                ViewBag.attribute = attribute.Data;
            ViewBag.pic = resultPic.Data.Where(p=>p.PathType == model.PathType.product).ToList();
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
            JsonResult result = decode(product.Data, form);
            return result;
        }
        
        private JsonResult decode(model.Product product, FormCollection form)
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
                foreach (var item in attribute)
                {
                    string controlId = string.Format("product_attribute_{0}_{1}_{2}_{3}", item.ProductID, item.AttributeID, item.ID,item.TextPrompt);

                    switch (item.AttributeControlType)
                    {
                        case model.AttributeControlType.کشویی:
                            {
                                var ctrlAttributes = form[controlId];
                                if(ctrlAttributes == "-1")
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = $"{item.TextPrompt} را انتخاب نمایید"
                                    });
                                }
                                if (!String.IsNullOrEmpty(ctrlAttributes))
                                {
                                    Guid selectedAttributeId = SQLHelper.CheckGuidNull(ctrlAttributes);
                                    var attributeMain = _productMapattributeService.Get(item.ID);
                                    var attributeDetail = _productVariantAttributeService.Get(selectedAttributeId);
                                    if (!attributeMain.Success || !attributeDetail.Success || attributeDetail.Data.ID == Guid.Empty)
                                        return Json(new
                                        {
                                            success = false,
                                            message = $"{item.TextPrompt} را انتخاب نمایید"
                                        });

                                    var json = new model.AttributeJsonVM();
                                    json.AttributeName = attributeMain.Data.TextPrompt;
                                    json.ID = selectedAttributeId;
                                    json.Name = attributeDetail.Data.Name;
                                    json.ProductVariantAttributeID = attributeMain.Data.ID;
                                    json.Price = attributeDetail.Data.Price;

                                    var cart = new model.ShoppingCartItem();
                                    cart.ProductID = product.ID;
                                    cart.UserID = SQLHelper.CheckGuidNull(User.Identity.Name);
                                    cart.ShoppingID = SQLHelper.CheckGuidNull(Request.Cookies["ShoppingID"].Value);
                                    cart.AttributeJson = JsonConvert.SerializeObject(json);
                                    var shopResult = _shoppingCartService.Get(cart.ShoppingID, cart.ProductID);
                                    if (shopResult.Data.ID != Guid.Empty)
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
                                break;
                            }
                    }
                }
            }else
            {
                var cart = new model.ShoppingCartItem();
                cart.ProductID = product.ID;
                cart.UserID = SQLHelper.CheckGuidNull(User.Identity.Name);
                cart.ShoppingID = SQLHelper.CheckGuidNull(Request.Cookies["ShoppingID"].Value);
                cart.AttributeJson = null;
                var shopResult = _shoppingCartService.Get(cart.ShoppingID, cart.ProductID);
                if (shopResult.Data.ID != Guid.Empty)
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
    }
}
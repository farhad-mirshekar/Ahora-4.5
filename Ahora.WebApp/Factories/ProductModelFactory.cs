using Ahora.WebApp.Models;
using Ahora.WebApp.Models.App;
using FM.Portal.Core.Caching;
using FM.Portal.Core.Common;
using FM.Portal.Core.Common.Serializer;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Ahora.WebApp.Factories
{
    public class ProductModelFactory : IProductModelFactory
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IDiscountService _discountService;
        private readonly IAttachmentService _attachmentService;
        private readonly ICategoryMapDiscountService _categoryMapDiscountService;
        private readonly IProductMapAttributeService _productMapAttributeService;
        private readonly IProductVariantAttributeValueService _productVariantAttributeValueService;
        private readonly IShoppingCartItemService _shoppingCartItemService;
        private readonly IWorkContext _workContext;
        private readonly IObjectSerializer _objectSerializer;
        private readonly ICacheManager _cacheManager;
        public ProductModelFactory(IProductService productService
                                  , ICategoryService categoryService
                                  , IDiscountService discountService
                                  , IAttachmentService attachmentService
                                  , ICategoryMapDiscountService categoryMapDiscountService
                                  , IProductMapAttributeService productMapAttributeService
                                  , IProductVariantAttributeValueService productVariantAttributeValueService
                                  , IShoppingCartItemService shoppingCartItemService
                                  , IWorkContext workContext
                                  , IObjectSerializer objectSerializer
                                  , ICacheManager cacheManager)
        {
            _productService = productService;
            _categoryService = categoryService;
            _discountService = discountService;
            _attachmentService = attachmentService;
            _categoryMapDiscountService = categoryMapDiscountService;
            _productMapAttributeService = productMapAttributeService;
            _productVariantAttributeValueService = productVariantAttributeValueService;
            _shoppingCartItemService = shoppingCartItemService;
            _workContext = workContext;
            _objectSerializer = objectSerializer;
            _cacheManager = cacheManager;
        }

        public JsonResultModel AddToCartWithAttribute(ProductModel product, FormCollection form)
        {
            try
            {
                if (_workContext.ShoppingID == null)
                    return new JsonResultModel
                    {
                        Success = false,
                        Message = "خطایی اتفاق افتاده است"
                    };

                if (_workContext.User == null)
                    return new JsonResultModel
                    {
                        Success = false,
                        Message = "ابتدا وارد سایت شوید"
                    };

                var attributeChosen = new List<AttributeJsonVM>();
                if (product.Attributes.Count > 0)
                {
                    foreach (var item in product.Attributes)
                    {
                        string controlId = $"product_attribute_{item.ProductID}_{item.AttributeID}_{item.ID}_{item.TextPrompt}";

                        switch (item.AttributeControlType)
                        {
                            case AttributeControlType.کشویی:
                                {
                                    var ctrlAttributes = form[controlId];
                                    if (ctrlAttributes == "-1")
                                    {
                                        return new JsonResultModel
                                        {
                                            Success = false,
                                            Message = $"{item.TextPrompt} را انتخاب نمایید"
                                        };
                                    }
                                    if (!String.IsNullOrEmpty(ctrlAttributes))
                                    {
                                        var selectedAttributeId = SQLHelper.CheckGuidNull(ctrlAttributes);
                                        var attributeMain = _productMapAttributeService.Get(item.ID);
                                        var attributeDetail = _productVariantAttributeValueService.Get(selectedAttributeId);
                                        if (!attributeMain.Success || !attributeDetail.Success || attributeDetail.Data.ID == Guid.Empty || attributeDetail.Data == null)
                                            return new JsonResultModel
                                            {
                                                Success = false,
                                                Message = $"{item.TextPrompt} را انتخاب نمایید"
                                            };

                                        attributeChosen.Add(new AttributeJsonVM()
                                        {
                                            AttributeName = attributeMain.Data.TextPrompt,
                                            ID = selectedAttributeId,
                                            Name = attributeDetail.Data.Name,
                                            Price = attributeDetail.Data.Price,
                                            ProductVariantAttributeID = attributeMain.Data.ID,
                                            ProductID = product.ID
                                        });
                                    }
                                    break;
                                }
                        }
                    }

                    var cart = new ShoppingCartItem();
                    cart.ProductID = product.ID;
                    cart.UserID = _workContext.User.ID;
                    cart.ShoppingID = _workContext.ShoppingID.Value;
                    if (attributeChosen.Count > 0)
                        cart.AttributeJson = _objectSerializer.Serialize(attributeChosen);
                    else
                        cart.AttributeJson = null;

                    var shopResult = _shoppingCartItemService.Get(cart.ShoppingID, cart.ProductID);
                    if (shopResult.Data != null && shopResult.Data.ID != Guid.Empty)
                    {
                        cart.Quantity = shopResult.Data.Quantity + 1;
                        var result = _shoppingCartItemService.Edit(cart);
                        if (!result.Success)
                            return new JsonResultModel
                            {
                                Success = false,
                                Message = result.Message
                            };
                    }
                    else
                    {
                        cart.Quantity = 1;
                        var result = _shoppingCartItemService.Add(cart);
                        if (!result.Success)
                            return new JsonResultModel
                            {
                                Success = false,
                                Message = result.Message
                            };
                    }
                }

                _cacheManager.Remove(CacheParamExtention.Shopping_Cart_Item_Details);
                return new JsonResultModel
                {
                    Success = true,
                    Message = "محصول با موفقیت درج گردید",
                    Url = "Cart"
                };
            }
            catch (Exception e)
            {
                return new JsonResultModel() { Success = false, Message = "خطایی اتفاق افتاده است" };
            }
        }

        public JsonResultModel AddToCartWithNotAttribute(ProductModel product)
        {
            try
            {
                if (_workContext.ShoppingID == null)
                    return new JsonResultModel
                    {
                        Success = false,
                        Message = "خطایی اتفاق افتاده است"
                    };

                if (_workContext.User == null)
                    return new JsonResultModel
                    {
                        Success = false,
                        Message = "ابتدا وارد سایت شوید"
                    };

                var cart = new ShoppingCartItem();
                cart.ProductID = product.ID;
                cart.UserID = _workContext.User.ID;
                cart.ShoppingID = _workContext.ShoppingID.Value;
                cart.AttributeJson = null;
                var shopResult = _shoppingCartItemService.Get(cart.ShoppingID, cart.ProductID);

                if (shopResult.Data != null && shopResult.Data.ID != Guid.Empty)
                {
                    cart.Quantity = shopResult.Data.Quantity + 1;
                    var result = _shoppingCartItemService.Edit(cart);
                    if (!result.Success)
                        return new JsonResultModel
                        {
                            Success = false,
                            Message = result.Message
                        };
                }
                else
                {
                    cart.Quantity = 1;
                    var result = _shoppingCartItemService.Add(cart);
                    if (!result.Success)
                        return new JsonResultModel
                        {
                            Success = false,
                            Message = result.Message
                        };
                }
                _cacheManager.Remove(CacheParamExtention.Shopping_Cart_Item_Details);
                return new JsonResultModel
                {
                    Success = true,
                    Message = "محصول با موفقیت درج گردید",
                    Url = "Cart"
                };
            }
            catch (Exception e)
            {
                return new JsonResultModel
                {
                    Success = false,
                    Message = "خطایی اتفاق افتاده است. دوباره امتحان کنید"
                };
            }
        }

        public ProductModel GetProduct(Guid ID)
        {
            try
            {
                var productResult = _productService.Get(ID);
                if (!productResult.Success)
                    return null;
                var product = productResult.Data;

                var productModel = product.ToModel();

                var categoryResult = _categoryService.Get(productModel.CategoryID);
                if (!categoryResult.Success)
                    productModel.Category = null;
                productModel.Category = categoryResult.Data;

                if (productModel.Category != null)
                {
                    if (productModel.Category.HasDiscountsApplied)
                    {
                        var categoryDiscountResult = _categoryMapDiscountService.Get(productModel.CategoryID, null);
                        if (!categoryDiscountResult.Success)
                            productModel.CategoryDiscount = null;
                        else
                        {
                            var discountResult = _discountService.Get(categoryDiscountResult.Data.DiscountID);
                            if (!discountResult.Success)
                                productModel.CategoryDiscount = null;
                            productModel.CategoryDiscount = discountResult.Data;
                        }

                    }
                }

                var attachmentResult = _attachmentService.List(productModel.ID);
                if (attachmentResult.Success)
                {
                    productModel.PictureAttachments = attachmentResult.Data.Where(a => a.PathType == PathType.product).ToList();
                    productModel.VideoAttachments = attachmentResult.Data.Where(a => a.PathType == PathType.video).ToList();
                }

                var attributesResult = _productService.SelectAttributeForCustomer(productModel.ID);
                if (!attributesResult.Success)
                    productModel.Attributes = null;
                productModel.Attributes = attributesResult.Data;

                return productModel;
            }
            catch (Exception e) { return null; }
        }
    }
}
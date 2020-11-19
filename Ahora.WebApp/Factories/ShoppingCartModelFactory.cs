using Ahora.WebApp.Models;
using Ahora.WebApp.Models.App;
using FM.Portal.Core.Caching;
using FM.Portal.Core.Common;
using FM.Portal.Core.Common.Serializer;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ahora.WebApp.Factories
{
    public class ShoppingCartModelFactory : IShoppingCartModelFactory
    {
        #region Contructure
        private readonly IShoppingCartItemService _shoppingCartItemService;
        private readonly IProductService _productService;
        private readonly IAttachmentService _attachmentService;
        private readonly ICategoryService _categoryService;
        private readonly ICategoryMapDiscountService _categoryMapDiscountService;
        private readonly IDiscountService _discountService;
        private readonly IWorkContext _workContext;
        private readonly ICacheManager _cacheManager;
        private readonly IObjectSerializer _objectSerializer;
        private readonly IDeliveryDateService _deliveryDateService;
        private readonly IShippingCostService _shippingCostService;

        public ShoppingCartModelFactory(IShoppingCartItemService shoppingCartItemService
                                        , IProductService productService
                                        , IAttachmentService attachmentService
                                        , ICategoryService categoryService
                                        , ICategoryMapDiscountService categoryMapDiscountService
                                        , IDiscountService discountService
                                        , IWorkContext workContext
                                        , ICacheManager cacheManager
                                        , IObjectSerializer objectSerializer
                                        , IDeliveryDateService deliveryDateService
                                        , IShippingCostService shippingCostService)
        {
            _shoppingCartItemService = shoppingCartItemService;
            _productService = productService;
            _attachmentService = attachmentService;
            _categoryService = categoryService;
            _categoryMapDiscountService = categoryMapDiscountService;
            _discountService = discountService;
            _workContext = workContext;
            _cacheManager = cacheManager;
            _objectSerializer = objectSerializer;
            _shippingCostService = shippingCostService;
            _deliveryDateService = deliveryDateService;
        }
        #endregion

        public ShoppingCartItemListModel CartDetail()
        {
            try
            {
                return _cacheManager.Get(CacheParamExtention.Shopping_Cart_Item_Details, () =>
                {

                    var shoppingCartItemResult = _shoppingCartItemService.List(_workContext.ShoppingID.Value);
                    if (!shoppingCartItemResult.Success)
                        return null;
                    var shoppingCartItemModel = shoppingCartItemResult.Data.Select(shopping => new ShoppingCartItemModel
                    {
                        ID = shopping.ID,
                        ProductID = shopping.ProductID,
                        Quantity = shopping.Quantity,
                        CreationDate = shopping.CreationDate,
                        AttributeModel = shopping.AttributeJson != null ? _objectSerializer.DeSerialize<List<AttributeModel>>(shopping.AttributeJson) : null
                    }).ToList();

                    if (shoppingCartItemModel != null && shoppingCartItemModel.Count > 0)
                    {
                        foreach (var cartItem in shoppingCartItemModel)
                        {
                            var productResult = _productService.Get(cartItem.ProductID);
                            if (!productResult.Success)
                                cartItem.Product = null;
                            else
                                cartItem.Product = productResult.Data;

                            if (cartItem.Product != null)
                            {
                                #region Category
                                var categoryResult = _categoryService.Get(cartItem.Product.CategoryID);
                                if (!categoryResult.Success)
                                    cartItem.Category = null;
                                else
                                    cartItem.Category = categoryResult.Data;
                                #endregion

                                #region Discount
                                if (cartItem.Category != null)
                                {
                                    var categoryMapDiscountResult = _categoryMapDiscountService.Get(cartItem.Category.ID, null);
                                    if (!categoryMapDiscountResult.Success)
                                        cartItem.CategoryDiscount = null;
                                    else
                                    {
                                        var discountResult = _discountService.Get(categoryMapDiscountResult.Data.DiscountID);
                                        if (discountResult.Success)
                                            cartItem.CategoryDiscount = discountResult.Data;
                                    }
                                }
                                #endregion

                                #region Attachment
                                var attachmentsResult = _attachmentService.List(cartItem.ProductID);
                                if (attachmentsResult.Success)
                                {
                                    cartItem.PictureAttachment = attachmentsResult.Data.Where(a => a.Type == AttachmentType.اصلی).FirstOrDefault();
                                }
                                #endregion

                                #region DeliveryDate
                                if (cartItem.Product.DeliveryDateID != null)
                                {
                                    var deliveryDateResult = _deliveryDateService.Get(cartItem.Product.DeliveryDateID.Value);
                                    if (deliveryDateResult.Success)
                                        cartItem.DeliveryDate = deliveryDateResult.Data;
                                }
                                #endregion

                                #region ShippingCost
                                if (cartItem.Product.ShippingCostID != null)
                                {
                                    var shippingCostResult = _shippingCostService.Get(cartItem.Product.ShippingCostID.Value);
                                    if (shippingCostResult.Success)
                                        cartItem.ShippingCost = shippingCostResult.Data;
                                }
                                #endregion

                            }
                        }

                    }

                    return new ShoppingCartItemListModel { AvailableShoppingCartItem = shoppingCartItemModel };
                });
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public JsonResultModel DeleteShoppingCartItem(Guid ProductID)
        {
            try
            {
                if (_workContext.ShoppingID == null)
                    return new JsonResultModel
                    {
                        Success = false,
                        Message = "خطا در حذف از سبد خرید"
                    };

                var deleteShoppingCartIremResult = _shoppingCartItemService.Delete(new DeleteCartItemVM() { ProductID = ProductID, ShoppingID = _workContext.ShoppingID.Value });
                if (!deleteShoppingCartIremResult.Success)
                    return new JsonResultModel
                    {
                        Success = false,
                        Message = "خطا در حذف از سبد خرید"
                    };

                _cacheManager.Remove(CacheParamExtention.Shopping_Cart_Item_Details);
                return new JsonResultModel
                {
                    Success = true,
                    Message = "محصول با موفقیت حذف گردید"
                };
            }
            catch (Exception e)
            {
                return new JsonResultModel
                {
                    Success = false,
                    Message = "خطا"
                };
            }
        }

        public JsonResultModel QuantityMinus(Guid ProductID)
        {
            try
            {
                var productResult = _productService.Get(ProductID);
                if (!productResult.Success)
                    return new JsonResultModel
                    {
                        Success = false,
                        Message = "دوباره امتحان کنید"
                    };
                var product = productResult.Data;

                if (_workContext.ShoppingID == null)
                    return new JsonResultModel
                    {
                        Success = false,
                        Message = "دوباره امتحان کنید"
                    };

                var shoppingCartItemResult = _shoppingCartItemService.Get(_workContext.ShoppingID.Value, product.ID);
                if (shoppingCartItemResult == null)
                    return new JsonResultModel
                    {
                        Success = false,
                        Message = "دوباره امتحان کنید"
                    };
                var shoppingCartitem = shoppingCartItemResult.Data;

                shoppingCartitem.Quantity -= 1;
                if (shoppingCartitem.Quantity > 0)
                {
                    var result = _shoppingCartItemService.Edit(shoppingCartitem);
                    if (!result.Success)
                        return new JsonResultModel
                        {
                            Success = false,
                            Message = "دوباره امتحان کنید"
                        };
                    else
                    {
                        _cacheManager.Remove(CacheParamExtention.Shopping_Cart_Item_Details);
                        return new JsonResultModel
                        {
                            Success = true,
                            Url = "Cart"
                        };
                    }
                }
                else
                    return new JsonResultModel
                    {
                        Success = false,
                        Message = "1"
                    };
            }
            catch (Exception e)
            {
                return new JsonResultModel
                {
                    Success = false,
                    Message = "خطا"
                };
            }
        }

        public JsonResultModel QuantityPlus(Guid ProductID)
        {
            try
            {
                var productResult = _productService.Get(ProductID);
                if (!productResult.Success)
                    return new JsonResultModel
                    {
                        Success = false,
                        Message = "دوباره امتحان کنید"
                    };
                var product = productResult.Data;

                if (_workContext.ShoppingID == null)
                    return new JsonResultModel
                    {
                        Success = false,
                        Message = "دوباره امتحان کنید"
                    };

                var shoppingCartItemResult = _shoppingCartItemService.Get(_workContext.ShoppingID.Value, product.ID);
                if (shoppingCartItemResult == null)
                    return new JsonResultModel
                    {
                        Success = false,
                        Message = "دوباره امتحان کنید"
                    };
                var shoppingCartitem = shoppingCartItemResult.Data;

                shoppingCartitem.Quantity += 1;
                if (shoppingCartitem.Quantity > product.StockQuantity)
                {
                    return new JsonResultModel { Message = "2", Success = false };
                }
                else
                {
                    var result = _shoppingCartItemService.Edit(shoppingCartitem);
                    if (!result.Success)
                        return new JsonResultModel
                        {
                            Success = false,
                            Message = "دوباره امتحان کنید"
                        };
                    else
                    {
                        _cacheManager.Remove(CacheParamExtention.Shopping_Cart_Item_Details);
                        return new JsonResultModel
                        {
                            Success = true,
                            Url = "Cart"
                        };
                    }
                }

            }
            catch (Exception e)
            {
                return new JsonResultModel
                {
                    Success = false,
                    Message = "خطا"
                };
            }
        }
    }
}
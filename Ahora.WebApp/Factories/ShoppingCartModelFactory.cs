using Ahora.WebApp.Models;
using Ahora.WebApp.Models.App;
using FM.Payment.Bank.Melli;
using FM.Portal.Core.Caching;
using FM.Portal.Core.Common;
using FM.Portal.Core.Common.Serializer;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

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
        private readonly IUserAddressService _userAddressService;
        private readonly IBankService _bankService;
        private readonly IPaymentService _paymentService;
        private readonly HttpContextBase _httpContext;

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
                                        , IShippingCostService shippingCostService
                                        , IUserAddressService userAddressService
                                        , IBankService bankService
                                        , IPaymentService paymentService
                                        , HttpContextBase httpContext)
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
            _userAddressService = userAddressService;
            _bankService = bankService;
            _paymentService = paymentService;
            _httpContext = httpContext;
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
                        ShoppingID = _workContext.ShoppingID.Value,
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

        public async Task<JsonResultModel> Payment(UserAddress model)
        {
            try
            {
                if (_workContext.ShoppingID == null || _workContext.User == null)
                    return new JsonResultModel
                    {
                        Success = false,
                        Message = "دوباره امتحان کنید",
                        Url = "Error"
                    };

                var activeBankResult = _bankService.GetActiveBank();
                if (!activeBankResult.Success)
                    return new JsonResultModel
                    {
                        Success = false,
                        Message = "دوباره امتحان کنید",
                        Url = "Error"
                    };

                var activeBank = activeBankResult.Data;
                var shoppingCartDetail = ShoppingCartDetail();
                if (shoppingCartDetail == null)
                    return new JsonResultModel
                    {
                        Success = false,
                        Message = "دوباره امتحان کنید",
                        Url = "Error"
                    };

                var order = new Order();
                var orderDetail = new OrderDetail();
                var payment = new Payment();
                decimal amount = 0;
                var attributeModel = new List<AttributeModel>();
                var productList = new List<Product>();

                foreach (var shoppingCart in shoppingCartDetail.AvailableShoppingCartItem)
                {
                    if (CheckQuantity(shoppingCart))
                    {
                        decimal temp1 = 0;
                        decimal temp2 = 0;
                        decimal attributePrice = 0;

                        var price = shoppingCart.Product.Price;
                        if (shoppingCart.Product.HasDiscount == HasDiscountType.دارای_تخفیف)
                        {
                            if (shoppingCart.Product.DiscountType != DiscountType.نامشخص)
                            {
                                switch (shoppingCart.Product.DiscountType)
                                {
                                    case DiscountType.درصدی:
                                        temp1 = (shoppingCart.Product.Price * shoppingCart.Product.Discount) / 100;
                                        break;
                                    case DiscountType.مبلغی:
                                        temp1 = shoppingCart.Product.Discount;
                                        break;
                                }
                            }
                        }
                        if (shoppingCart.Category != null && shoppingCart.CategoryDiscount != null)
                        {
                            if (shoppingCart.Category.HasDiscountsApplied)
                            {
                                switch (shoppingCart.CategoryDiscount.DiscountType)
                                {
                                    case DiscountType.درصدی:
                                        temp2 = (shoppingCart.Product.Price * shoppingCart.CategoryDiscount.DiscountAmount) / 100;
                                        break;
                                    case DiscountType.مبلغی:
                                        temp2 = shoppingCart.CategoryDiscount.DiscountAmount;
                                        break;
                                }
                            }
                        }
                        if (shoppingCart.AttributeModel != null && shoppingCart.AttributeModel.Count > 0)
                        {
                            attributeModel.AddRange(shoppingCart.AttributeModel);
                            foreach (var attribute in shoppingCart.AttributeModel)
                            {
                                if (attribute.Price > 0)
                                    attributePrice += attribute.Price;
                            }

                        }
                        shoppingCart.Product.CountSelect = shoppingCart.Quantity;

                        amount += attributePrice + (price - (temp1 + temp2)) * shoppingCart.Quantity;
                        productList.Add(new Product
                        {
                            ID = shoppingCart.Product.ID,
                            Name = shoppingCart.Product.Name,
                            Price = shoppingCart.Product.Price,
                            Discount = shoppingCart.Product.Discount,
                            DiscountType = shoppingCart.Product.DiscountType,
                            StockQuantity = shoppingCart.Product.StockQuantity,
                            SpecialOffer = shoppingCart.Product.SpecialOffer,
                            IsDownload = shoppingCart.Product.IsDownload,
                        });
                    }
                    else
                        return new JsonResultModel { Success = false, Message = $"موجودی محصول {shoppingCart.Product.Name} به اتمام رسیده است" };
                }


                var shippingCost = shoppingCartDetail.AvailableShoppingCartItem.Where(x => x.ShippingCost != null).OrderByDescending(x => x.ShippingCost.Priority).Select(x => x.ShippingCost).FirstOrDefault();
                if (shippingCost != null && shippingCost.Price > 0)
                {
                    amount += shippingCost.Price;
                }
                else
                {
                    if (amount < Helper.ShoppingCartRate)
                    {
                        amount += Helper.ShoppingCartRate;
                    }
                }

                if (model.ID == Guid.Empty)
                {
                    var userAddressResult = _userAddressService.Add(model);
                    if (!userAddressResult.Success)
                        return new JsonResultModel
                        {
                            Success = false,
                            Message = "آدرس و کدپستی را وارد نمایید"
                        };
                    order.AddressID = userAddressResult.Data.ID;
                }
                else
                    order.AddressID = model.ID;

                order.SendType = SendType.آنلاین;
                order.ShoppingID = _workContext.ShoppingID.Value;
                order.UserID = _workContext.User.ID;
                order.Price = amount;
                orderDetail.ProductJson = _objectSerializer.Serialize(productList);
                orderDetail.AttributeJson = _objectSerializer.Serialize(attributeModel);
                orderDetail.ShoppingCartJson = _objectSerializer.Serialize(shoppingCartDetail.AvailableShoppingCartItem);
                orderDetail.Quantity = shoppingCartDetail.AvailableShoppingCartItem.Count;
                orderDetail.UserJson = _objectSerializer.Serialize(_workContext.User);

                payment.UserID = _workContext.User.ID;
                payment.Price = amount;

                return await PaymentByBankMelli((long)amount, payment, order, activeBank, orderDetail);
            }
            catch (Exception e)
            {
                return new JsonResultModel
                {
                    Success = false,
                    Message = "خطای ناشناخته ! دوباره امتحان کنید"
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

        public ShoppingCartItemListModel ShoppingCartDetail()
        {
            try
            {
                var cartDetail = CartDetail();
                if (_workContext.User == null || cartDetail.AvailableShoppingCartItem == null)
                    return null;

                var userAddressResult = _userAddressService.List(new UserAddressListVM() { UserID = _workContext.User.ID });
                if (userAddressResult.Success)
                    cartDetail.UserAddress = userAddressResult.Data;

                return cartDetail;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #region Private
        private bool CheckQuantity(ShoppingCartItemModel shoppingCartItem)
        {
            if (shoppingCartItem.Product.StockQuantity > 0 && shoppingCartItem.Product.StockQuantity >= shoppingCartItem.Quantity)
                return true;
            return false;
        }
        #region Bank Melli
        private async Task<JsonResultModel> PaymentByBankMelli(long amount, Payment payment, Order order, Bank bankActive, OrderDetail orderDetail)
        {
            var _bankMelli = new BankMelli();

            var bankResult = await _bankMelli.PaymentRequest((long)amount);
            switch (bankResult.ResCode)
            {
                case "0":
                    {
                        payment.Token = bankResult.Token;
                        order.BankID = bankActive.ID;
                        var firstStep = _paymentService.FirstStepPayment(order, orderDetail, payment);
                        if (!firstStep.Success)
                        {
                            return new JsonResultModel{Success = false,  Url = "Error" , Message="" };
                        }
                        else
                        {
                            var merchantTerminalKeyCookie = new HttpCookie("Data", bankResult.Request);
                            _httpContext.Response.Cookies.Add(merchantTerminalKeyCookie);

                            return new JsonResultModel { Success = true, Url = MelliPurchase(bankResult.Token) };
                        }
                    }
                default:
                    return new JsonResultModel { Success = false, Url = "Error", Message = "خطا در برقراری ارتباط با بانک ملی" };
            }
        }
        private string MelliPurchase(string Token)
        {
            var _bankMelli = new BankMelli();
            return _bankMelli.PaymentPurchase(Token);
        }
        #endregion

        #endregion

    }
}
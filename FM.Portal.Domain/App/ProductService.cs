using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System.Linq;
using FM.Portal.Core.Common;
using FM.Portal.Core.LucenceSearch.Product;

namespace FM.Portal.Domain
{
    public class ProductService : IProductService
    {
        private readonly IProductDataSource _dataSource;
        private readonly IRelatedProductService _relatedProductService;
        private readonly IShippingCostService _shippingCostService;
        private readonly IDeliveryDateService _deliveryDateService;
        public ProductService(IProductDataSource dataSource
                              , IRelatedProductService relatedProductService
                              , IShippingCostService shippingCostService
                              , IDeliveryDateService deliveryDateService)
        {
            _dataSource = dataSource;
            _relatedProductService = relatedProductService;
            _deliveryDateService = deliveryDateService;
            _shippingCostService = shippingCostService;
        }
        public Result<Product> Add(Product model)
        {
            var validate = ValidationModel(model);
            if (!validate.Success)
                return Result<Product>.Failure(message: validate.Message);

            var result = _dataSource.Insert(model);
            if (result.Success)
                LucenceSearch();
            return result;
        }

        public Result<Product> Edit(Product model)
        {
            var validate = ValidationModel(model);
            if (!validate.Success)
                return Result<Product>.Failure(message: validate.Message);

            var result = _dataSource.Update(model);
            if (result.Success)
                LucenceSearch();
            return result;
        }

        public Result<Product> Get(Guid ID)
        {
            var result = _dataSource.Get(ID, null);
            if (!result.Success)
                return result;
            var relatedProducts = _relatedProductService.List(new RelatedProductListVM {ProductID1 = result.Data.ID });
            if (!relatedProducts.Success)
                return Result<Product>.Failure(message:"خطا در بازیابی محصولات مرتبط");

            result.Data.RelatedProducts = relatedProducts.Data;
            if (result.Data.DeliveryDateID != null)
            {
                if (result.Data.DeliveryDateID.Value != Guid.Empty)
                {
                    var deliveryDateResult = _deliveryDateService.Get(result.Data.DeliveryDateID.Value);
                    if (!deliveryDateResult.Success)
                        return Result<Product>.Failure(message: "خطا در بازیابی زمان ارسال محصول");
                    result.Data.DeliveryDate = deliveryDateResult.Data;
                }
            }

            if (result.Data.ShippingCostID != null)
            {
                if (result.Data.ShippingCostID.Value != Guid.Empty)
                {
                    var shippingCostResult = _shippingCostService.Get(result.Data.ShippingCostID.Value);
                    if (!shippingCostResult.Success)
                        return Result<Product>.Failure(message: "خطا در بازیابی هزینه ارسال محصول");
                    result.Data.ShippingCost = shippingCostResult.Data;
                }
            }
            return result;
        }
        public Result<Product> Get(string TrackingCode)
        {
            var result = _dataSource.Get(null, TrackingCode);
            if (!result.Success)
                return result;
            var relatedProducts = _relatedProductService.List(new RelatedProductListVM { ProductID1 = result.Data.ID });
            if (!relatedProducts.Success)
                return Result<Product>.Failure(message: "خطا در بازیابی محصولات مرتبط");

            result.Data.RelatedProducts = relatedProducts.Data;
            if (result.Data.DeliveryDateID != null)
            {
                if(result.Data.DeliveryDateID.Value != Guid.Empty)
                {
                    var deliveryDateResult = _deliveryDateService.Get(result.Data.DeliveryDateID.Value);
                    if (!deliveryDateResult.Success)
                        return Result<Product>.Failure(message: "خطا در بازیابی زمان ارسال محصول");
                    result.Data.DeliveryDate = deliveryDateResult.Data;
                }
            }

            if (result.Data.ShippingCostID != null)
            {
                if(result.Data.ShippingCostID.Value != Guid.Empty)
                {
                    var shippingCostResult = _shippingCostService.Get(result.Data.ShippingCostID.Value);
                    if (!shippingCostResult.Success)
                        return Result<Product>.Failure(message: "خطا در بازیابی هزینه ارسال محصول");
                    result.Data.ShippingCost = shippingCostResult.Data;
                }
            }
            return result;
        }
        public Result<List<Product>> List()
        {
            var table = ConvertDataTableToList.BindList<Product>(_dataSource.List());
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Product>>.Successful(data: table);
            return Result<List<Product>>.Failure();
        }

        public Result<List<ListProductByCategoryIDVM>> List(Guid CategoryID)
        {
            var table = ConvertDataTableToList.BindList<ListProductByCategoryIDVM>(_dataSource.List(CategoryID));
            if (table.Count > 0)
                return Result<List<ListProductByCategoryIDVM>>.Successful(data: table);
            return Result<List<ListProductByCategoryIDVM>>.Failure();
        }

        public Result<List<ListProductShowOnHomePageVM>> ListProductShowOnHomePage(int Count)
        {
            var table = ConvertDataTableToList.BindList<ListProductShowOnHomePageVM>(_dataSource.ListProductShowOnHomePage(Count));
            if (table.Count > 0)
                return Result<List<ListProductShowOnHomePageVM>>.Successful(data: table);
            return Result<List<ListProductShowOnHomePageVM>>.Failure();
        }

        public Result<List<ListAttributeForSelectCustomerVM>> SelectAttributeForCustomer(Guid ProductID)
        {
            var attribute = ConvertDataTableToList.BindList<ListAttributeForSelectCustomerVM>(_dataSource.ListAttributeForProduct(ProductID));

            if (attribute.Count > 0)
            {
                for (int i = 0; i < attribute.Count; i++)
                {
                    var varient = ConvertDataTableToList.BindList<ListSubAttribute>(_dataSource.ListProductVarientAttribute(attribute[i].ID));
                    if (varient.Count > 0)
                        attribute[i].ProductVariantAttributeValue = varient;
                }
            }

            if (attribute.Count > 0 || attribute.Count == 0)
                return Result<List<ListAttributeForSelectCustomerVM>>.Successful(data: attribute);
            return Result<List<ListAttributeForSelectCustomerVM>>.Failure();
        }

        private Result ValidationModel(Product model)
        {
            List<string> Errors = new List<string>();
            if (model.Name == null || model.Name == "" || model.Name == string.Empty)
                Errors.Add("نام محصول را وارد نمایید. ");

            if (model.ShortDescription == null || model.ShortDescription == "" || model.ShortDescription == string.Empty)
                Errors.Add("توضیحات کوتاه محصول را وارد نمایید. ");

            if (model.FullDescription == null || model.FullDescription == "" || model.FullDescription == string.Empty)
                Errors.Add("بررسی تخصصی محصول را وارد نمایید. ");

            if (model.Price == 0)
                Errors.Add("قیمت محصول را وارد نمایید .");
            if (model.Width == 0)
                Errors.Add("عرض محصول را وارد نمایید .");
            if (model.Weight == 0)
                Errors.Add("وزن محصول را وارد نمایید. ");

            if (model.MetaDescription == null || model.MetaDescription == "" || model.MetaDescription == string.Empty)
                Errors.Add("توضیحات متاتگ را وارد نمایید. ");

            if (model.MetaKeywords == null || model.MetaKeywords == "" || model.MetaKeywords == string.Empty)
                Errors.Add("کلید واژه های محصول را وارد نمایید. ");

            if (model.MetaTitle == null || model.MetaTitle == "" || model.MetaTitle == string.Empty)
                Errors.Add("عنوان متاتگ را وارد نمایید. ");

            if (model.Length == 0)
                Errors.Add("طول محصول را وارد نمایید .");

            if (model.Height == 0)
                Errors.Add("ارتفاع محصول را وارد نمایید .");
            if (model.StockQuantity == 0)
                Errors.Add("موجودی محصول را مشخص نمایید");
            if (model.HasDiscount)
            {
                if (model.DiscountType == 0)
                {
                    Errors.Add("نوع تخفیف را مشخص نمایید");
                }
            }
            if (model.DiscountType > 0)
            {
                switch (model.DiscountType)
                {
                    case DiscountType.مبلغی:
                        if (model.Discount == 0)
                            Errors.Add("مبلغ تخفیف را مشخص نمایید");
                        break;
                    case DiscountType.درصدی:
                        if (model.Discount == 0 || model.Discount > 100)
                            Errors.Add("درصد را درست وارد نمایدد");
                        break;
                }
            }
            if (model.ShippingCostID != null && model.ShippingCostID.Value.ToString() == "-1")
                model.ShippingCostID = null;

            if (Errors.Any())
                return Result.Failure(message: string.Join("&&", Errors));

            return Result.Successful();
        }
        private bool LucenceSearch()
        {
            try
            {
                LucenceProductIndexSearch.ClearLuceneIndex();

                foreach (var product in List().Data)
                {
                    LucenceProductIndexSearch.ClearLuceneIndexRecord(product.ID);
                    LucenceProductIndexSearch.AddUpdateLuceneIndex(new Product
                    {
                        ID = product.ID,
                        Name = product.Name,
                        TrackingCode = product.TrackingCode,
                        CategoryName = product.CategoryName

                    });
                }
                return true;
            }
            catch(Exception e) { return false; }
        }
    }
}

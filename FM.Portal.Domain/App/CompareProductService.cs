using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FM.Portal.Domain
{
    public class CompareProductService : ICompareProductService
    {
        private readonly IProductService _productService;
        private readonly HttpContextBase _httpContext;
        private static int CompareProductsNumber = 4;
        public CompareProductService(IProductService productService
                                    , HttpContextBase httpContext)
        {
            _productService = productService;
            _httpContext = httpContext;
        }

        public void AddProductToCompareList(Guid ProductID)
        {
            try
            {
                if (_httpContext?.Response == null)
                    return;
                var compareProducts = GetCountComparedProduct();

                if (!compareProducts.Contains(ProductID))
                    compareProducts.Insert(0, ProductID);

                compareProducts = compareProducts.Take(CompareProductsNumber).ToList();
                AddCompareProductsCookie(compareProducts);

            }
            catch(Exception e) { throw; }
        }

        public void ClearCompareProducts()
        {
            try
            {
                if (_httpContext?.Response == null)
                    return;
                var cookieName = $"{CookieDefaults.Prefix}{CookieDefaults.ComparedProductsCookie}";
                _httpContext.Response.Cookies.Remove(cookieName);
            }
            catch(Exception e) { throw; }
        }

        public Result<List<Product>> GetComparedProducts()
        {
            try
            {
                var products = new List<Product>();
                var compareProducts = GetCountComparedProduct();
                foreach (var ProductID in compareProducts)
                {
                    var productResult = _productService.Get(ProductID);
                    if (productResult.Success)
                        products.Add(productResult.Data);
                }
                return Result<List<Product>>.Successful(data:products);
            }
            catch(Exception e) { return Result<List<Product>>.Failure(); }
        }

        public void RemoveProductFromCompareList(Guid ProductID)
        {
            try
            {
                if (_httpContext.Response == null)
                    return;
                var compareProducts = GetCountComparedProduct();
                if (!compareProducts.Contains(ProductID))
                    return;
                //if exist
                compareProducts.Remove(ProductID);
                //set reload
                AddCompareProductsCookie(compareProducts);
            }
            catch(Exception e) { throw; }
        }

        private List<Guid> GetCountComparedProduct()
        {
            if (_httpContext?.Request == null)
                return new List<Guid>();

            var cookieName = $"{CookieDefaults.Prefix}{CookieDefaults.ComparedProductsCookie}";
            var productsCookie = string.Empty;
            if (_httpContext.Request.Cookies[cookieName] != null)
                productsCookie = _httpContext.Request.Cookies[cookieName].Value;

            if (string.IsNullOrEmpty(productsCookie))
                return new List<Guid>();

            var products = productsCookie.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            return products.Select(Guid.Parse).Distinct().ToList();
        }
        private void AddCompareProductsCookie(IEnumerable<Guid> comparedProductIds)
        {
            var cookieName = $"{CookieDefaults.Prefix}{CookieDefaults.ComparedProductsCookie}";
            _httpContext.Response.Cookies.Remove(cookieName);

            //create cookie 
            var comparedProductIdsCookie = string.Join(",", comparedProductIds);
            _httpContext.Response.Cookies[cookieName].Value = comparedProductIdsCookie;
            _httpContext.Response.Cookies[cookieName].Expires = DateTime.Now.AddYears(50);
        }
    }
}

using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using System;
using System.Web.Mvc;
using FM.Portal.Core.Common;
using System.Linq;
using Ahora.WebApp.Models;
using Ahora.WebApp.Models.App;
using FM.Portal.FrameWork.AutoMapper;
using Ahora.WebApp.Factories;
using FM.Portal.Core.Infrastructure;
using System.Collections.Generic;

namespace Ahora.WebApp.Controllers
{
    public class ProductController : BaseController<IProductService>
    {
        private readonly ICompareProductService _compareProductService;
        private readonly IWorkContext _workContext;
        private readonly IProductCommentService _productCommentService;
        private readonly IProductModelFactory _productModelFactory;
        public ProductController(IProductService service
                                 , ICompareProductService compareProductService
                                 , IWorkContext workContext
                                 , IProductCommentService productCommentService
                                 , IProductModelFactory productModelFactory) : base(service)
        {
            _compareProductService = compareProductService;
            _workContext = workContext;
            _productCommentService = productCommentService;
            _productModelFactory = productModelFactory;
        }

        #region Product
        // GET: Product
        public ActionResult Index(Guid ID)
        {
            var productDetailResult = _productModelFactory.GetProduct(ID);
            if (productDetailResult == null)
                RedirectToRoute("Home");

            SetCookie("ShoppingID");
            return View(productDetailResult);
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

            if (_workContext.User == null)
                return Json(new
                {
                    success = false,
                    message = "ابتدا باید وارد شوید"
                });

            var result = _AddToCart(product.Data, form);
            return result;
        }

        private JsonResult _AddToCart(Product product, FormCollection form)
        {
            try
            {
                var jsonResultModel = new JsonResultModel();
                var productModel = _productModelFactory.GetProduct(product.ID);
                if (productModel.Attributes != null && productModel.Attributes.Count > 0)
                    jsonResultModel = _productModelFactory.AddToCartWithAttribute(productModel, form);
                else
                    jsonResultModel = _productModelFactory.AddToCartWithNotAttribute(productModel);

                return Json(new
                {
                    success = jsonResultModel.Success,
                    message = jsonResultModel.Message,
                    url = !string.IsNullOrEmpty(jsonResultModel.Url) ? Url.RouteUrl(jsonResultModel.Url) : null
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
            {
                var productsModel = new List<ProductModel>();
                foreach (var product in compareProducts)
                {
                    productsModel.Add(_productModelFactory.GetProduct(product.ID));
                }

                return View(productsModel);
            }

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

        #region Product Comment
        public ActionResult Comment(Guid ProductID)
        {
            var productResult = _service.Get(ProductID);
            if (!productResult.Success)
                return Content("");

            var product = productResult.Data;
            if (!product.AllowCustomerReviews)
                return Content("");

            var commentsResult = _productCommentService.List(new ProductCommentListVM() { ProductID = ProductID, ShowChildren = true, CommentType = CommentType.تایید });
            if (!commentsResult.Success)
                return Content("");

            var comments = commentsResult.Data;
            var productCommentListModel = new ProductCommentListModel();
            productCommentListModel.AvailableComments = comments;
            productCommentListModel.User = _workContext.User;
            productCommentListModel.Product = product;

            return PartialView("~/Views/Product/Partial/_PartialComment.cshtml", productCommentListModel);

        }

        [HttpGet]
        public ActionResult AddProductComment(Guid ProductID, Guid? ParentID)
        {
            var errors = new Error();
            errors.Code = 500;
            errors.ErorrDescription = "خطای ناشناخته";

            var productCommentModel = new ProductCommentModel();
            productCommentModel.ProductID = ProductID;

            if (_workContext.User == null)
            {
                errors.Code = 404;
                errors.ErorrDescription = "برای رای دادن باید عضو سایت باشید";
                return PartialView("~/Views/Product/Partial/_PartialError.cshtml", errors);
            }
            var commentsResult = _productCommentService.List(new ProductCommentListVM() { ProductID = ProductID, UserID = _workContext.User.ID, CommentType = CommentType.در_حال_بررسی });

            if (!commentsResult.Success)
                return PartialView("~/Views/Product/Partial/_PartialError.cshtml");

            if (commentsResult.Data.Any())
            {
                errors.Code = 1;
                errors.ErorrDescription = "شما برای این مقاله یک نظر تایید نشده دارید";
                return PartialView("~/Views/Product/Partial/_PartialError.cshtml", errors);
            }
            else
            {
                if (ParentID.HasValue)
                    productCommentModel.ParentID = ParentID;

                return PartialView("~/Views/Product/Partial/_PartialModify.cshtml", productCommentModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProductComment(ProductCommentModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            if (_workContext.User == null)
                return Json(new { show = "true" });

            model.UserID = _workContext.User.ID;

            var result = _productCommentService.Add(model.ToEntity());
            if (!result.Success)
                return Json(new { show = "true" });

            return Json(new { show = "false" });
        }

        public JsonResult Like(Guid CommentID)
        {
            if (_workContext.User == null)
                return Json(new { result = "login", CommentID = CommentID });

            var userCanLikeResult = _productCommentService.UserCanLike(new ProductCommentMapUser() { CommentID = CommentID, UserID = _workContext.User.ID });
            if (!userCanLikeResult.Success)
                return Json(new { result = "duplicate", CommentID = CommentID });

            var LikeResult = _productCommentService.Like(CommentID, _workContext.User.ID);
            if (!LikeResult.Success)
                return Json(new { result = "error", CommentID = CommentID });


            return Json(new { result = "success", CommentID = CommentID, state = "like", count = LikeResult.Data.LikeCount });

        }
        public JsonResult DisLike(Guid CommentID)
        {
            if (_workContext.User == null)
                return Json(new { result = "login", CommentID = CommentID });

            var userCanLikeResult = _productCommentService.UserCanLike(new ProductCommentMapUser() { CommentID = CommentID, UserID = _workContext.User.ID });
            if (!userCanLikeResult.Success)
                return Json(new { result = "duplicate", CommentID = CommentID });

            var disLikeResult = _productCommentService.DisLike(CommentID, _workContext.User.ID);
            if (!disLikeResult.Success)
                return Json(new { result = "error", CommentID = CommentID });


            return Json(new { result = "success", CommentID = CommentID, state = "dislike", count = disLikeResult.Data.DisLikeCount });

        }
        #endregion
    }
}
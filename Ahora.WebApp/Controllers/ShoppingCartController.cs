using Ahora.WebApp.Factories;
using Ahora.WebApp.Models;
using FM.Payment.Bank.Melli;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ahora.WebApp.Controllers
{
    public class ShoppingCartController : BaseController<IShoppingCartItemService>
    {
        private readonly IWorkContext _workContext;
        private readonly IShoppingCartModelFactory _shoppingCartModelFactory;
        public ShoppingCartController(IShoppingCartItemService service
                                     , IWorkContext workContext
                                     , IShoppingCartModelFactory shoppingCartModelFactory) : base(service)
        {
            _workContext = workContext;
            _shoppingCartModelFactory = shoppingCartModelFactory;

        }
        #region Cart
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Delete(Guid ProductID)
        {
            try
            {
                var deleteResult = _shoppingCartModelFactory.DeleteShoppingCartItem(ProductID);
                if (!deleteResult.Success)
                    return Json(deleteResult, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Cart");
            }
            catch { throw; }
        }

        [HttpPost]
        public ActionResult QuantityPlus(Guid ProductID)
        {
            try
            {
                var quantityPlusResult = _shoppingCartModelFactory.QuantityPlus(ProductID);
                if (!quantityPlusResult.Success)
                    return Json(new { error = quantityPlusResult.Message }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Cart");

            }
            catch { throw; }
        }
        public ActionResult Cart()
        {
            try
            {
                if (_workContext.ShoppingID == null)
                    return View("Error", new Error { ClassCss = "alert alert-danger", ErorrDescription = "خطا در بازیابی سبد خرید" });

                var CartDetailResult = _shoppingCartModelFactory.CartDetail();
                if (CartDetailResult == null || CartDetailResult.AvailableShoppingCartItem.Count == 0)
                    return View("~/Views/ShoppingCart/Partial/_PartialCartEmpty.cshtml");

                return PartialView("~/Views/ShoppingCart/Partial/_PartialCart.cshtml", CartDetailResult);
            }
            catch (Exception e) { return View("error"); }
        }
        [HttpPost]
        public ActionResult QuantityMinus(Guid ProductID)
        {
            var quantityMinusResult = _shoppingCartModelFactory.QuantityMinus(ProductID);
            if (!quantityMinusResult.Success)
                return Json(new { error = quantityMinusResult.Message }, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Cart");
        }
        public ActionResult CartEmpty()
        {
            return View();
        }
        #endregion

        #region Shopping
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Shopping()
        {
            try
            {
                if (_workContext.ShoppingID == null)
                    return View("Error", new Error { ClassCss = "alert alert-danger", ErorrDescription = "خطا در بازیابی سبد خرید" });

                var CartDetailResult = _shoppingCartModelFactory.ShoppingCartDetail();
                if (CartDetailResult == null || CartDetailResult.AvailableShoppingCartItem.Count == 0)
                    return View("~/Views/ShoppingCart/Partial/_PartialCartEmpty.cshtml");

                return View(CartDetailResult);
            }
            catch (Exception e) { return View("error"); }

        }

        [HttpPost]
        public async Task<JsonResult> Shopping(UserAddress model)
        {
            var paymentResult = await _shoppingCartModelFactory.Payment(model);
            return Json(paymentResult);
        }

        #endregion
    }
}
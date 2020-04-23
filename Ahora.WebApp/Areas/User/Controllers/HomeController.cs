using System;
using System.Web.Mvc;
using FM.Portal.Core.Common;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Attributes;
using FM.Portal.FrameWork.MVC.Controller;

namespace Ahora.WebApp.Areas.User.Controllers
{
    [UserAuthorizeAttribute(Roles ="User")]
    public class HomeController : BaseController<IPaymentService>
    {
        public HomeController(IPaymentService service) : base(service)
        {
        }

        // GET: User/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Order()
        {
            var userID = SQLHelper.CheckGuidNull(User.Identity.Name);
            var result = _service.ListPaymentForUser(userID);
            result.Data.ForEach(x => {
                x.TrackingCode = x.TrackingCode.Split('-')[1];
                x.Price = x.Price.Split('.')[0];
            }) ;
            return View(result.Data);
        }
        public ActionResult OrderDetail(Guid PaymentID)
        {
            var result = _service.GetDetail(PaymentID);
            return View(result.Data);
        }
        public ActionResult Address()
        {
            return View();
        }
    }
}
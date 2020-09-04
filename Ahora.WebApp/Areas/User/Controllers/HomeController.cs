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
        private readonly ISalesService _salesService;
        public HomeController(IPaymentService service
                             , ISalesService salesService) : base(service)
        {
            _salesService = salesService;
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
            var salesResult = _salesService.Get(null, PaymentID);
            if (!salesResult.Success)
                return View("Error");
            var sales = salesResult.Data;
            var listFlowsResult = _salesService.ListFlow(sales.ID);
            if (!listFlowsResult.Success)
                return View("Error");
            var listFlows = listFlowsResult.Data;

            var detailResult = _service.GetDetail(PaymentID);
            detailResult.Data.SalesFlows = listFlows;
            return View(detailResult.Data);
        }
        public ActionResult Address()
        {
            return View();
        }
    }
}
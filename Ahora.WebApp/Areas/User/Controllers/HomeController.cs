using System;
using System.Linq;
using System.Web.Mvc;
using Ahora.WebApp.Models;
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
        public ActionResult Orders(int? page)
        {
            var userID = SQLHelper.CheckGuidNull(User.Identity.Name);
            var ordersResult = _service.ListPaymentForUser(new FM.Portal.Core.Model.PaymentListForUserVM() {UserID = userID , PageSize = 4 , PageIndex = page });
            if (!ordersResult.Success)
                return View("Error");

            var orders = ordersResult.Data;
            
            orders.ForEach(x => {
                x.TrackingCode = x.TrackingCode.Split('-')[1];
            }) ;

            var pageInfo = new PagingInfo();
            pageInfo.CurrentPage = page ?? 1;
            pageInfo.TotalItems = orders.Select(x=>x.Total).First();
            pageInfo.ItemsPerPage = 4;

            ViewBag.Paging = pageInfo;

            return View(orders);
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
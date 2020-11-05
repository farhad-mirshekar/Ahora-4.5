using System;
using System.IO;
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
        private readonly IPdfService _pdfService;
        private readonly IWorkContext _workContext;
        public HomeController(IPaymentService service
                             , ISalesService salesService
                             , IPdfService pdfService
                             , IWorkContext workContext) : base(service)
        {
            _salesService = salesService;
            _pdfService = pdfService;
            _workContext = workContext;
        }

        // GET: User/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Orders(int? page)
        {
            var ordersResult = _service.ListPaymentForUser(new FM.Portal.Core.Model.PaymentListForUserVM() {UserID = _workContext.User.ID, PageSize = 4 , PageIndex = page });
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

        public ActionResult PrintToPdf(Guid PaymentID)
        {
            var paymentDetailResult = _service.GetDetail(PaymentID);
            byte[] bytes = null;
            using (var stream = new MemoryStream())
            {
                _pdfService.PrintPaymentToPdf(PaymentID, Helper.LanguageID);
                bytes = stream.ToArray();
            }
            return null;
            //return File(bytes, "application/pdf", string.Format("order_{0}.pdf",paymentDetailResult.Data.Payment.OrderID ));
        }
    }
}
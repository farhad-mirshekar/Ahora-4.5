using Ahora.WebApp.Models;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ahora.WebApp.Controllers
{
    public class FaqGroupController : BaseController<IFaqGroupService>
    {
        public FaqGroupController(IFaqGroupService service) : base(service)
        {
        }

        // GET: Faq
        public ActionResult Index(int? page)
        {
            ViewBag.Title = "صفحه پرسش های متداول";
            var faqGrouptsResult = _service.List(new FM.Portal.Core.Model.FaqGroupListVM() { PageSize =5, PageIndex = page });
            if (!faqGrouptsResult.Success)
                return View("Error");
            var faqGroups = faqGrouptsResult.Data;

            var pageInfo = new PagingInfo();
            pageInfo.CurrentPage = page ?? 1;
            pageInfo.TotalItems = faqGroups.Select(x => x.Total).First();
            pageInfo.ItemsPerPage = 5;

            ViewBag.Paging = pageInfo;
            return View(faqGroups);
        }
    }
}
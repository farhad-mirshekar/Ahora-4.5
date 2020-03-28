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
        public ActionResult Index()
        {
            ViewBag.Title = "صفحه پرسش های متداول";
            var model = _service.ListForWeb();
            return View(model.Data);
        }
    }
}
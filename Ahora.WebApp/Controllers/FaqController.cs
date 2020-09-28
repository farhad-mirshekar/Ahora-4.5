using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Ahora.WebApp.Controllers
{
    public class FaqController : BaseController<IFaqService>
    {
        public FaqController(IFaqService service) : base(service)
        {
        }
        public ActionResult Index(Guid ID)
        {
            var faqResult = _service.List(new FM.Portal.Core.Model.FaqListVM() {FAQGroupID = ID });
            if (!faqResult.Success)
                return View("Error");
            var faq = faqResult.Data;

            ViewBag.Title = faq.Select(x => x.FaqGroupTitle).FirstOrDefault();
            return View(faq);

        }
    }
}

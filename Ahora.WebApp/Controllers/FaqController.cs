using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using System;
using System.Web.Mvc;

namespace Ahora.WebApp.Controllers
{
    public class FaqController : BaseController<IFaqService>
    {
        public FaqController(IFaqService service) : base(service)
        {
        }

        public ActionResult Index(Guid id)
        {
            var result = _service.List(id);
            return View(result.Data);
        }
    }
}

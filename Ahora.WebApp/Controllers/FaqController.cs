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
        public ActionResult Index(Guid ID)
        {
            var result = _service.List(ID);
            return View(result.Data);
        }
    }
}

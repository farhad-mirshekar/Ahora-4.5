using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using System.Web.Mvc;
//using FM.Payment.Core.Model;
namespace Ahora.WebApp.Controllers
{
    public class RedirectController : BaseController<IPaymentService>
    {
        private readonly IPaymentService _service;

        public RedirectController(IPaymentService service) : base(service)
        {
            _service = service;
        }

        // GET: Redirect
        public ActionResult Index()
        {
            
            return View();
        }
    }
}
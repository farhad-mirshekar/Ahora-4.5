using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [FM.Portal.FrameWork.Attributes.Authorize.Authorize]
    public class SliderController : Controller
    {
        // GET: Admin/Slider
        public ActionResult Index()
        {
            return View();
        }
    }
}
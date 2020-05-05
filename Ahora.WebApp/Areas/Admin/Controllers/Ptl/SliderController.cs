using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class SliderController : Controller
    {
        // GET: Admin/Slider
        public ActionResult Index()
        {
            return View();
        }
    }
}
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [FM.Portal.FrameWork.Attributes.Authorize.Authorize]
    public class GeneralSettingController : Controller
    {
        // GET: Admin/GeneralSetting
        public ActionResult Index()
        {
            return View();
        }
    }
}
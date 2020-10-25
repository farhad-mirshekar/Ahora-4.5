using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class LanguageController:Controller
    {
        // GET: Admin/Language
        public ActionResult Index()
        {
            return View();
        }
    }
}
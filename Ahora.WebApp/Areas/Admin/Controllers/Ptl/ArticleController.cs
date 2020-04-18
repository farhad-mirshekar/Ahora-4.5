using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [FM.Portal.FrameWork.Attributes.Authorize.Authorize]
    public class ArticleController : Controller
    {
        // GET: Admin/Article
        public ActionResult Index()
        {
            return View();
        }
    }
}
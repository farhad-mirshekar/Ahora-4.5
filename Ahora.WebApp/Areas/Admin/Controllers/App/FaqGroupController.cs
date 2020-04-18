using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [FM.Portal.FrameWork.Attributes.Authorize.Authorize]
    public partial class FaqGroupController : Controller
    {
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}
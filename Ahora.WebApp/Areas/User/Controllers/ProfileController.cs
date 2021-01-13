using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.User.Controllers
{
    public class ProfileController : BaseController<IUserService>
    {
        public ProfileController(IUserService service) : base(service)
        {
        }

        // GET: User/Profile
        public ActionResult Index()
        {
            var userResult = _service.Get(SQLHelper.CheckGuidNull(User.Identity.Name));
            if (!userResult.Success)
                return View("Error");
            var user = userResult.Data;
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(FM.Portal.Core.Model.User model)
        {
            model.Enabled = true;
            var userResult = _service.Edit(model);
            if (!userResult.Success)
                return View("Error");
            var user = userResult.Data;
            return RedirectToAction("Index");
        }
    }
}
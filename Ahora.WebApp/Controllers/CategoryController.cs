using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using System;
using System.Web.Mvc;

namespace Ahora.WebApp.Controllers
{
    public class CategoryController : BaseController<IProductService>
    {
        public CategoryController(IProductService service) : base(service)
        {
        }

        // GET: Category
        public ActionResult Index(Guid ID)
        {
            var result = _service.List(ID);
            if(result.Success)
                return View(result.Data);
            return View("error");
        }
    }
}
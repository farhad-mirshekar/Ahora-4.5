using Ahora.WebApp.Models;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using PagedList;
using System.Web.Mvc;

namespace Ahora.WebApp.Controllers
{
    public class TagController : BaseController<ITagsService>
    {
        public TagController(ITagsService service) : base(service)
        {

        }
        // GET: Tag
        public ActionResult Index(string Name, int? page)
        {
            ViewBag.Title = $"جست و جو برچسب - {Name}";
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            var result =_service.SearchByName(Name);
            if(!result.Success)
            {
                var error = new Error() {ClassCss="alert alert-info" , ErorrDescription="خطا در بازیابی داده" };
                return View("Error", error);
            }

            return View(result.Data.ToPagedList(pageNumber, pageSize));
        }
    }
}
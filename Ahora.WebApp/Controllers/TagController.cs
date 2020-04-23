using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ahora.WebApp.Controllers
{
    public class TagController : BaseController<ITagsService>
    {
        public TagController(ITagsService service) : base(service)
        {

        }
        // GET: Tag
        public ActionResult Index(string Name)
        {
           var result =_service.SearchByName(Name);
            return View();
        }
    }
}
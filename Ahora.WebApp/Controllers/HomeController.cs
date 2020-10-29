using Ahora.WebApp.Models;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using System.Linq;
using System.Web.Mvc;

namespace Ahora.WebApp.Controllers
{
    public partial class HomeController : BaseController<FM.Portal.Core.Service.IProductService>
    {
        private readonly IContactService _contactService;

        public HomeController(IProductService service
                             , IContactService contactService) : base(service)
        {
            _contactService = contactService;
        }

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Title = "صفحه نخست";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Title = "صفحه ارتباط با ما";
            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactVM model)
        {
            if (!ModelState.IsValid)
                return Json(new { Success = false, Message = "پر کردن تمامی فیلد ها الزامی می باشد" });
            else
            {
                var result = _contactService.Add(new FM.Portal.Core.Model.Contact()
                {
                    Description = model.Description,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Phone = model.Phone,
                    Title = model.Title
                });
                if (!result.Success)
                    return Json(new { Success = false, Message = "خطا در ارسال پیام، دوباره تلاش نمایید" });
                else
                    return Json(new { Success = true, Message = "پیام شما با موفقیت ارسال گردید" });
            }

        }
        
    }
}
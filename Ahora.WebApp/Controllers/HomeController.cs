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
        private readonly IAttachmentService _attachmentService;
        private readonly IMenuService _menuService;
        private readonly IArticleService _articleService;
        private readonly INewsService _newsService;
        private readonly ISliderService _sliderService;
        private readonly IEventsService _eventsService;
        private readonly IContactService _contactService;
        public HomeController(IProductService service
                             , IAttachmentService attachmentService
                             , IMenuService menuService
                             , IArticleService articleService
                             , INewsService newsService
                             , ISliderService sliderService
                             , IEventsService eventsService
                             , IContactService contactService) : base(service)
        {
            _attachmentService = attachmentService;
            _menuService = menuService;
            _articleService = articleService;
            _newsService = newsService;
            _sliderService = sliderService;
            _eventsService = eventsService;
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
        #region ChildAction
        [ChildActionOnly]
        public virtual ActionResult TrendingProduct()
        {
            var result = _service.List(new ProductListVM() { SpecialOffer = true });
            return PartialView("_PartialProduct", result.Data.Skip((1 - 1) * Helper.CountShowProduct).Take(Helper.CountShowProduct).ToList());
        }
        [ChildActionOnly]
        public virtual ActionResult SaleProduct()
        {
            var result = _service.List(new ProductListVM() { HasDiscount = true });
            return PartialView("_PartialProduct", result.Data.Skip((1 - 1) * Helper.CountShowProduct).Take(Helper.CountShowProduct).ToList());
        }

        [ChildActionOnly]
        public virtual ActionResult GetLastArticle()
        {
            var result = _articleService.List(new ArticleListVM() { PageSize = Helper.CountShowArticle });
            return PartialView("~/Views/Shared/_PartialSideArticle.cshtml", result.Data);
        }
        [ChildActionOnly]
        public virtual ActionResult GetLastNews()
        {
            var result = _newsService.List(Helper.CountShowNews);
            return PartialView("~/Views/Shared/_PartialSideNews.cshtml", result.Data);
        }
        [ChildActionOnly]
        public ActionResult RenderMenu()
        {
            var result = _menuService.GetMenuForWeb("/1/");
            return PartialView("~/Views/Home/_PartialMenu.cshtml", result.Data);
        }
        [ChildActionOnly]
        public ActionResult RenderSlide()
        {
            var result = _sliderService.List(Helper.CountShowSlider);
            return PartialView("~/Views/Shared/_PartialSlider.cshtml", result.Data);
        }
        [ChildActionOnly]
        public ActionResult GetLastEvents()
        {
            var result = _eventsService.List(new EventsListVM() { PageSize = Helper.CountShowEvents });
            return PartialView("~/Views/Shared/_PartialSideEvents.cshtml", result.Data);
        }
        [ChildActionOnly]
        public ActionResult AutoComplateSearch()
        {
            return PartialView("~/Views/Shared/_PartialAutoComplateSearch.cshtml");
        }
        #endregion
    }
}
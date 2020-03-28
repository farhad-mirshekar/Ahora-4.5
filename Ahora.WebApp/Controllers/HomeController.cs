using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
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
        public HomeController(IProductService service
                             , IAttachmentService attachmentService
                             , IMenuService menuService
                             , IArticleService articleService
                             , INewsService newsService
                             , ISliderService sliderService) : base(service)
        {
            _attachmentService = attachmentService;
            _menuService = menuService;
            _articleService = articleService;
            _newsService = newsService;
            _sliderService = sliderService;
        }
        // GET: Home
        public virtual ActionResult Index()
        {
            ViewBag.Title = "صفحه نخست";
            return View();
        }

        #region ChildAction
        [ChildActionOnly]
        public virtual ActionResult TrendingProduct()
        {
            var result = _service.ListProductShowOnHomePage(10);
            return PartialView("_PartialTrendingProduct", result.Data);
        }
        [ChildActionOnly]
        public virtual ActionResult SaleProduct()
        {
            var result = _service.ListProductShowOnHomePage(10);
            return PartialView("_PartialSaleProduct", result.Data);
        }

        [ChildActionOnly]
        public virtual ActionResult GetLastArticle()
        {
            var result = _articleService.List(4);
            return PartialView("~/Views/Shared/_PartialSideArticle.cshtml", result.Data);
        }
        [ChildActionOnly]
        public virtual ActionResult GetLastNews()
        {
            var result = _newsService.List(4);
            return PartialView("~/Views/Shared/_PartialSideNews.cshtml", result.Data);
        }
        [ChildActionOnly]
        public ActionResult RenderMenu()
        {
            var result = _menuService.GetMenuForWeb("/1/");
            return PartialView("~/Views/Home/_PartialMenu.cshtml", result);
        }
        [ChildActionOnly]
        public ActionResult RenderSlide()
        {
            var result = _sliderService.List(4);
            return PartialView("~/Views/Shared/_PartialCarousel.cshtml", result.Data);
        }
        #endregion
    }
}
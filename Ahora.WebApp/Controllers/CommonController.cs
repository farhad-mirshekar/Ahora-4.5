﻿using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Caching;
using FM.Portal.FrameWork.MVC.Controller;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ahora.WebApp.Controllers
{
    public class CommonController : BaseController<ILanguageService>
    {
        private readonly IWorkContext _workContext;
        private readonly IAttachmentService _attachmentService;
        private readonly IMenuService _menuService;
        private readonly IArticleService _articleService;
        private readonly INewsService _newsService;
        private readonly ISliderService _sliderService;
        private readonly IEventsService _eventsService;
        private readonly IContactService _contactService;
        private readonly ILinkService _linkService;
        private readonly IProductService _Productservice;
        private readonly string ComponentUrl = @"~/Views/Shared/Components/{0}/{1}";
        public CommonController(ILanguageService service
                                , IWorkContext workContext,
                                IProductService Productservice
                             , IAttachmentService attachmentService
                             , IMenuService menuService
                             , IArticleService articleService
                             , INewsService newsService
                             , ISliderService sliderService
                             , IEventsService eventsService
                             , IContactService contactService
                             , ILinkService linkService) : base(service)
        {
            _workContext = workContext;
            _attachmentService = attachmentService;
            _menuService = menuService;
            _articleService = articleService;
            _newsService = newsService;
            _sliderService = sliderService;
            _eventsService = eventsService;
            _contactService = contactService;
            _linkService = linkService;
            _Productservice = Productservice;
        }

        #region Language
        [ChildActionOnly]
        public ActionResult LanguageSelector()
        {
            var languageResult = _service.List(new FM.Portal.Core.Model.LanguageListVM() { PageSize = 5, PageIndex = 1 });
            if (!languageResult.Success)
                return Content("");
            var languageList = languageResult.Data;

            languageList.ForEach(lng =>
            {
                if (_workContext.WorkingLanguage != null)
                {
                    if (lng.ID == _workContext.WorkingLanguage.ID)
                        lng.CurrentLanguage = _workContext.WorkingLanguage;
                }
            });
            return PartialView("_PartialLanguageSelector", languageList);
        }
        public ActionResult ChangeLanguage(Guid LanguageID)
        {
            string redirect = "";
            var languageResult = _service.Get(LanguageID);
            if (!languageResult.Success)
                redirect = Url.RouteUrl("Home");
            var language = languageResult.Data;

            if (language != null)
            {
                _workContext.WorkingLanguage = language;
                language.CurrentLanguage = language;
            }

            redirect = Url.RouteUrl("Home");
            return Redirect(redirect);
        }
        #endregion

        #region ChildAction
        [ChildActionOnly]
        public virtual ActionResult TrendingProduct()
        {
            var result = _Productservice.List(new ProductListVM() { SpecialOffer = true });
            return PartialView(string.Format(ComponentUrl,"Product", "_PartialProduct.cshtml"), result.Data.Skip((1 - 1) * Helper.CountShowProduct).Take(Helper.CountShowProduct).ToList());
        }
        [ChildActionOnly]
        public virtual ActionResult SaleProduct()
        {
            var result = _Productservice.List(new ProductListVM() { HasDiscount = true });
            return PartialView(string.Format(ComponentUrl, "Product" ,"_PartialProduct.cshtml"), result.Data.Skip((1 - 1) * Helper.CountShowProduct).Take(Helper.CountShowProduct).ToList());
        }

        [ChildActionOnly]
        public virtual ActionResult GetLastArticle()
        {
            var result = _articleService.List(new ArticleListVM() { PageSize = Helper.CountShowArticle });
            return PartialView(string.Format(ComponentUrl, "Article", "_PartialSideArticle.cshtml"), result.Data);
        }
        [ChildActionOnly]
        public virtual ActionResult GetLastNews()
        {
            var result = _newsService.List(new NewsListVM() { PageSize = Helper.CountShowNews });
            return PartialView(string.Format(ComponentUrl, "News", "_PartialSideNews.cshtml"), result.Data);
        }
        [ChildActionOnly]
        public ActionResult RenderMenu()
        {
            var result = _menuService.GetMenuForWeb("/1/");
            return PartialView(string.Format(ComponentUrl, "Menu", "_PartialMenu.cshtml"), result.Data);
        }
        [ChildActionOnly]
        public ActionResult RenderSlide()
        {
            var result = _sliderService.List(new SliderListVM() { PageSize = Helper.CountShowSlider });
            return PartialView(string.Format(ComponentUrl, "Slider", "_PartialSlider.cshtml"), result.Data);
        }
        [ChildActionOnly]
        public ActionResult GetLastEvents()
        {
            var result = _eventsService.List(new EventsListVM() { PageSize = Helper.CountShowEvents });
            return PartialView(string.Format(ComponentUrl, "Events", "_PartialSideEvents.cshtml"), result.Data);
        }
        [ChildActionOnly]
        public ActionResult AutoComplateSearch()
        {
            return PartialView(string.Format(ComponentUrl, "AutoComplateSearch", "_PartialAutoComplateSearch.cshtml"));
        }
        [ChildActionOnly]
        public ActionResult RenderFooter()
        {
            var linkResult = _linkService.List(new LinkListVM() { PageIndex = 0 });
            return PartialView(string.Format(ComponentUrl, "Footer", "_PartialFooter.cshtml"), linkResult.Data);
        }
        #endregion
    }
}
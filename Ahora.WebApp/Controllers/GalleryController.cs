using Ahora.WebApp.Models;
using FM.Portal.Core.Common;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using System.Linq;
using System.Web.Mvc;

namespace Ahora.WebApp.Controllers
{
    public class GalleryController : BaseController<IGalleryService>
    {
        private readonly IAttachmentService _attachmentService;
        public GalleryController(IGalleryService service
                                , IAttachmentService attachmentService) : base(service)
        {
            _attachmentService = attachmentService;
        }

        public ActionResult Image(int? page)
        {
            ViewBag.Title = "آلبوم تصاویر";
            var galleriesResult = _service.List(new FM.Portal.Core.Model.GalleryListVM() { PageSize = Helper.CountShowArticle, PageIndex = page });
            if (!galleriesResult.Success)
                return View("Error");
            var galleries = galleriesResult.Data;

            var pageInfo = new PagingInfo();
            pageInfo.CurrentPage = page ?? 1;
            pageInfo.TotalItems = galleries.Select(x => x.Total).First();
            pageInfo.ItemsPerPage = Helper.CountShowArticle;

            ViewBag.Paging = pageInfo;
            return View(galleries);
        }
        public ActionResult ImageDetail(string TrackingCode , string Seo)
        {
            if (string.IsNullOrEmpty(TrackingCode))
                return View("Error", new Error { ClassCss = "Alert Alert-Danger", ErorrDescription = "آلبوم تصویر مورد نظر یافت نشد" });
            var galleryResult = _service.Get(TrackingCode);
            if (!galleryResult.Success)
                return View("Error", new Error { ClassCss = "Alert Alert-Danger", ErorrDescription = "آلبوم تصویر مورد نظر یافت نشد" });
            var gallery = galleryResult.Data;

            var attachmentResult = _attachmentService.List(gallery.ID);
            if (!attachmentResult.Success)
                return View("Error", new Error { ClassCss = "Alert Alert-Danger", ErorrDescription = "آلبوم تصویر مورد نظر یافت نشد" });

            ViewBag.attachments = attachmentResult.Data;
            return View(gallery);
        }
    }
}
using Ahora.WebApp.Models;
using Ahora.WebApp.Models.Pbl;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using PagedList;
using System.Linq;
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
            var tagsResult =_service.List(new TagsListVM {TagName = Name.Replace("_"," ") , PageSize = 5 , PageIndex = page });
            if(!tagsResult.Success)
            {
                var error = new Error() {ClassCss="alert alert-info" , ErorrDescription="خطا در بازیابی داده" };
                return View("Error", error);
            }
            var tags = tagsResult.Data;

            var tagsListModel = new TagsListModel();
            if(tags.Count > 0)
            {
                tagsListModel.AvailableTags = tags;

                var pageInfo = new PagingInfo();
                pageInfo.CurrentPage = page ?? 1;
                pageInfo.TotalItems = tags.Select(x => x.Total).First();
                pageInfo.ItemsPerPage = 5;

                tagsListModel.PagingInfo = pageInfo;
            }

            tagsListModel.TagNameSearch = Name;

            return View(tagsListModel);
        }
    }
}
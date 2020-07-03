using FM.Portal.Core.LucenceSearch.Product;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using FM.Portal.FrameWork.MVC.Helpers.AutoComplate;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;

namespace Ahora.WebApp.Controllers
{
    public class SearchController : BaseController<IProductService>
    {
        public SearchController(IProductService service) : base(service)
        {
        }

        // GET: Search
        [Route("AutoCompleteSearch")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult AutoCompleteSearch(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return Content(string.Empty);

            var items =
               LucenceProductIndexSearch.Search(term, "Name").Take(10).ToList();

            var productList = items.Select(item => new AutoCompleteSearchViewModel
            {
                Name = item.Name,
                Url = "",
                Category = item.DeliveryDateName,
                Image = null,
            }).ToList();

            return Json(productList, JsonRequestBehavior.AllowGet);
        }
    }
}
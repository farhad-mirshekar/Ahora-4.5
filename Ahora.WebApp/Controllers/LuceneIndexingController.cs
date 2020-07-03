using FM.Portal.Core.LucenceSearch.Product;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using System.Web.Mvc;

namespace Ahora.WebApp.Controllers
{
    public class LuceneIndexingController : BaseController<IProductService>
    {
        public LuceneIndexingController(IProductService service) : base(service)
        {
        }

        public ActionResult ReIndex()
        {
            LucenceProductIndexSearch.ClearLuceneIndex();

            foreach (var product in _service.List().Data)
            {
                LucenceProductIndexSearch.ClearLuceneIndexRecord(product.ID);
                LucenceProductIndexSearch.AddUpdateLuceneIndex(new Product
                {
                    ID=product.ID,
                    Name=product.Name
                    
                });
            }
            return Content("ReIndexing Complete.");
        }
    }
}
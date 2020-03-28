using FM.Portal.Core.Service;
using System;
using System.Web.Mvc;

namespace Ahora.WebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IProductService _productService;
        public CategoryController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: Category
        public ActionResult Index(Guid ID)
        {
            var result = _productService.List(ID);
            if(result.Success)
                return View(result.Data);
            return View("error");
        }
    }
}
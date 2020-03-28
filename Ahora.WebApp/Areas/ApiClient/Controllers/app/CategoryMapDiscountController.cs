using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/CategoryMapDiscount")]
    public class CategoryMapDiscountController : BaseApiController<ICategoryMapDiscountService>
    {
        public CategoryMapDiscountController(ICategoryMapDiscountService service) : base(service)
        {
        }
        [Route("Add")]
        public IHttpActionResult Add(CategoryMapDiscount model)
        {
            try
            {
                var result = _service.Add(model);
                return Ok(model);
            }
            catch { return NotFound(); }
        }
    }
}

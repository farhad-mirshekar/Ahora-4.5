using FM.Portal.FrameWork.Api.Controller;
using FM.Portal.Core.Service;
using System.Web.Http;
using System;
using FM.Portal.Core.Model;
namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/ProductAttribute")]
    public class ProductAttributeController : BaseApiController<IProductAttributeService>
    {
        public ProductAttributeController(IProductAttributeService service) : base(service)
        {
        }
        [HttpPost, Route("Get/{ID:guid}")]
        public IHttpActionResult Get(Guid ID)
        {
            try
            {
                var result = _service.Get(ID);
                return Ok(result);
            }
            catch { throw; }
        }
        [HttpPost, Route("List")]
        public IHttpActionResult List()
        {
            try
            {
                var result = _service.List();
                return Ok(result);
            }
            catch { throw; }
        }

        [HttpPost, Route("Add")]
        public IHttpActionResult Add(ProductAttribute model)
        {
            try
            {
                var result = _service.Add(model);
                return Ok(result);
            }
            catch { throw; }
        }
        [HttpPost, Route("Edit")]
        public IHttpActionResult Edit(ProductAttribute model)
        {
            try
            {
                var result = _service.Edit(model);
                return Ok(result);
            }
            catch { throw; }
        }
    }
}

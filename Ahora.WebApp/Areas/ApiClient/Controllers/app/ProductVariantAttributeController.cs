using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/ProductVariantAttribute")]
    public class ProductVariantAttributeController : BaseApiController<IProductVariantAttributeService>
    {
        public ProductVariantAttributeController(IProductVariantAttributeService service) : base(service)
        {
        }
        [HttpPost, Route("Add")]
        public IHttpActionResult Add(ProductVariantAttribute model)
        {
            try
            {
                var result = _service.Add(model);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost, Route("Edit")]
        public IHttpActionResult Edit(ProductVariantAttribute model)
        {
            try
            {
                var result = _service.Edit(model);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost, Route("List/{ProductVariantAttributeID:guid}")]
        public IHttpActionResult List(Guid ProductVariantAttributeID)
        {
            try
            {
                var result = _service.List(ProductVariantAttributeID);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost, Route("Get/{ID:guid}")]
        public IHttpActionResult Get(Guid ID)
        {
            try
            {
                var result = _service.Get(ID);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}

using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/RelatedProduct")]
    public class RelatedProductController : BaseApiController<IRelatedProductService>
    {
        public RelatedProductController(IRelatedProductService service) : base(service)
        {
        }

        [HttpPost, Route("Add")]
        public IHttpActionResult Add(List<RelatedProduct> model)
        {
            try
            {
                foreach (var item in model)
                {
                    _service.Add(item);
                }
                return Ok(new { Success = true });
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost, Route("Edit")]
        public IHttpActionResult Edit(RelatedProduct model)
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

        [HttpPost, Route("List")]
        public IHttpActionResult List(RelatedProductListVM listVM)
        {
            try
            {
                var result = _service.List(listVM);
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
        [HttpPost, Route("Delete/{ID:guid}")]
        public IHttpActionResult Delete(Guid ID)
        {
            try
            {
                var result = _service.Delete(ID);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}
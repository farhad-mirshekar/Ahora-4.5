using FM.Portal.Core.Model.Ptl;
using FM.Portal.Core.Service.Ptl;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/categoryPortal")]
    public class CategoryPortalController : BaseApiController<ICategoryService>
    {
        public CategoryPortalController(ICategoryService service) : base(service)
        {
        }

        [HttpPost, Route("Add")]
        public IHttpActionResult Add(Category model)
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
        public IHttpActionResult Edit(Category model)
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
        public IHttpActionResult List()
        {
            try
            {
                var result = _service.List();
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
        [HttpPost, Route("ListByNode")]
        public IHttpActionResult ListByNode(FM.Portal.Core.Model.NodeVM model)
        {
            try
            {
                var result = _service.ListByNode(model.Node);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}
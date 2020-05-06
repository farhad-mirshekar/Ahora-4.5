using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers.org
{
    [RoutePrefix("api/v1/department")]
    public class DepartmentController : BaseApiController<IDepartmentService>
    {
        public DepartmentController(IDepartmentService service) : base(service)
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
        public IHttpActionResult Add(Department model)
        {
            try
            {
                var result = _service.Add(model);
                return Ok(result);
            }
            catch (Exception e) { return NotFound(); }
        }

        [HttpPost, Route("Edit")]
        public IHttpActionResult Edit(Department model)
        {
            try
            {
                var result = _service.Edit(model);
                return Ok(result);
            }
            catch (Exception e) { return NotFound(); }
        }

        [HttpPost, Route("Delete/{ID:guid}")]
        public IHttpActionResult Delete(Guid ID)
        {
            try
            {
                var result = _service.Delete(ID);
                return Ok(result);
            }
            catch { throw; }
        }
        [HttpPost, Route("ListByNode")]
        public IHttpActionResult ListByNode(NodeVM model)
        {
            try
            {
                var result = _service.ListByNode(model.Node);
                return Ok(result);
            }
            catch { throw; }
        }
    }
}

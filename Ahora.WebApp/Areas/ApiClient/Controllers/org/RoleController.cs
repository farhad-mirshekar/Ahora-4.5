using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/role")]
    public class RoleController : BaseApiController<IRoleService>
    {
        public RoleController(IRoleService service) : base(service)
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
        public IHttpActionResult List(RoleListVM listVM)
        {
            try
            {
                var result = _service.List(listVM);
                return Ok(result);
            }
            catch { throw; }
        }

        [HttpPost, Route("Add")]
        public IHttpActionResult Add(Role model)
        {
            try
            {
                var result = _service.Add(model);
                return Ok(result);
            }
            catch { throw; }
        }
        [HttpPost, Route("Edit")]
        public IHttpActionResult Edit(Role model)
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

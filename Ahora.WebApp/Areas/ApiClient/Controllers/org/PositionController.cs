using FM.Portal.Core.Model;
using FM.Portal.Domain;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/position")]
    public class PositionController : BaseApiController<PositionService>
    {
        public PositionController(PositionService service) : base(service)
        {
        }

        [HttpPost,Route("Add")]
        public IHttpActionResult Add(Position model)
        {
            try
            {
                var result = _service.Add(model);
                return Ok(result);
            }
            catch { return NotFound(); }
        }
        [HttpPost, Route("List")]
        public IHttpActionResult List(PositionListVM model)
        {
            try
            {
                var result = _service.List(model);
                return Ok(result);
            }
            catch { return NotFound(); }
        }
        [HttpPost, Route("SetDefault/{PositionID:guid}")]
        public IHttpActionResult SetDefault(Guid PositionID)
        {
            try
            {
                var result = _service.SetDefault(PositionID);
                return Ok(result);
            }
            catch { return NotFound(); }
        }

        [HttpPost, Route("Get/{ID:guid}")]
        public IHttpActionResult Get(Guid ID)
        {
            try
            {
                var result = _service.Get(ID);
                return Ok(result);
            }
            catch { return NotFound(); }
        }
    }
}

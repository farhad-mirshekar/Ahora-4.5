using FM.Portal.Core.Model;
using FM.Portal.Domain;
using FM.Portal.FrameWork.Api.Controller;
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
    }
}

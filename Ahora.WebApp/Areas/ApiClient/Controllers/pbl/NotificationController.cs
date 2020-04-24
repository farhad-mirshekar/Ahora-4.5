using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/Notification")]
    public class NotificationController : BaseApiController<INotificationService>
    {
        public NotificationController(INotificationService service) : base(service)
        {
        }

        [HttpPost , Route("List")]
        public IHttpActionResult List()
        {
            try
            {
                var result = _service.List();
                return Ok(result);
            }
            catch(Exception e) { return NotFound() ; }
        }
        [HttpPost , Route("ReadNotification/{ID:guid}")]
        public IHttpActionResult ReadNotification(Guid ID)
        {
            try
            {
                var result = _service.ReadNotification(ID);
                return Ok(result);
            }
            catch(Exception e) { return NotFound(); }
        }
        [HttpPost, Route("Get/{ID:guid}")]
        public IHttpActionResult Get(Guid ID)
        {
            try
            {
                var result = _service.Get(ID);
                return Ok(result);
            }
            catch (Exception e) { return NotFound(); }
        }
    }
}
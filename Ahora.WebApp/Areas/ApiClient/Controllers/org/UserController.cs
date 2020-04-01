using FM.Portal.Core.Model;
using FM.Portal.Core.Security;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/user")]
    public class UserController : BaseApiController<IUserService>
    {
        public UserController(IUserService service) : base(service)
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
            catch (Exception e) { return NotFound(); }
        }

        [HttpPost, Route("SearchByNationalCode")]
        public IHttpActionResult SearchByNationalCode(LoginVM model)
        {
            try
            {
                var result = _service.Get(null, null, model.NationalCode);
                return Ok(result);
            }
            catch (Exception e) { return NotFound(); }
        }

        [HttpPost, Route("SetPassword")]
        public IHttpActionResult SetPassword(SetPasswordVM model)
        {
            try
            {
                var result = _service.SetPassword(model);
                return Ok(result);
            }
            catch (Exception e) { return NotFound(); }
        }
    }
}

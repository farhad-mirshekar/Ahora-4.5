using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Web.Http;

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
                var result = _service.Get(null, null, model.NationalCode , UserType.کاربر_درون_سازمانی);
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

        [HttpPost, Route("List")]
        public IHttpActionResult List()
        {
            try
            {
                var result = _service.List();
                return Ok(result);
            }
            catch (Exception e) { return NotFound(); }
        }
        [HttpPost, Route("Add")]
        public IHttpActionResult Add(FM.Portal.Core.Model.User model)
        {
            try
            {
                model.Username = model.NationalCode;
                model.Password = model.NationalCode;
                model.Enabled = true;
                model.Type = UserType.کاربر_درون_سازمانی;
                var result = _service.Add(model);
                return Ok(result);
            }
            catch (Exception e) { return NotFound(); }
        }
        [HttpPost, Route("ResetPassword/{UserID:guid}")]
        public IHttpActionResult ResetPassword(Guid UserID)
        {
            try
            {
                var result = _service.ResetPassword(UserID);
                return Ok(result);
            }
            catch { return NotFound(); }
        }
    }
}

using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using Model = FM.Portal.Core.Model;
using System;
using System.Web.Http;
using FM.Portal.Core;
using System.Collections.Generic;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/user")]
    public class UserController : BaseApiController<IUserService>
    {
        public UserController(IUserService service) : base(service)
        {
        }

        [HttpPost, Route("Get/{ID:guid}")]
        public Result<Model.User> Get(Guid ID)
        => _service.Get(ID);

        [HttpPost, Route("SearchByNationalCode")]
        public Result<Model.User> SearchByNationalCode(Model.LoginVM model)
         => _service.Get(null, null, model.NationalCode, Model.UserType.کاربر_درون_سازمانی);

        [HttpPost, Route("SetPassword")]
        public Result SetPassword(Model.SetPasswordVM model)
         => _service.SetPassword(model);

        [HttpPost, Route("List")]
        public Result<List<Model.User>> List()
         => _service.List();

        [HttpPost, Route("Add")]
        public Result<Model.User> Add(Model.User model)
        {
            model.Username = model.NationalCode;
            model.Password = model.NationalCode;
            model.Enabled = true;
            model.Type = Model.UserType.کاربر_درون_سازمانی;
            return _service.Add(model);
        }

        [HttpPost, Route("ResetPassword/{UserID:guid}")]
        public Result ResetPassword(Guid UserID)
         => _service.ResetPassword(UserID);

        [HttpPost, Route("Edit")]
        public Result<Model.User> Edit(Model.User model)
         => _service.Edit(model);
    }
}

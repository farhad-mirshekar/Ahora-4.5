using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using System.Web.Mvc;
using System;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using FM.Portal.Core.Common;
using FM.Portal.Core.Common.Serializer;

namespace Ahora.WebApp.Controllers
{
    public class AccountController : BaseController<IUserService>
    {
        private readonly IPositionService _positionService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IWorkContext _workContext;
        private readonly IObjectSerializer _objectSerializer;
        public AccountController(IUserService service
                                 , IPositionService positionService
                                 , IAuthenticationService authenticationService
                                 , IWorkContext workContext
                                 , IObjectSerializer objectSerializer) : base(service)
        {
            _positionService = positionService;
            _authenticationService = authenticationService;
            _workContext = workContext;
            _objectSerializer = objectSerializer;
        }

        #region Login / Register / SignOut
        public ActionResult Login(string returnurl)
        {
            TempData["returnUrl"] = "";
            if (returnurl != "" || returnurl != null)
                TempData["returnUrl"] = returnurl;
            ViewBag.Title = "صفحه ورود";
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Login(LoginVM model)
        {
            var returnUrl = "";
            if (TempData["returnUrl"] != null)
                returnUrl = TempData["returnUrl"].ToString() ?? "";
            var result = await GetToken(new Token { username = model.UserName, password = model.Password }, returnUrl);
            return result;
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
                return View(registerVM);
            var user = new User();
            user.CellPhone = registerVM.CellPhone.Trim();
            user.FirstName = registerVM.FirstName.Trim();
            user.LastName = registerVM.LastName.Trim();
            user.NationalCode = registerVM.NationalCode.Trim();
            user.Password = registerVM.Password.Trim();
            user.Username = registerVM.UserName.Trim();
            user.Type = UserType.کاربر_برون_سازمانی;
            user.Enabled = true;

            var result = _service.Add(user);
            if (!result.Success)
                return View(registerVM);
            else
            {
                _authenticationService.SignIn(result.Data, false);
                return RedirectToRoute("Home");
            }
        }

        [Route("SignOut"), HttpPost]
        public ActionResult SignOut(string type)
        {
            _authenticationService.SignOut();
            _workContext.User = null;
            _workContext.IsAdmin = false;
            switch (type)
            {
                case "admin":
                    return Json(new { Success = true, Data = true });
                default:
                    return RedirectToAction("Index", "Home");
            }     
        }
        #endregion

        #region Utilities
        [HttpPost]
        public JsonResult IsAlreadyUserName(string UserName)
        {
            var getUserResult = _service.Get(UserName.Trim(), null, null, UserType.کاربر_درون_سازمانی);
            var user = getUserResult.Data;
            if (user.ID != Guid.Empty)
                return Json(false);
            else
                return Json(true);
        }
        public async Task<JsonResult> RefreshToken(LoginVM model)
        {
            var result = await GetRefreshToken(new Token { grant_type = "refresh_token", RefreshToken = model.RefreshToken });
            return result;
        }

        private async Task<JsonResult> GetToken(Token model, string returnUrl)
        {
            try
            {
                var userResult = _service.Get(model.username, model.password, null, UserType.Unknown);
                if (!userResult.Success)
                    return Json(new { status = 0, token = "" });

                var user = userResult.Data;
                if (user.Type == UserType.کاربر_درون_سازمانی)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri($"{Request.Url.Scheme}://{Request.Url.Authority}");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //setup login data
                        var formContent = new FormUrlEncodedContent(new[]
                        {  new KeyValuePair<string, string>("grant_type", "password"),
                                new KeyValuePair<string, string>("username", model.username),
                                new KeyValuePair<string, string>("password", model.password),
                            });
                        //send request
                        HttpResponseMessage responseMessage = await client.PostAsync("/Token", formContent);
                        var result = await responseMessage.Content.ReadAsStringAsync();
                        var tokenvm = _objectSerializer.DeSerialize<TokenVM>(result);
                        switch (responseMessage.StatusCode)
                        {
                            case System.Net.HttpStatusCode.OK:
                                {
                                    var positionsResult = _positionService.ListByUser(new PositionListVM() { UserID = user.ID });
                                    if (!positionsResult.Success)
                                        return Json(new { status = 0, token = "" });

                                    _authenticationService.SignIn(user, false);
                                    _workContext.User = user;
                                    _workContext.IsAdmin = true;
                                    return Json(new
                                    {
                                        status = 1,
                                        type = 1,
                                        authorizationData = _objectSerializer.Serialize(tokenvm),
                                        currentUserPositions = _objectSerializer.Serialize(positionsResult.Data),
                                        currentUserPosition = _objectSerializer.Serialize(positionsResult.Data.Where(x => x.Default == true).First())
                                    });
                                }
                            default:
                                return Json(new { status = 0, token = "" });
                        }
                    }
                }
                else
                {
                    _authenticationService.SignIn(user, false);
                    _workContext.User = user;
                    _workContext.IsAdmin = false;
                    return Json(new { status = 1, token = "", userid = "", type = user.Type, url = returnUrl });
                }
            }
            catch (Exception e) { throw; }
        }

        private async Task<JsonResult> GetRefreshToken(Token model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{Request.Url.Scheme}://{Request.Url.Authority}");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //setup login data
                var formContent = new FormUrlEncodedContent(new[]
                {
                     new KeyValuePair<string, string>("grant_type", model.grant_type),
                     new KeyValuePair<string, string>("refresh_token", model.RefreshToken),
                });
                //send request
                HttpResponseMessage responseMessage = await client.PostAsync("/Token", formContent);
                var result = await responseMessage.Content.ReadAsStringAsync();
                var tokenvm = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenVM>(result);
                if (tokenvm.UserID != Guid.Empty)
                {
                    var user = _service.Get(tokenvm.UserID);

                    if (user.Data.Type == UserType.کاربر_برون_سازمانی)
                    {
                        _authenticationService.SignIn(user.Data, false);
                    }
                    switch (responseMessage.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK:
                            {
                                if (user.Data.Type == UserType.کاربر_برون_سازمانی)
                                {
                                    return Json(new { status = 1, authorizationData = "", userid = "", type = user.Data.Type });
                                }
                                else if (user.Data.Type == UserType.کاربر_درون_سازمانی)
                                {
                                    return Json(new { status = 0, authorizationData = tokenvm });
                                }
                                return null;
                            }
                        default:
                            return Json(new { status = 0, authorizationData = "" });
                    }
                }
                return Json(new { status = 0, authorizationData = "" });
            }

        }
        #endregion
    }
}
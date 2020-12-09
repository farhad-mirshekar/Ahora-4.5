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
using FM.Portal.Core.Infrastructure;
using FM.Portal.Core.Common.Serializer;
using Ahora.WebApp.Models.Org;
using FM.Portal.FrameWork.AutoMapper;
using FM.Portal.FrameWork.MVC.Helpers.Captcha;

namespace Ahora.WebApp.Controllers
{
    public class AccountController : BaseController<IUserService>
    {
        private readonly IPositionService _positionService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IWorkContext _workContext;
        private readonly IObjectSerializer _objectSerializer;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        public AccountController(IUserService service
                                 , IPositionService positionService
                                 , IAuthenticationService authenticationService
                                 , IWorkContext workContext
                                 , IObjectSerializer objectSerializer
                                 , ILocaleStringResourceService localeStringResourceService) : base(service)
        {
            _positionService = positionService;
            _authenticationService = authenticationService;
            _workContext = workContext;
            _objectSerializer = objectSerializer;
            _localeStringResourceService = localeStringResourceService;
        }

        #region Login / Register / SignOut
        public ActionResult Login(string returnUrl)
        {
            var login = new LoginModel();
            login.ReturnUrl = returnUrl;
            return View(login);
        }
        [HttpPost]
        public async Task<JsonResult> Login(LoginModel login)
        {
            if (login.ShowCaptcha)
            {
                if (!string.IsNullOrEmpty(login.CaptchaText) && string.Equals(login.CaptchaText, Session["tokenCaptcha"].ToString(), StringComparison.CurrentCultureIgnoreCase) == true)
                {
                    var result = await GetToken(login);
                    return result;
                }
                else
                {
                    login.status = false;
                    login.Captcha = new Captcha().Generate("tokenCaptcha");
                    Session["toknCaptcha"] = login.Captcha;
                    login.ErrorText = _localeStringResourceService.GetResource("Account.Login.Captcha.ErrorMessage").Data;
                    login.ShowCaptcha = true;
                    return Json(login);
                }
            }
            else
            {
                var result = await GetToken(login);
                return result;
            }
        }
        public ActionResult Register()
        {
            var register = new RegisterModel();
            register.UserName = null;
            register.Password = null;
            return View(register);
        }
        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
                return View(registerModel);

            var user = registerModel.ToEntity();
            user.Enabled = true;
            user.Type = UserType.کاربر_برون_سازمانی;

            var result = _service.Add(user);
            if (!result.Success)
                return View(registerModel);
            else
            {
                _authenticationService.SignIn(result.Data, false);
                return RedirectToRoute("Home");
            }
        }

        public ActionResult Logout(string type)
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
            if (!getUserResult.Success)
                return Json(true);
            else
                return Json(false);
        }
        [HttpPost]
        public async Task<JsonResult> RefreshToken(LoginVM model)
        {
            var result = await GetRefreshToken(new Token { grant_type = "refresh_token", RefreshToken = model.RefreshToken });
            return result;
        }

        private async Task<JsonResult> GetToken(LoginModel model)
        {
            try
            {
                var userResult = _service.Get(model.Username, model.Password, null, UserType.Unknown);
                if (!userResult.Success)
                {
                    model.status = false;
                    model.ShowCaptcha = true;
                    model.Captcha = new Captcha().Generate("tokenCaptcha");
                    Session["toknCaptcha"] = model.Captcha;
                    model.ErrorText = _localeStringResourceService.GetResource("Account.Login.NotFindUser.ErrorMessage").Data;
                    return Json(model);
                }

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
                                new KeyValuePair<string, string>("username", model.Username),
                                new KeyValuePair<string, string>("password", model.Password),
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

                                    model.status = true;
                                    model.userType = 1;
                                    model.authorizationData = _objectSerializer.Serialize(tokenvm);
                                    model.currentUserPositions = _objectSerializer.Serialize(positionsResult.Data);
                                    model.currentUserPosition = _objectSerializer.Serialize(positionsResult.Data.Where(x => x.Default == true).First());

                                    return Json(model);
                                }
                            default:
                                {
                                    model.status = false;
                                    model.ShowCaptcha = true;
                                    model.Captcha = new Captcha().Generate("tokenCaptcha");
                                    Session["toeknCaptcha"] = model.Captcha;
                                    model.ErrorText = "خطا";
                                    return Json(model);
                                }
                        }
                    }
                }
                else
                {
                    _authenticationService.SignIn(user, false);
                    model.status = true;
                    model.userType = 2;

                    return Json(model);
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
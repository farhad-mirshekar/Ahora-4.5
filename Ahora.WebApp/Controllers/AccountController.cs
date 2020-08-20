using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using Entity = FM.Portal.Core.Model.User;
using System.Web.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Collections.Generic;
using System.Security.Claims;
using FM.Portal.Core.Owin;
using System.Web.Security;
using System.Web;
using System.Linq;
using Newtonsoft.Json;

namespace Ahora.WebApp.Controllers
{
    public class AccountController : BaseController<IUserService>
    {
        private readonly IRequestInfo _requestInfo;
        private readonly IPositionService _positionService;
        public AccountController(IUserService service
                                 , IRequestInfo requestInfo
                                 , IPositionService positionService) : base(service)
        {
            _requestInfo = requestInfo;
            _positionService = positionService;
        }

        // GET: Account
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
                SetAuthCookie(result.Data.ID.ToString(), "User", false);
                Session.RemoveAll();
                Session.Add("login", true);
                Session.Timeout = 30;
                return RedirectToRoute("Home");
            }
        }
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
                var user = _service.Get(model.username, model.password, null, UserType.Unknown);
                if (user.Success && user.Data.ID != Guid.Empty)
                {
                    if (user.Data.Type == UserType.کاربر_درون_سازمانی)
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
                            var tokenvm = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenVM>(result);
                            switch (responseMessage.StatusCode)
                            {
                                case System.Net.HttpStatusCode.OK:
                                    {
                                        var positionsResult = _positionService.ListByUser(new PositionListVM() { UserID = user.Data.ID });
                                        if (!positionsResult.Success)
                                            return Json(new { status = 0, token = "" });

                                        Session.RemoveAll();
                                        SetAuthCookie(user.Data.ID.ToString(), "Admin", false);
                                        Session.Add("login", true);
                                        Session.Timeout = 30;
                                        return Json(new { status = 1 , type = 1, authorizationData = JsonConvert.SerializeObject(tokenvm), currentUserPositions = JsonConvert.SerializeObject(positionsResult.Data), currentUserPosition = JsonConvert.SerializeObject(positionsResult.Data.Where(x => x.Default == true).First()) });
                                    }
                                default:
                                    return Json(new { status = 0, token = "" });
                            }
                        }
                    }
                    else
                    {
                        SetAuthCookie(user.Data.ID.ToString(), "User", false);
                        Session.RemoveAll();
                        Session.Add("login", true);
                        Session.Timeout = 30;
                        return Json(new { status = 1, token = "", userid = "", type = user.Data.Type, url = returnUrl });
                    }
                }
                else
                {
                    return Json(new { status = 0, token = "" });
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
                        SetAuthCookie(user.Data.ID.ToString(), "User", false);
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
                                    return Json(new { status = 0 ,authorizationData = tokenvm });
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
        [Route("SignOut"), HttpPost]
        public ActionResult SignOut(string type)
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            FormsAuthentication.SignOut();

            if (type == "admin")
            {
                return Json(new { Success = true, Data = true });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        #region Authentication
        [NonAction]
        private void SetAuthCookie(string userName, string roleofUser, bool presistantCookie)
        {
            var timeout = presistantCookie ? FormsAuthentication.Timeout.TotalMinutes : 30;

            var now = DateTime.UtcNow.ToLocalTime();
            var expirationTimeSapne = TimeSpan.FromMinutes(timeout);

            var authTicket = new FormsAuthenticationTicket(
                1,
                userName,
                now,
                now.Add(expirationTimeSapne),
                presistantCookie,
                roleofUser,
                FormsAuthentication.FormsCookiePath
                );

            var encryptedTicket = FormsAuthentication.Encrypt(authTicket);

            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true,
                Secure = FormsAuthentication.RequireSSL,
                Path = FormsAuthentication.FormsCookiePath
            };

            if (FormsAuthentication.CookieDomain != null)
            {
                authCookie.Domain = FormsAuthentication.CookieDomain;
            }

            if (presistantCookie)
                authCookie.Expires = DateTime.Now.AddMinutes(timeout);

            Response.Cookies.Add(authCookie);
        }
        #endregion //Authentication
    }
}
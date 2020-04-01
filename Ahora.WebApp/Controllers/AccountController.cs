﻿using FM.Portal.Core.Model;
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

namespace Ahora.WebApp.Controllers
{
    public class AccountController : BaseController<IUserService>
    {
        private readonly IRequestInfo _requestInfo;
        public AccountController(IUserService service
                                 , IRequestInfo requestInfo) : base(service)
        {
            _requestInfo = requestInfo;
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
        public async Task<JsonResult> RefreshToken(LoginVM model)
        {
            var result = await GetRefreshToken(new Token { grant_type = "refresh_token", RefreshToken = model.RefreshToken });
            return result;
        }

        private async Task<JsonResult> GetToken(Token model,string returnUrl)
        {
            var user = _service.Get(model.username, model.password, null);
            if (user.Success)
            {
                if (user.Data.Type == UserType.کاربر_درون_سازمانی)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:61837/");
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
                                    Session.RemoveAll();
                                    Session.Add("login", true);
                                    Session.Timeout = 30;
                                    return Json(new { status = 1, token = tokenvm, userid = user.Data.ID, type = user.Data.Type, authorizationData = tokenvm });
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

        private async Task<JsonResult> GetRefreshToken(Token model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5139/");
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
                                    return Json(new { status = 1, token = "", userid = "", type = user.Data.Type });
                                }
                                else if (user.Data.Type == UserType.کاربر_درون_سازمانی)
                                {
                                    return Json(new { status = 1, token = tokenvm, userid = user.Data.ID, type = user.Data.Type });
                                }
                                return null;
                            }
                        default:
                            return Json(new { status = 0, token = "" });
                    }
                }
                return Json(new { status = 0, token = "" });
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
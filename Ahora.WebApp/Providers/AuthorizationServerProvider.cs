using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using FM.Portal.Core.Model;
using System.Security.Claims;
using FM.Portal.Core.Service;
using Unity;
using FM.Portal.Domain;
using System;
using Microsoft.Owin.Security;
using FM.Portal.DataSource;
using FM.Portal.Infrastructure.DAL;
using System.Collections.Generic;
using FM.Portal.Core.Owin;
using System.Linq;
using FM.Portal.Core.Common;
using FM.Portal.FrameWork.Unity;
using System.Web.Mvc;

namespace FM.Portal.WebApp.Providers
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private IAppSetting _appSetting;
        private IUserService _userService;
        private IPositionService _positionService;
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (_appSetting == null)
            {
                _appSetting = (IAppSetting)DependencyResolver.Current.GetService(typeof(IAppSetting));
            }

            context.Validated();
            context.OwinContext.Set("clientRefreshTokenLifeTime", "1140");
            context.OwinContext.Set("applicationId", _appSetting.ApplicationID.ToString());
            return Task.FromResult(0);
        }
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var applicationId = Guid.Parse(context.OwinContext.Get<string>("applicationId"));

            if (_userService == null)
            {
                _userService = (IUserService)DependencyResolver.Current.GetService(typeof(IUserService));
            }
            if (_positionService == null)
            {
                _positionService = (IPositionService)DependencyResolver.Current.GetService(typeof(IPositionService));
            }
            var data = _userService.Get(context.UserName, context.Password, null, UserType.Unknown);
            if (data.Data.ID != Guid.Empty)
            {
                var positionDefault = _positionService.GetDefaultPosition(data.Data.ID);
                var position = positionDefault.Data;
                var claims = new List<Claim>
            {
                new Claim(type: ClaimTypes.Name, value: data.Data.Username??""),
                new Claim(type: ClaimTypes.NameIdentifier, value: data.Data.ID.ToString()),
                new Claim(type: Claims.ApplicationId, value:applicationId.ToString()),
                new Claim(type: Claims.DepartmentId, value:position.DepartmentID.ToString() ),
                new Claim(type: Claims.PositionId, value:position.ID.ToString()),
                new Claim(type: Claims.UserId, value: position.UserID.ToString()),
                new Claim(type: Claims.UserName, value: data.Data.Username.ToString()),
                new Claim(type: Claims.PositionType, value: position.Type.ToString("d")),
            };

                var identity = new ClaimsIdentity(claims, context.Options.AuthenticationType);


                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                         "as:client_id", context.ClientId ?? string.Empty
                    },
                    {
                        "username", data.Data.Username??""
                    },
                    {
                        "userid", data.Data.ID.ToString()??""
                    }
                });

                var ticket = new AuthenticationTicket(identity, props);
                context.Validated(ticket);
                return Task.FromResult(0);
            }
            else
            {

                return Task.FromResult(0);
            }
        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {

            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            var userId = Guid.Parse(newIdentity.GetUserId());

            if (_userService == null)
            {
                _userService = (IUserService)DependencyResolver.Current.GetService(typeof(IUserService));
            }
            if (_positionService == null)
            {
                _positionService = (IPositionService)DependencyResolver.Current.GetService(typeof(IPositionService));
            }

            var userResult = _userService.Get(userId);
            if (!userResult.Success)
            {
                context.SetError("invalid_user", userResult.Message);
                return Task.FromResult(0);
            }
            var user = userResult.Data;

            var getDefaultPositionResult = _positionService.GetDefaultPosition(userId);
            if (!getDefaultPositionResult.Success)
            {
                context.SetError("invalid_user", getDefaultPositionResult.Message);
                return Task.FromResult(0);
            }
            var positionDefault = getDefaultPositionResult.Data;

            ReplaceClaim(newIdentity, Claims.ApplicationId, positionDefault.ApplicationID.ToString());
            ReplaceClaim(newIdentity, Claims.DepartmentId, positionDefault.DepartmentID.ToString());
            ReplaceClaim(newIdentity, Claims.PositionId, positionDefault.ID.ToString());
            ReplaceClaim(newIdentity, Claims.UserId, positionDefault.UserID.ToString());
            ReplaceClaim(newIdentity, Claims.UserName, user.Username.ToString());
            ReplaceClaim(newIdentity, Claims.PositionType, positionDefault.Type.ToString("d"));
            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);
            return Task.FromResult(0);

            //return Task.FromResult<object>(null);
        }
        void ReplaceClaim(ClaimsIdentity identity, string type, string value)
        {
            var oldClaim = identity.Claims.FirstOrDefault(c => c.Type == type);
            identity.RemoveClaim(oldClaim);
            identity.AddClaim(new Claim(type: type, value: value));
        }
    }
}
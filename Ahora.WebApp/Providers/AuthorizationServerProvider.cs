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

namespace FM.Portal.WebApp.Providers
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            var container = new UnityContainer();
            container.RegisterType<IAppSetting, AppSetting>();
            var _appSetting = container.Resolve<IAppSetting>();

            context.Validated();
            context.OwinContext.Set("clientRefreshTokenLifeTime", "1140");
            context.OwinContext.Set("applicationId", _appSetting.ApplicationID.ToString());
            return Task.FromResult(0);
        }
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var applicationId = Guid.Parse(context.OwinContext.Get<string>("applicationId"));

            var container = new UnityContainer();
            container.RegisterType<IRequestInfo, RequestInfo>();
            container.RegisterType<IUserDataSource, UserDataSource>();
            container.RegisterType<IUserService, UserService>();

            container.RegisterType<IPositionDataSource, PositionDataSource>();
            container.RegisterType<IPositionService, PositionService>();

            IUserService _service = container.Resolve<IUserService>();
            IPositionService _position = container.Resolve<IPositionService>();
            var data = _service.Get(context.UserName, context.Password, null , UserType.Unknown);
            if (data.Data.ID != Guid.Empty)
            {
                var positionDefault = _position.PositionDefault(data.Data.ID);
                var position = positionDefault.Data;
                var claims = new List<Claim>
            {
                new Claim(type: ClaimTypes.Name, value: data.Data.Username??""),
                new Claim(type: ClaimTypes.NameIdentifier, value: data.Data.ID.ToString()),
                new Claim(type: Claims.ApplicationId, value:applicationId.ToString()),
                new Claim(type: Claims.DepartmentId, value:position.DepartmentID.ToString() ),
                new Claim(type: Claims.PositionId, value:position.PositionID.ToString()),
                new Claim(type: Claims.UserId, value: position.UserID.ToString()),
                new Claim(type: Claims.UserName, value: position.UserName.ToString()),
                new Claim(type: Claims.PositionType, value: position.PositionType.ToString()),
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

            var container = new UnityContainer();
            container.RegisterType<IRequestInfo, RequestInfo>();
            container.RegisterType<IPositionDataSource, PositionDataSource>();
            container.RegisterType<IPositionService, PositionService>();
            IPositionService _positionService = container.Resolve<IPositionService>();

            var getDefaultPositionResult = _positionService.PositionDefault(userId);
            if (!getDefaultPositionResult.Success)
            {
                context.SetError("invalid_user", getDefaultPositionResult.Message);
                return Task.FromResult(0);
            }
            var positionDefault = getDefaultPositionResult.Data;

            ReplaceClaim(newIdentity, Claims.ApplicationId, positionDefault.ApplicationID.ToString());
            ReplaceClaim(newIdentity, Claims.DepartmentId, positionDefault.DepartmentID.ToString());
            ReplaceClaim(newIdentity, Claims.PositionId, positionDefault.PositionID.ToString());
            ReplaceClaim(newIdentity, Claims.UserId, positionDefault.UserID.ToString());
            ReplaceClaim(newIdentity, Claims.UserName, positionDefault.UserName.ToString());
            ReplaceClaim(newIdentity, Claims.PositionType, positionDefault.PositionType.ToString());
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
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

namespace FM.Portal.WebApp.Providers
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            context.OwinContext.Set("clientRefreshTokenLifeTime", "1140");
            context.OwinContext.Set("applicationId", "2C321EC4-EEDC-4292-807E-80497E280266");
            context.OwinContext.Set("departmentId", "DBE78DFF-A580-4793-833D-6B5540353D53");
            return Task.FromResult(0);
        }
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var applicationId = Guid.Parse(context.OwinContext.Get<string>("applicationId"));
            var departmentId = Guid.Parse(context.OwinContext.Get<string>("departmentId"));

            var container = new UnityContainer();
            container.RegisterType<IRequestInfo, RequestInfo>();
            container.RegisterType<IUserDataSource, UserDataSource>();
            container.RegisterType<IUserService, UserService>();

            container.RegisterType<IPositionDataSource, PositionDataSource>();
            container.RegisterType<IPositionService, PositionService>();

            IUserService _service = container.Resolve<IUserService>();
            IPositionService _position = container.Resolve<IPositionService>();
            var data = _service.Get(context.UserName, context.Password, null);
            if (data.Data.ID != Guid.Empty)
            {
                var positionDefault = _position.PositionDefault(data.Data.ID);
                var claims = new List<Claim>
            {
                new Claim(type: ClaimTypes.Name, value: data.Data.Username??""),
                new Claim(type: ClaimTypes.NameIdentifier, value: data.Data.ID.ToString()),
                new Claim(type: Claims.ApplicationId, value:applicationId.ToString()),
                new Claim(type: Claims.DepartmentId, value:departmentId.ToString() ),
                new Claim(type: Claims.PositionId, value:positionDefault.Data.PositionID.ToString()),
                new Claim(type: Claims.UserId, value: positionDefault.Data.UserID.ToString()),
                new Claim(type: Claims.UserName, value: positionDefault.Data.UserName.ToString()),
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

            //ReplaceClaim(newIdentity, Claims.ApplicationId, applicationId.ToString());
            //ReplaceClaim(newIdentity, Claims.DepartmentId, defaultPosition.DepartmentID.ToString());
            ReplaceClaim(newIdentity, Claims.PositionId, "35CFD065-748D-435F-A9AA-A40A545DC289");
            ReplaceClaim(newIdentity, Claims.UserId, positionDefault.UserID.ToString());
            ReplaceClaim(newIdentity, Claims.UserName, positionDefault.UserName.ToString());

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);
            return Task.FromResult(0);

            //return Task.FromResult<object>(null);
        }
        private ClaimsIdentity setClaimsIdentity(OAuthGrantResourceOwnerCredentialsContext context, User user)
        {
            var identity = new ClaimsIdentity(authenticationType: "JWT");
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            // custom data
            identity.AddClaim(new Claim(ClaimTypes.UserData, user.ID.ToString()));

            var roles = new[] { "user" };
            foreach (var role in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return identity;
        }
        void ReplaceClaim(ClaimsIdentity identity, string type, string value)
        {
            var oldClaim = identity.Claims.FirstOrDefault(c => c.Type == type);
            identity.RemoveClaim(oldClaim);
            identity.AddClaim(new Claim(type: type, value: value));
        }
    }
}
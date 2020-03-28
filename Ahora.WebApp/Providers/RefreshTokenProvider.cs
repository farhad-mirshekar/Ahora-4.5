using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Unity;
using FM.Portal.Core.Service;
using FM.Portal.Domain;
using FM.Portal.Core.Owin;
using FM.Portal.DataSource;
using FM.Portal.Infrastructure.DAL;

namespace FM.Portal.WebApp.Providers
{
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {
        public void Create(AuthenticationTokenCreateContext context)
        {
            CreateAsync(context).RunSynchronously();
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var container = new UnityContainer();
            container.RegisterType<IRefreshTokenDataSource,RefreshTokenDataSource>();
            container.RegisterType<IRefreshTokenService, RefreshTokenService>();
            IRefreshTokenService _refreshTokenService= container.Resolve<IRefreshTokenService>();

            var refreshTokenLifeTime = context.OwinContext.Get<string>("clientRefreshTokenLifeTime") ?? "60";

            var token = new Core.Model.RefreshToken()
            {
                ID = Guid.NewGuid(),
                UserID = Guid.Parse(context.Ticket.Identity.GetUserId()),
                IssuedDate = DateTime.UtcNow,
                ExpireDate = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
            };

            context.Ticket.Properties.IssuedUtc = token.IssuedDate;
            context.Ticket.Properties.ExpiresUtc = token.ExpireDate;

            token.ProtectedTicket = context.SerializeTicket();

            var result = _refreshTokenService.Add(token);

            if (result.Success)
                context.SetToken(token.ID.ToString("n"));
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            ReceiveAsync(context).RunSynchronously();
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var container = new UnityContainer();
            container.RegisterType<IRefreshTokenDataSource, RefreshTokenDataSource>();
            container.RegisterType<IRefreshTokenService, RefreshTokenService>();
            IRefreshTokenService _refreshTokenService = container.Resolve<IRefreshTokenService>();

            //var allowedOrigin = context.OwinContext.Get<string>("clientAllowedOrigin");
            ////var allowedOrigin = "*";
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
            Guid tokenId = Guid.Parse(context.Token);

            var getResult = _refreshTokenService.Get(tokenId);
            if (getResult.Success && getResult.Data != null && !getResult.Data.Expired)
            {
                context.DeserializeTicket(getResult.Data.ProtectedTicket);
                //await _refreshTokenService.DeleteAsync(tokenId);
            }
        }
    }
}
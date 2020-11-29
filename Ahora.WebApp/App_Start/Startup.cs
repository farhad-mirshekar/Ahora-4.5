using Ahora.WebApp;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(Ahora.Startup))]
namespace Ahora
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureOAuth(app);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var accessTokenExpireTimeSpan = 1;
            if (System.Web.Configuration.WebConfigurationManager.AppSettings["AccessTokenExpireTimeSpan"] != null)
                accessTokenExpireTimeSpan = 1;

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(accessTokenExpireTimeSpan),
                Provider = new FM.Portal.WebApp.Providers.AuthorizationServerProvider(),
                RefreshTokenProvider = new FM.Portal.WebApp.Providers.RefreshTokenProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}
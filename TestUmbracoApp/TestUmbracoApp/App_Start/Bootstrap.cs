using Our.Umbraco.AuthU;
using Our.Umbraco.AuthU.Data;
using Our.Umbraco.AuthU.Services;
using Umbraco.Core;

namespace TestUmbracoApp
{
    public class Boostrap : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase app, ApplicationContext ctx)
        {
            OAuth.ConfigureEndpoint("realm", "/oauth/token", new OAuthOptions
            {
                UserService = new UmbracoMembersOAuthUserService(),
                SymmetricKey = "856FECBA3B06519C8DDDBC80BB080553",
                AccessTokenLifeTime = 20, // Minutes
                ClientStore = new UmbracoDbOAuthClientStore(),
                RefreshTokenStore = new UmbracoDbOAuthRefreshTokenStore(),
                RefreshTokenLifeTime = 1440, // Minutes (1 day)
                AllowedOrigin = "*",
                AllowInsecureHttp = true // During development only
            });
        }
    }
}
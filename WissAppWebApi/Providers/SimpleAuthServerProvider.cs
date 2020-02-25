using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Services.Description;
using AppCore.Services;
using Microsoft.Owin.Security.OAuth;
using WissAppEF.Contexts;
using WissAppEntities.Entities;
using WissAppWebApi.Configs;

namespace WissAppWebApi.Providers
{
    public class SimpleAuthServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            using (var db = new WissAppContext())
            {
                using (var userService = new Service<Users>(db))
                {
                    var user = userService.GetEntity(e =>
                        e.UserName == context.UserName &&
                        e.Password == context.Password &&
                        e.IsActive);

                    if (user != null)
                    {
                        UserConfig.RemoveLoggedOutUser(user.UserName);
                        var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                        identity.AddClaim(new Claim("user", user.UserName));
                        identity.AddClaim(new Claim("Role", user.Roles.Name));
                        context.Validated(identity);
                    }
                    else
                    {
                        context.SetError("invalid_grant", "User name or passwor is incorrect");

                    }
                }
            }
        }

        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            var accesToken = context.AccessToken; //gelen tokenı almaya yarar
            return Task.FromResult<object>(null);
        }
    }
}
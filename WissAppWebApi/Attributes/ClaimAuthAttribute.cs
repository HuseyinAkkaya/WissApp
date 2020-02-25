using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WissAppWebApi.Configs;

namespace WissAppWebApi.Attributes
{
    public class ClaimAuthAttribute : AuthorizationFilterAttribute
    {
        public ClaimAuthAttribute(string claimType, string claimValue)
        {
            ClaimType = claimType;
            ClaimValue = claimValue;
        }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;

            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any<AllowAnonymousAttribute>() || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any<AllowAnonymousAttribute>())
            {
                return Task.FromResult<object>(null);
            }



            if (!principal.Identity.IsAuthenticated)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return Task.FromResult<object>(null);

            }
            if (UserConfig.GetLoggedOutUser().Contains(principal.FindFirst(e => e.Type == "user").Value))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return Task.FromResult<object>(null);
            }

            if (!(principal.HasClaim(e => e.Type.ToLower().Equals(ClaimType.ToLower()) &&
                                          ClaimValue.ToLower().Contains(e.Value.ToLower()))))
            {

                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return Task.FromResult<object>(null);
            }

            return Task.FromResult<object>(null);


        }
    }
}
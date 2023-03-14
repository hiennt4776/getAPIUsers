using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
//using System.Runtime.Caching;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;


namespace getAPIUsers.Authorize
{
    public class BearerAuthorizationAttribute : AuthorizationFilterAttribute
    {
        private static string secretKeyString = "MbQeThWmZq4t7w!z";

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader != null && authHeader.Scheme.Equals("Bearer", StringComparison.OrdinalIgnoreCase)
                 && !string.IsNullOrWhiteSpace(authHeader.Parameter))
            {
                var token = authHeader.Parameter;

                var principal = ValidateToken(token);
                if (principal == null)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                    return;
                }
                Thread.CurrentPrincipal = principal;
                HttpContext.Current.User = principal;
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return;
            }
            base.OnAuthorization(actionContext);
        }

        private static IPrincipal ValidateToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var claimsPrincipal = handler.ValidateToken(token, new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyString)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out var securityToken);


                var jwt = securityToken as JwtSecurityToken;
                var id = new ClaimsIdentity(jwt.Claims, "jwt");
                return new ClaimsPrincipal(id);
            }
            catch (Exception)
            {

                return null;
            }



        }


    }
}

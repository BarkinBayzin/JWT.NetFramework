using JWT.NetFramework.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace JWT.NetFramework.Attributes
{
    public class JwtAuthenticationAttribute : AuthorizationFilterAttribute
    {
        private readonly JwtAuthentication _jwtAuthentication;

        public JwtAuthenticationAttribute()
        {
            _jwtAuthentication = new JwtAuthentication("your_secret_key_here"); // gizli anahtarınızı buraya girin, 16 karakter olması base64 dönüşümü için önemlidir aksi takdirde ArgumentOutOfRange benzeri hatalarla karşılaşırsınız.
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader == null || authHeader.Scheme != "Bearer")
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("Missing or invalid Authorization header")
                };
                return;
            }

            var token = authHeader.Parameter;

            if (!_jwtAuthentication.TryValidateToken(token, out var principal))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("Invalid token")
                };
                return;
            }

            actionContext.RequestContext.Principal = principal;
        }
    }
}
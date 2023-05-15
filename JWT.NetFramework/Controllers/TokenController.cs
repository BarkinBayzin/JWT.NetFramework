using JWT.NetFramework.Attributes;
using JWT.NetFramework.Helpers;
using JWT.NetFramework.Models;
using JWT.NetFramework.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace JWT.NetFramework.Controllers
{
    public class TokenController : ApiController
    {
        private readonly JwtAuthentication _jwtAuthentication;

        public TokenController()
        {
            _jwtAuthentication = new JwtAuthentication("your_secret_key_here");
        }

        [HttpPost]
        public IHttpActionResult CreateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid && AuthService.Authenticate(model.UserName, model.Password))
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.UserName)
            };

                var identity = new ClaimsIdentity(claims, "Jwt");
                var token = _jwtAuthentication.GenerateToken(identity, DateTime.UtcNow.AddMinutes(30));

                return Ok(token);
            }

            return Unauthorized();
        }

        [HttpGet]
        [JwtAuthentication]
        public IHttpActionResult GetProtectedData()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userName = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var token = Request.Headers.Authorization.Parameter; // JWT tokenını al
            return Ok($"Hello, {userName}! Your token is here => {token}");
        }
    }
}

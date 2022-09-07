using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Framework.Core.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        // Dependency Injection
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var tokenClaims = new JwtSecurityToken(token);

                var identity = new ClaimsIdentity(tokenClaims.Claims, "basic");
                context.User = new ClaimsPrincipal(identity);
            }

            await _next.Invoke(context);
        }
    }
}
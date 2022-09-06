using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Framework.Core.Base.ModelEntity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Framework.Core.Base.Controller
{
    public abstract class ProControllerBase : ControllerBase
    {
        private readonly HttpContext _httpContext;

        protected ProControllerBase(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        protected async Task<string> RetrieveAccessTokenAsync()
        {
            return await _httpContext.GetTokenAsync("access_token");
        }

        protected IActionResult BadRequest(string message)
        {
            return new BadRequestObjectResult(new { message, currentDate = DateTime.UtcNow });
        }

        protected IActionResult NotFoundObjectResult()
        {
            var result = new NotFoundObjectResult(new { message = "404 Not Found", currentDate = DateTime.Now });
            return result;
        }

        protected IActionResult ObjectResult(int statusCode)
        {
            var result = new ObjectResult(new { statusCode = statusCode, currentDate = DateTime.Now });
            result.StatusCode = statusCode;

            return result;
        }

        protected IActionResult File(Result<PrometheusResponse> result)
        {
            var data = result.Value.Data as FileContentResult;
            return File(new MemoryStream(data?.FileContents), data?.ContentType, data?.FileDownloadName);
        }

        protected async Task<IActionResult> ExecuteActionAsync(Func<Task<OkObjectResult>> func)
        {
            return await func();
        }

        protected void SetTokenInCookie(string tokenString)
        {
            var handler = new JwtSecurityTokenHandler();
            if (handler.ReadToken(tokenString) is JwtSecurityToken token)
            {
                var tokenExpiryDate = token.ValidTo;

                var cookieOptions = new CookieOptions()
                {
                    Path = "/",
                    HttpOnly = false,
                    Secure = false,
                    IsEssential = true, //<- there
                    Expires = DateTime.Now.AddMonths(1),
                };

                _httpContext.Response.Cookies.Append("token", tokenString, cookieOptions);

            }
        }

        protected string GetTokenFromCookie()
        {
            return _httpContext.Request.Cookies["token"];
        }

        protected string GetIpAddress()
        {
            if (_httpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                return _httpContext.Request.Headers["X-Forwarded-For"];

            return _httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}

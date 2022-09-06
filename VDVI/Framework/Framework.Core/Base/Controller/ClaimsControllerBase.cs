using System.Linq;
using System.Security.Claims;
using CSharpFunctionalExtensions;
using Framework.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Enum;

namespace Framework.Core.Base.Controller
{
    public abstract class ClaimsControllerBase : ControllerBase
    {
        private readonly HttpContext _httpContext;

        protected ClaimsControllerBase(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public Result<int> GetLoggedInUserCompanyId()
        {
            Claim claim = GetClaim(Constants.Constants.Jwt.Claims.CompanyId);
            if (claim == null)
            {
                return Result.Failure<int>("User data not found");
            }

            return Result.Success(int.Parse(claim.Value));
        }

        public Result<RoleEnum> GetLoggedInUserRole()
        {
            Claim claim = GetClaim(ClaimTypes.Role);

            if (claim == null)
            {
                return Result.Failure<RoleEnum>("User role not found");
            }

            return Result.Success(claim.Value.ToEnum<RoleEnum>());
        }

        public Claim GetClaim(string claimType)
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                return identity.Claims.FirstOrDefault(x => x.Type == claimType);
            }

            return null;
        }
    }
}

using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using CSharpFunctionalExtensions;
using Microsoft.IdentityModel.Tokens; 

namespace Framework.Core.Extensions
{
    public static class ClaimsExtension
    {
        public static Result<int> GetCompanyId(this IIdentity identity)
        {
            Claim claim = GetClaim(identity, Constants.Constants.Jwt.Claims.CompanyId);
            if (claim == null)
            {
                return Result.Failure<int>("CompanyId  not found");
            }

            return Result.Success(int.Parse(claim.Value));
        }

        public static Result<int> GetUserId(this IIdentity identity)
        {
            Claim claim = GetClaim(identity, Constants.Constants.Jwt.ClaimIdentifiers.UserId);
            if (claim == null)
            {
                return Result.Failure<int>("User id not found");
            }

            return Result.Success(int.Parse(claim.Value));
        }

        //public static Result<RoleEnum> GetUserRole(this IIdentity identity)
        //{
        //    Claim claim = GetClaim(identity, ClaimTypes.Role);

        //    if (claim == null)
        //    {
        //        return Result.Failure<RoleEnum>("User role not found");
        //    }

        //    return Result.Success(claim.Value.ToEnum<RoleEnum>());
        //}

        public static Claim GetClaim(IIdentity identity, string claimType)
        {
            if (identity is ClaimsIdentity claimsIdentity)
            {
                return claimsIdentity.Claims.FirstOrDefault(x => x.Type == claimType);
            }

            throw new SecurityTokenException("Token has expired. Claims identity not found. User need to logout and login again.");
        }

    }
}

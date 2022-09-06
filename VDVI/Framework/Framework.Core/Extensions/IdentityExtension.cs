using System.Security.Claims;
using System.Threading.Tasks;
using Framework.Core.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Framework.Core.Extensions
{
    public class ApplicationClaimsIdentityFactory : Microsoft.AspNetCore.Identity.UserClaimsPrincipalFactory<AppUser>
    {
        UserManager<AppUser> _userManager;
        public ApplicationClaimsIdentityFactory(UserManager<AppUser> userManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        { }
        public async override Task<ClaimsPrincipal> CreateAsync(AppUser user)
        {
            var principal = await base.CreateAsync(user);

            ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                    new Claim("IsDeveloper", "true")
                });

            return principal;
        }
    }
}

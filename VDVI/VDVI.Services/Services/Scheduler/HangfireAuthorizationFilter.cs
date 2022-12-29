using VDVI.Services.Interfaces.Scheduler;

namespace VDVI.Services.Services.Scheduler
{
    public class HangfireAuthorizationFilter : IHangfireAuthorizationFilter
    {
        private readonly string[] _roles;

        public HangfireAuthorizationFilter(params string[] roles)
        {
            _roles = roles;
        }

        //public bool Authorize(HangfireDb context)
        public bool Authorize()
        {
            ///var httpContext = ((AspNetCoreDashboardContext)context).HttpContext;

            return true;
        }
    }
}

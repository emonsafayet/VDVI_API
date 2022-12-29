namespace VDVI.Services.Interfaces.Scheduler
{
    internal interface IHangfireAuthorizationFilter
    {
        //bool Authorize([NotNull] dbContext context);
        bool Authorize();
    }
}

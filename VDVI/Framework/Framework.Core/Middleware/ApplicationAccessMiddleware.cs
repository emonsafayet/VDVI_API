using Framework.Core.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Middleware
{
    public class ApplicationAccessMiddleware
    {
        private readonly RequestDelegate _next;

        public ApplicationAccessMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
         {

            if (context.User.Identity.Name != null)
            {
                var companyId = context.User.Identity.GetCompanyId().Value;
                var userRole = context.User.Identity.GetUserRole().Value;
                int userId = context.User.Identity.GetUserId().Value;

                LoggedUser loggedUser = new LoggedUser()
                {
                    CompanyId = context.User.Identity.GetCompanyId().Value,
                    LastAccessTime = DateTime.UtcNow,
                    LoggedTime = DateTime.UtcNow,
                    UserId = context.User.Identity.GetUserId().Value,
                    UserName = context.User.Identity.Name
                };


                bool isValidSession = loggedUser.IsValidSession();

                if (!isValidSession) {
                    context.Response.Clear();
                    context.Response.StatusCode = (int)StatusCodes.Status406NotAcceptable;

                    return;
                }
                
            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}

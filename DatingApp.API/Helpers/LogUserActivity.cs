using System;
using System.Security.Claims;
using System.Threading.Tasks;
using DatingApp.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace DatingApp.API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            var userId = int.Parse(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userRepo = resultContext.HttpContext.RequestServices.GetService<IUsersRepository>();

            var user = await userRepo.Get(userId);
            user.LastActive = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
            await userRepo.Update(user);

        }
    }
}
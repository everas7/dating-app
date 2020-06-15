using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helpers
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int getCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x =>
           x.Type == ClaimTypes.NameIdentifier).Value;

            return int.Parse(userId);
        }
    }
}
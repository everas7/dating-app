using System.Linq;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using Domain.Users.Responses;

namespace DatingApp.API.Domain.Users.Mappers
{
    public class LikedResolver : IValueResolver<User, UserListResponse, bool>
    {
        private readonly IUserAccessor _userAccessor;
        public LikedResolver(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }

        public bool Resolve(User source, UserListResponse destination, bool destMember, ResolutionContext context)
        {
            return source.Likers.Any(l => l.LikerId == _userAccessor.getCurrentUserId());
        }
    }
}
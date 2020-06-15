using System.Net;
using System.Threading.Tasks;
using DatingApp.API.Errors;
using DatingApp.API.Services.Interfaces;
using DatingApp.API.Models;
using DatingApp.API.Repositories.Interfaces;
using System;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using AutoMapper;
using Domain.Users.Responses;
using Domain.Users.Requests;
using DatingApp.API.Helpers;
using DatingApp.API.Domain.Users.Requests;

namespace DatingApp.API.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILikesRepository _likesRepo;
        public UsersService(IUsersRepository repo, IMapper mapper, ILikesRepository likesRepo)
        {
            _likesRepo = likesRepo;
            _mapper = mapper;
            _repo = repo;
        }

        public Task Create(User user)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDetailsResponse> Get(string usernameOrId)
        {
            int id = 0;
            Int32.TryParse(usernameOrId, out id);
            User user = null;
            if (id > 0)
            {
                user = await _repo.Get(Int32.Parse(usernameOrId));
            }
            if (user == null)
            {
                user = await _repo.GetByUsername(usernameOrId);
            }
            if (user == null)
                throw new RestException(HttpStatusCode.NotFound, new { User = "Not found" });

            return _mapper.Map<UserDetailsResponse>(user);
        }

        public async Task<PaginatedResponseEnvelope<UserListReponse>> GetAll(RequestForUserList request)
        {
            if (string.IsNullOrEmpty(request.Gender))
            {
                var user = await _repo.Get(request.UserId);
                request.Gender = user.Gender == "male" ? "female" : "male";
            }
            var users = await _repo.GetAll(request);
            var response = _mapper.Map<List<UserListReponse>>(users);
            return new PaginatedResponseEnvelope<UserListReponse>
            {
                Response = response,
                PaginationHeaders = _mapper.Map<PaginationHeaders>(users)
            };
        }

        public async Task Update(int id, UserUpdateRequest userUpdateRequest)
        {
            var userFromRepo = await _repo.Get(id);
            _mapper.Map(userUpdateRequest, userFromRepo);
            await _repo.Update(userFromRepo);
        }

        public async Task LikeUser(int likerId, string likedUsernameOrId)
        {
            int likedId = 0;
            Int32.TryParse(likedUsernameOrId, out likedId);
            User liked = null;
            if (likedId > 0)
            {
                liked = await _repo.Get(likedId);
            }
            if (liked == null)
            {
                liked = await _repo.GetByUsername(likedUsernameOrId);
                likedId = liked.Id;
            }
            if (liked == null)
                throw new RestException(HttpStatusCode.NotFound, new { User = "Not found" });

            var like = await _likesRepo.Get(likerId, likedId);
            if (like != null)
                throw new RestException(HttpStatusCode.BadRequest, new { Like = "You already like this user" });

            

            like = new Like
            {
                LikerId = likerId,
                LikeeId = likedId,
            };
            await _likesRepo.Create(like);
        }
    }
}
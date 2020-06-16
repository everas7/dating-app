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
using DatingApp.API.Domain.Messages.Requests;
using DatingApp.API.Domain.Messages.Responses;

namespace DatingApp.API.Services
{
    public class MessagesService : IMessagesService
    {
        private readonly IMessagesRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        private readonly IUsersRepository _usersRepo;
        public MessagesService(IMessagesRepository repo, IMapper mapper, IUserAccessor userAccessor, IUsersRepository usersRepo)
        {
            _usersRepo = usersRepo;
            _userAccessor = userAccessor;
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<MessageDetailsResponse> Create(int userId, MessageCreationRequest request)
        {
            if (userId != _userAccessor.getCurrentUserId())
                throw new RestException(HttpStatusCode.Forbidden);

            request.SenderId = userId;
            var recipient = await _usersRepo.Get(request.RecipientId);
            if (recipient == null)
                throw new RestException(HttpStatusCode.NotFound, new { Recipient = "Not found" });
            
            var message = _mapper.Map<Message>(request);
            await _repo.Create(message);
            return _mapper.Map<MessageDetailsResponse>(message);
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<MessageDetailsResponse> Get(int id)
        {
            var message = await _repo.Get(id);
            if (message == null)
                throw new RestException(HttpStatusCode.NotFound, new { Message = "Not found" });
            var currentUserId = _userAccessor.getCurrentUserId();

            if (message.RecipientId != currentUserId && message.SenderId != currentUserId)
                throw new RestException(HttpStatusCode.Forbidden);

            return _mapper.Map<MessageDetailsResponse>(message);
        }

        // public async Task<PaginatedResponseEnvelope<UserListResponse>> GetAll(RequestForUserList request)
        // {
        //     if (string.IsNullOrEmpty(request.Gender))
        //     {
        //         var user = await _repo.Get(request.UserId);
        //         request.Gender = user.Gender == "male" ? "female" : "male";
        //     }
        //     var users = await _repo.GetAll(request);
        //     var response = _mapper.Map<List<UserListResponse>>(users);
        //     return new PaginatedResponseEnvelope<UserListResponse>
        //     {
        //         Response = response,
        //         PaginationHeaders = _mapper.Map<PaginationHeaders>(users)
        //     };
        // }

        // public async Task LikeUser(int likerId, string likedUsernameOrId)
        // {
        //     int likedId = 0;
        //     Int32.TryParse(likedUsernameOrId, out likedId);
        //     User liked = null;
        //     if (likedId > 0)
        //     {
        //         liked = await _repo.Get(likedId);
        //     }
        //     if (liked == null)
        //     {
        //         liked = await _repo.GetByUsername(likedUsernameOrId);
        //         likedId = liked.Id;
        //     }
        //     if (liked == null)
        //         throw new RestException(HttpStatusCode.NotFound, new { User = "Not found" });

        //     var like = await _likesRepo.Get(likerId, likedId);
        //     if (like != null)
        //         throw new RestException(HttpStatusCode.BadRequest, new { Like = "You already like this user" });

        //     like = new Like
        //     {
        //         LikerId = likerId,
        //         LikeeId = likedId,
        //     };
        //     await _likesRepo.Create(like);
        // }

        //  public async Task DislikeUser(int likerId, string likedUsernameOrId)
        // {
        //     int likedId = 0;
        //     Int32.TryParse(likedUsernameOrId, out likedId);
        //     User liked = null;
        //     if (likedId > 0)
        //     {
        //         liked = await _repo.Get(likedId);
        //     }
        //     if (liked == null)
        //     {
        //         liked = await _repo.GetByUsername(likedUsernameOrId);
        //         likedId = liked.Id;
        //     }
        //     if (liked == null)
        //         throw new RestException(HttpStatusCode.NotFound, new { User = "Not found" });

        //     var like = await _likesRepo.Get(likerId, likedId);
        //     if (like == null)
        //         throw new RestException(HttpStatusCode.BadRequest, new { Like = "You do not like this user" });

        //     await _likesRepo.Delete(like);
        // }
    }
}
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
            var sender = await _usersRepo.Get(request.SenderId);
            message.Sender = sender;
            await _repo.Create(message);
            return _mapper.Map<MessageDetailsResponse>(message);
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

        public async Task<PaginatedResponseEnvelope<MessageListResponse>> GetMessagesForUser(MessageListRequest request)
        {
            if (request.UserId != _userAccessor.getCurrentUserId())
                throw new RestException(HttpStatusCode.Forbidden);

            var messages = await _repo.GetMessagesForUser(request);
            var response = _mapper.Map<List<MessageListResponse>>(messages);
            return new PaginatedResponseEnvelope<MessageListResponse>
            {
                Response = response,
                PaginationHeaders = _mapper.Map<PaginationHeaders>(messages)
            };
        }

        public async Task<List<MessageListResponse>> GetMessageThread(int userId, int recipientId)
        {
            if (userId != _userAccessor.getCurrentUserId())
                throw new RestException(HttpStatusCode.Forbidden);

            var messages = await _repo.GetMessageThread(userId, recipientId);
            return _mapper.Map<List<MessageListResponse>>(messages);
        }

        public async Task Delete(int id)
        {
            var message = await _repo.Get(id);
            if (message == null)
                throw new RestException(HttpStatusCode.NotFound, new { Message = "Not found" });
            var currentUserId = _userAccessor.getCurrentUserId();

            if (message.RecipientId != currentUserId && message.SenderId != currentUserId)
                throw new RestException(HttpStatusCode.Forbidden);

            if (message.RecipientId == currentUserId)
                message.RecipientDeleted = true;

            if (message.SenderId == currentUserId)
                message.SenderDeleted = true;

            if (message.RecipientDeleted && message.SenderDeleted)
                await _repo.Delete(message);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Domain.Messages.Requests;
using DatingApp.API.Domain.Messages.Responses;
using DatingApp.API.Domain.Users.Requests;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using Domain.Users.Requests;
using Domain.Users.Responses;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Services.Interfaces
{
    public interface IMessagesService
    {
        Task<PaginatedResponseEnvelope<MessageListResponse>> GetMessagesForUser(MessageListRequest request);
        Task<List<MessageListResponse>> GetMessageThread(int userId, int recipientId);
        Task<MessageDetailsResponse> Get(int id);
        Task<MessageDetailsResponse> Create(int userId, MessageCreationRequest request);
        Task Delete(int id);
    }
}
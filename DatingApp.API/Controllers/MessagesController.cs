using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Errors;
using DatingApp.API.Models;
using DatingApp.API.Services.Interfaces;
using Domain.Auth;
using Domain.Auth.Responses;
using Domain.Auth.Requests;
using Domain.Users.Requests;
using Domain.Users.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatingApp.API.Helpers;
using Helpers;
using DatingApp.API.Domain.Users.Requests;
using DatingApp.API.Domain.Messages.Requests;
using DatingApp.API.Domain.Messages.Responses;

namespace DatingApp.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesService _serv;

        public MessagesController(IMessagesService serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public async Task<ActionResult<List<MessageListResponse>>> GetMessagesForUser(int userId, [FromQuery] MessageListRequest request)
        {
            request.UserId = userId;
            var envelope = await _serv.GetMessagesForUser(request);
            Response.AddPaginationHeaders(envelope.PaginationHeaders);
            return envelope.Response;
        }

        [HttpGet("{id}", Name = "GetMessage")]
        public async Task<ActionResult<MessageDetailsResponse>> Get(int userId, int id)
        {
            return await _serv.Get(userId, id);
        }

        [HttpPost]
        public async Task<ActionResult> Create(int userId, MessageCreationRequest request)
        {
            var message = await _serv.Create(userId, request);
            return CreatedAtRoute("GetMessage", new { userId, id = message.Id }, message);
        }

        [HttpGet("thread/{recipientId}")]
        public async Task<ActionResult<List<MessageListResponse>>> GetMessageThread(int userId, int recipientId)
        {
            return await _serv.GetMessageThread(userId, recipientId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int userId, int id)
        {
            await _serv.Delete(userId, id);
            return NoContent();
        }

        [HttpPost("{id}/read")]
        public async Task<ActionResult> MarkAsRead(int userId, int id)
        {
            await _serv.MarkAsRead(userId, id);
            return NoContent();
        }
    }
}

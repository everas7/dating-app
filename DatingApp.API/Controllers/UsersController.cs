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

namespace DatingApp.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _serv;

        public UsersController(IUsersService serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserListReponse>>> Get([FromQuery] RequestForUserList request)
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            request.UserId = id;
            var envelope = await _serv.GetAll(request);
            Response.AddPaginationHeaders(envelope.PaginationHeaders);
            return envelope.Response;
        }

        [HttpGet("self")]
        public async Task<ActionResult<UserDetailsResponse>> CurrentUser()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _serv.Get(id);
        }

        [HttpGet("{usernameOrId}", Name = "GetUser")]
        public async Task<ActionResult<UserDetailsResponse>> GetUser(string usernameOrId)
        {
            return await _serv.Get(usernameOrId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UserUpdateRequest userUpdateRequest)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                throw new RestException(HttpStatusCode.Forbidden);

            await _serv.Update(id, userUpdateRequest);
            return NoContent();
        }
    }
}

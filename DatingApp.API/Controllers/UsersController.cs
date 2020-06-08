using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using DatingApp.API.Services.Interfaces;
using Domain.Auth;
using Domain.Auth.DTOs;
using Domain.Auth.Payloads;
using Domain.Users.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
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
        public async Task<ActionResult<List<UserListReponse>>> Get()
        {
            return await _serv.GetAll();
        }

        [HttpGet("{usernameOrId}")]
        public async Task<ActionResult<UserDetailsResponse>> Get(string usernameOrId)
        {
            return await _serv.Get(usernameOrId);
        }

        // [HttpGet("{username}")]
        // public async Task<ActionResult<User>> GetByUsername(string username)
        // {
        //     return await _serv.GetByUsername(username);
        // }
    }
}

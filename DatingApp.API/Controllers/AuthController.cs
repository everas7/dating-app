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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _serv;

        public AuthController(IAuthService serv)
        {
            _serv = serv;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterUserPayload registerUserPayload)
        {
            return await _serv.Register(registerUserPayload);
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginUserDTO>> Login(LoginUserPayload loginUserPayload)
        {
            return await _serv.Login(loginUserPayload);
        }

    }
}

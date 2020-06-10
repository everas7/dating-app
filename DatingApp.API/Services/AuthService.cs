using System.Net;
using System.Threading.Tasks;
using DatingApp.API.Errors;
using DatingApp.API.Services.Interfaces;
using DatingApp.API.Models;
using DatingApp.API.Repositories.Interfaces;
using Domain.Auth.Requests;
using System;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Domain.Auth.Responses;

namespace DatingApp.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public AuthService(IAuthRepository repo, IConfiguration config, IMapper mapper)
        {
            _mapper = mapper;
            _config = config;
            _repo = repo;
        }

        public async Task<LoginUserResponse> Login(LoginUserRequest loginUserRequest)
        {
            var user = await _repo.Login(loginUserRequest.Username.ToLower(), loginUserRequest.Password);
            if (user == null)
                throw new RestException(HttpStatusCode.Unauthorized);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:TokenKey").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new LoginUserResponse
            {
                Username = user.Username,
                UserId = user.Id,
                AccessToken = tokenHandler.WriteToken(token),
            };
        }

        public async Task<User> Register(RegisterUserRequest registerUserRequest)
        {
            // validate request
            registerUserRequest.Username = registerUserRequest.Username.ToLower();

            if (await _repo.UserExists(registerUserRequest.Username))
                throw new RestException(HttpStatusCode.BadRequest, new { Username = "Username already exists" });

            var userToCreate = new User
            {
                Username = registerUserRequest.Username,
            };

            var createdUser = await _repo.Register(userToCreate, registerUserRequest.Password);
            return createdUser;
        }
    }
}
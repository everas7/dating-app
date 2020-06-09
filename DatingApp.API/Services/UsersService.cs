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

namespace DatingApp.API.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repo;
        private readonly IMapper _mapper;
        public UsersService(IUsersRepository repo, IMapper mapper)
        {
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

        public async Task<List<UserListReponse>> GetAll()
        {
            var users = await _repo.GetAll();
            return _mapper.Map<List<UserListReponse>>(users);
        }

        public async Task Update(int id, UserUpdateRequest userUpdateRequest)
        {
            var userFromRepo = await _repo.Get(id);
            _mapper.Map(userUpdateRequest, userFromRepo);
            await _repo.Update(userFromRepo);
        }
    }
}
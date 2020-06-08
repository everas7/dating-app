using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Domain.Auth.DTOs;
using Domain.Auth.Payloads;
using Domain.Users.Responses;

namespace DatingApp.API.Services.Interfaces
{
    public interface IUsersService
    {
        Task<UserDetailsResponse> Get(string usernameOrId);
        Task<List<UserListReponse>> GetAll();
        Task Create(User user);
        Task Update(User user);
        Task Delete(int id);

    }
}
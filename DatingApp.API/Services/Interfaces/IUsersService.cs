using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Domain.Users.Requests;
using Domain.Users.Responses;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Services.Interfaces
{
    public interface IUsersService
    {
        Task<UserDetailsResponse> Get(string usernameOrId);
        Task<List<UserListReponse>> GetAll();
        Task Create(User user);
        Task Update(int id, UserUpdateRequest user);
        Task Delete(int id);

    }
}
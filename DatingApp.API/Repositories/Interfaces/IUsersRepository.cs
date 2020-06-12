using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Domain.Users.Requests;
using DatingApp.API.Helpers;
using DatingApp.API.Models;

namespace DatingApp.API.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> Get(int id);
        Task<User> GetByUsername(string username);
        Task<PaginatedList<User>> GetAll(RequestForUserList request);
        Task Create(User user);
        Task Update(User user);
        Task Delete(int id);

    }
}
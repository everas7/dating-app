using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> Get(int id);
        Task<User> GetByUsername(string username);
        Task<List<User>> GetAll();
        Task Create(User user);
        Task Update(User user);
        Task Delete(int id);

    }
}
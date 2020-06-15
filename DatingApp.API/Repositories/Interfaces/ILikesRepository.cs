using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Domain.Users.Requests;
using DatingApp.API.Helpers;
using DatingApp.API.Models;

namespace DatingApp.API.Repositories.Interfaces
{
    public interface ILikesRepository
    {
        Task<Like> Get(int likerId, int likeeId);
        Task Create(Like like);
        Task Delete(Like like);
    }
}
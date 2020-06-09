using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Repositories.Interfaces
{
    public interface IPhotosRepository
    {
        Task<Photo> Get(int id);
        Task Create(Photo photo);
        Task Update(Photo photo);
        Task Delete(int id);

    }
}
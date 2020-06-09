using System.Threading.Tasks;
using DatingApp.API.Domain.Photos.Requests;
using DatingApp.API.Domain.Photos.Responses;
using DatingApp.API.Models;

namespace Dating.API.Services.Interfaces
{
    public interface IPhotosService
    {
        Task<PhotoDetailsResponse> Get(int id);
        Task<PhotoCreateResponse> AddUserPhoto(int userId, UserPhotoRequest userPhotoRequest);
        Task SetMain(int userId, int photoId);
        Task Delete(int userId, int photoId);
    }
}
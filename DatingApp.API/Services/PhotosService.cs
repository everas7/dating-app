using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Dating.API.Services.Interfaces;
using DatingApp.API.Domain.Photos.Requests;
using DatingApp.API.Domain.Photos.Responses;
using DatingApp.API.Errors;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using DatingApp.API.Repositories.Interfaces;
using DatingApp.API.Services.Interfaces;

namespace DatingApp.API.Services
{
    public class PhotosService : IPhotosService
    {
        private readonly IPhotosRepository _repo;
        private readonly IPhotoAccessor _photoAccessor;
        private readonly IUsersRepository _usersRepo;
        private readonly IMapper _mapper;
        public PhotosService(IPhotosRepository repo, IPhotoAccessor photoAccessor, IUsersRepository usersRepo, IMapper mapper)
        {
            this._mapper = mapper;
            _repo = repo;
            _photoAccessor = photoAccessor;
            _usersRepo = usersRepo;
        }

        public async Task<PhotoCreateResponse> AddUserPhoto(int userId, UserPhotoRequest userPhotoRequest)
        {
            var photoUploadResult = _photoAccessor.AddPhoto(userPhotoRequest.File);
            var user = await _usersRepo.Get(userId);
            var photo = new Photo();
            _mapper.Map(userPhotoRequest, photo);

            photo.Url = photoUploadResult.Url;
            photo.PublicId = photoUploadResult.PublicId;
            photo.IsMain = !user.Photos.Any(p => p.IsMain);
            photo.User = user;


            await _repo.Create(photo);
            return _mapper.Map<PhotoCreateResponse>(photo);
        }

        public async Task<PhotoDetailsResponse> Get(int id)
        {
            var photo = await _repo.Get(id);
            if (photo == null)
                throw new RestException(HttpStatusCode.NotFound, new { Photo = "Not found" });

            return _mapper.Map<PhotoDetailsResponse>(photo);
        }

        public async Task SetMain(int userId, int photoId)
        {
            var user = await _usersRepo.Get(userId);
            var photo = user.Photos.FirstOrDefault(p => p.Id == photoId);
            if (photo == null)
                throw new RestException(HttpStatusCode.NotFound, new { Photo = "Not found" });

            if (photo.IsMain)
                throw new RestException(HttpStatusCode.BadRequest, new { Photo = "Photo is already main" });

            var mainPhoto = user.Photos.FirstOrDefault(p => p.IsMain);
            mainPhoto.IsMain = false;
            photo.IsMain = true;
            await _repo.Update(mainPhoto);
            await _repo.Update(photo);
        }

        public async Task Delete(int userId, int photoId)
        {
            var user = await _usersRepo.Get(userId);
            var photo = user.Photos.FirstOrDefault(p => p.Id == photoId);
            if (photo == null)
                throw new RestException(HttpStatusCode.NotFound, new { Photo = "Not found" });

            if (photo.IsMain)
                throw new RestException(HttpStatusCode.BadRequest, new { Photo = "You cannot delete your main photo" });

            await _repo.Delete(photo);
        }
    }
}
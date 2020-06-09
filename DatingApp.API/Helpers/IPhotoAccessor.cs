
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helpers
{
  public interface IPhotoAccessor
  {
    PhotoUploadResult AddPhoto(IFormFile file);
    string DeletePhoto(string publicId);
  }
}

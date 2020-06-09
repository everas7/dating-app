using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Dating.API.Services.Interfaces;
using DatingApp.API.Domain.Photos.Requests;
using DatingApp.API.Domain.Photos.Responses;
using DatingApp.API.Errors;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/users/{userId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotosService _serv;
        public PhotosController(IPhotosService serv)
        {
            _serv = serv;
        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<ActionResult<PhotoDetailsResponse>> Get(int id)
        {
            return await _serv.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult> AddUserPhoto(int userId, [FromForm] UserPhotoRequest userPhotoRequest)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                throw new RestException(HttpStatusCode.Forbidden);

            var photoAdded = await _serv.AddUserPhoto(userId, userPhotoRequest);

            return CreatedAtRoute("GetPhoto", new { id = photoAdded.Id, userId = userId }, photoAdded);
        }
    }
}
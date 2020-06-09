using DatingApp.API.Models;
using AutoMapper;
using Domain.Users.Responses;
using System.Linq;
using Helpers;
using Domain.Users.Requests;
using DatingApp.API.Domain.Photos.Requests;
using DatingApp.API.Domain.Photos.Responses;

namespace Domain.Photos.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserPhotoRequest, Photo>();
            CreateMap<Photo, PhotoDetailsResponse>();
            CreateMap<Photo, PhotoCreateResponse>();
        }
    }
}
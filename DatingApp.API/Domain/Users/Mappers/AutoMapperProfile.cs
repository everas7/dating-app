using DatingApp.API.Models;
using AutoMapper;
using Domain.Users.Responses;
using System.Linq;
using Helpers;
using Domain.Users.Requests;

namespace DatingApp.API.Domain.Users.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDetailsResponse>()
                .ForMember(dest => dest.PhotoUrl, opt =>
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(dest => dest.Age, opt =>
                    opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<User, UserListResponse>()
                .ForMember(dest => dest.PhotoUrl, opt =>
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(dest => dest.Age, opt =>
                   opt.MapFrom(src => src.DateOfBirth.CalculateAge()))
                .ForMember(dest => dest.Liked, opt =>
                    opt.MapFrom<LikedResolver>());
            CreateMap<Photo, UserDetailsPhotoResponse>();
            CreateMap<UserUpdateRequest, User>();
        }
    }
}
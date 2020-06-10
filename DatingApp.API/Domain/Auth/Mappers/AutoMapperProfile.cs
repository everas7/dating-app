using DatingApp.API.Models;
using AutoMapper;
using Domain.Auth.Requests;

namespace Domain.Auth.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterUserRequest, User>();
        }
    }
}
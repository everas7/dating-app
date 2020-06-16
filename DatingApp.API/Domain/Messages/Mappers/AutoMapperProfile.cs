using DatingApp.API.Models;
using AutoMapper;
using Domain.Users.Responses;
using System.Linq;
using Helpers;
using Domain.Users.Requests;
using DatingApp.API.Domain.Messages.Requests;
using DatingApp.API.Domain.Messages.Responses;

namespace DatingApp.API.Domain.Messages.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MessageCreationRequest, Message>();
            CreateMap<Message, MessageDetailsResponse>();
        }
    }
}
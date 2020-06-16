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
            CreateMap<Message, MessageDetailsResponse>()
                .ForMember(dest => dest.SenderKnownAs, opt =>
                    opt.MapFrom(src => src.Sender.KnownAs))
                .ForMember(dest => dest.SenderPhotoUrl, opt =>
                    opt.MapFrom(src => src.Sender.Photos.SingleOrDefault(p => p.IsMain).Url))
                .ForMember(dest => dest.RecipientKnownAs, opt =>
                    opt.MapFrom(src => src.Recipient.KnownAs))
                .ForMember(dest => dest.RecipientPhotoUrl, opt =>
                    opt.MapFrom(src => src.Recipient.Photos.SingleOrDefault(p => p.IsMain).Url));
            CreateMap<Message, MessageListResponse>()
                .ForMember(dest => dest.SenderKnownAs, opt =>
                    opt.MapFrom(src => src.Sender.KnownAs))
                .ForMember(dest => dest.SenderPhotoUrl, opt =>
                    opt.MapFrom(src => src.Sender.Photos.SingleOrDefault(p => p.IsMain).Url))
                .ForMember(dest => dest.RecipientKnownAs, opt =>
                    opt.MapFrom(src => src.Recipient.KnownAs))
                .ForMember(dest => dest.RecipientPhotoUrl, opt =>
                    opt.MapFrom(src => src.Recipient.Photos.SingleOrDefault(p => p.IsMain).Url));
        }
    }
}
using AutoMapper;

namespace DatingApp.API.Helpers
{
    public class PaginationHeaders
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
    }

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap(typeof(PaginatedList<>), typeof(PaginationHeaders));
        }
    }
}
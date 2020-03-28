using System.Collections.Generic;
using AutoMapper;
using WebApplicationAPI15_SecondStageTS_.dto;
using WebApplicationAPI15_SecondStageTS_.Models;

namespace WebApplicationAPI15_SecondStageTS_.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Announcement, AnnouncementDTO>();
            CreateMap<Announcement, IEnumerable<AnnouncementDTO>>();
            CreateMap<User, UserDTO>();
            CreateMap<Announcement, AnnouncementDTO>()
                .ForMember(x => x.user,
                    o => o.MapFrom(s => s.user));
        }
    }
}
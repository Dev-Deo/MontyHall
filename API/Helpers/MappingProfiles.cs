using AutoMapper;
using Domain.Entities;
using Domain.Entities.Identity;
using Shared.DTO;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<ApplicationUser, ApplicationUserUpdateDto>()
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName));
        }
    }
}

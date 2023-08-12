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
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName))
                .ForMember(d=> d.ContactNo, o => o.MapFrom(s=> s.ContactNo));

            CreateMap<ApplicationUser, UserAttemptCreateDto>()
                .ForMember(d => d.TotalAttempt, o => o.MapFrom(s => s.TotalAttempt));

            CreateMap<ApplicationUserDto, ApplicationUser>().ReverseMap();

            CreateMap<GameSetupCreateDto, GameSetup>();
            CreateMap<GameSetupUpdateDto, GameSetupDto>();
            CreateMap<GameSetup, GameSetupDto>().ReverseMap();

            CreateMap<GameResultCreateDto, GameResult>();
            CreateMap<GameResultUpdateDto, GameResultDto>();
            CreateMap<GameResult, GameResultDto>().ReverseMap();


        }
    }
}

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

            CreateMap<ApplicationUserDto, ApplicationUser>().ReverseMap();

            CreateMap<GameRequestCreateDto, GameRequest>();
            CreateMap<GameRequestUpdateDto, GameRequestDto>();
            CreateMap<GameRequest, GameRequestDto>().ReverseMap();

            CreateMap<GameSetupCreateDto, GameSetup>();
            CreateMap<GameSetupUpdateDto, GameSetupDto>();
            CreateMap<GameSetup, GameSetupDto>().ReverseMap();

            CreateMap<GameResultCreateDto, GameResult>();
            CreateMap<GameResultUpdateDto, GameResultDto>();
            CreateMap<GameResult, GameResultDto>().ReverseMap();


        }
    }
}

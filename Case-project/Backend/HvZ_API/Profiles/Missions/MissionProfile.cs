using AutoMapper;
using HvZ_API.Models;
using HvZ_API.Models.DTOs.Mission;

namespace HvZ_API.Profiles.Missions
{
    public class MissionProfile : Profile
    {
        public MissionProfile()
        {
            CreateMap<Mission, MissionReadDto>();
            CreateMap<MissionPostDto, Mission>();
            CreateMap<MissionPutDto, Mission>();
        }
    }
}

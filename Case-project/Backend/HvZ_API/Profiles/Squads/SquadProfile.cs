using AutoMapper;
using HvZ_API.Models;
using HvZ_API.Models.DTOs.Squad;

namespace HvZ_API.Profiles.Squads
{
    public class SquadProfile : Profile
    {
        public SquadProfile()
        {
            CreateMap<Squad, SquadReadDto>();


            CreateMap<SquadPutDto, Squad>();
            CreateMap<SquadPostDto, Squad>();
        }
    }
}

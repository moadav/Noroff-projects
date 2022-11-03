using AutoMapper;
using HvZ_API.Models;
using HvZ_API.Models.DTOs.Gravestone;
using HvZ_API.Models.DTOs.Mission;

namespace HvZ_API.Profiles.Gravestones
{
    public class GravestoneProfile : Profile
    {
        public GravestoneProfile()
        {
            CreateMap<Gravestone, GravestoneReadDto>();
            CreateMap<GravestonePostDto, Gravestone>();
            CreateMap<GravestonePutDto, Gravestone>();
            CreateMap<GravestoneKillPostDto, Gravestone>();

        }
    }
}

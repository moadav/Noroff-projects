using AutoMapper;
using HvZ_API.Models;
using HvZ_API.Models.DTOs.Game;
using HvZ_API.Models.DTOs.GameConfig;

namespace HvZ_API.Profiles.GameConfigs
{
    public class GameConfigProfile : Profile
    {
        public GameConfigProfile()
        {
            CreateMap<GameConfig, GameConfigReadDto>();
            CreateMap<GameConfigPostDto, GameConfig>();
            CreateMap<GameConfigPutDto, GameConfig>();
        }
    }
}

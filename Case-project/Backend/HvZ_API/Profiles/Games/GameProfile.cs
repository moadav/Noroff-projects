using AutoMapper;
using HvZ_API.Models;
using HvZ_API.Models.DTOs.CheckIn;
using HvZ_API.Models.DTOs.Game;

namespace HvZ_API.Profiles.Games
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<Game, GameReadDto>();
            CreateMap<GameReadDto,Game>();
            CreateMap<GamePostDto, Game>();
            CreateMap<GamePutDto, Game>();
        }
    }
}

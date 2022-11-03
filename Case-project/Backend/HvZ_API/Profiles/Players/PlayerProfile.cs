using AutoMapper;
using HvZ_API.Models.DTOs.Chat;
using HvZ_API.Models;
using HvZ_API.Models.DTOs.Player;
using HvZ_API.Models.DTOs.CheckIn;

namespace HvZ_API.Profiles.Players
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<Player, PlayerReadDto>();
            
            CreateMap<PlayerPostDto, Player>();
            CreateMap<PlayerPutDto, Player>();

            CreateMap<PlayerCheckinPutDto, Player>();
            CreateMap<Player, PlayerCheckinReadDto>();

        }
    }
}

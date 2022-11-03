using AutoMapper;
using HvZ_API.Models;
using HvZ_API.Models.DTOs.Chat;

namespace HvZ_API.Profiles.Chats
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            CreateMap<Chat, ChatReadDto>();
            CreateMap<ChatPostHumanDto, Chat>();
            CreateMap<ChatPostGlobalDto, Chat>();
            CreateMap<ChatPostSquadDto, Chat>();
            CreateMap<ChatPostZombieDto, Chat>();
            CreateMap<ChatPutDto, Chat>();

        }

    }
}

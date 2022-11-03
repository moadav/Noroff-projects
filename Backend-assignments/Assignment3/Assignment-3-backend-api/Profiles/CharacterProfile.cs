using Assignment_3_backend_api.Models.DTOs.Franchise;
using Assignment_3_backend_api.Models;
using AutoMapper;
using Assignment_3_backend_api.Models.DTOs.Character;

namespace Assignment_3_backend_api.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {

            CreateMap<CharacterPostDto, Character>();
            CreateMap<Character, CharacterReadDto>().ForMember(dto => dto.Movies, opt => opt
                .MapFrom(p => p.Movies.Select(s => s.Id).ToList()))
;
            CreateMap<CharacterPutDto, Character>();

        }
    }
}

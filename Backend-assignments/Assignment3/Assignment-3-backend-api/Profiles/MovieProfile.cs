
using Assignment_3_backend_api.Models;
using Assignment_3_backend_api.Models.DTOs.Movie;
using AutoMapper;

namespace Assignment_3_backend_api.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<MoviePostDto, Movie>();

            CreateMap<MoviePutDto, Movie>();

            CreateMap<Movie, MovieReadDto>().ForMember(dto => dto.Characters, opt => opt
                .MapFrom(p => p.Characters!.Select(s => s.Id).ToList())).ForMember(dto => dto.Franchise, opt => opt
                .MapFrom(p => p.Franchise!.Id));

        }
    }
}

using Assignment_3_backend_api.Models;
using Assignment_3_backend_api.Models.DTOs.Franchise;
using AutoMapper;

namespace Assignment_3_backend_api.Profiles
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<FranchisePostDto, Franchise>();
            CreateMap<FranchisePutDto, Franchise>();
            CreateMap<Franchise, FranchiseReadDto>().ForMember(dto => dto.Movies, opt => opt
                .MapFrom(p => p.Movies.Select(s => s.Id).ToList()));


        }
    }
}

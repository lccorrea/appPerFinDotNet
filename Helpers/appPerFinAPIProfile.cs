using appPerfinAPI.Models;
using appPerfinAPI.Dtos;
using AutoMapper;

namespace appPerfinAPI.Helpers
{
    public class appPerFinAPIProfile : Profile
    {
        public appPerFinAPIProfile()
        {
            CreateMap<Categoria, CategoriaDto>();
                /*.ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
                )
                .ForMember(
                    dest => dest.Idade,
                    opt => opt.MapFrom(src => src.DateNasc.GetCurrentAge())
                );*/
        }
    }
}
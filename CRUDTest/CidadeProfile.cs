using AutoMapper;
using CRUDTest.DTO;
using CRUDTest.Models;

namespace CRUDTest
{
    public class CidadeProfile : Profile
    {
        public CidadeProfile()
        {
            CreateMap<Cidade, CidadeListResponseDto>()
                .ForMember(m => m.Id, map => map.MapFrom(cidade => cidade.Id))
                .ForMember(m => m.Nome, map => map.MapFrom(cidade => cidade.Nome))
                .ForMember(m => m.UF, map => map.MapFrom(cidade => cidade.UF));

            CreateMap<Cidade, CidadeResponseDto>()
                .ForMember(m => m.Id, map => map.MapFrom(cidade => cidade.Id))
                .ForMember(m => m.Nome, map => map.MapFrom(cidade => cidade.Nome))
                .ForMember(m => m.UF, map => map.MapFrom(cidade => cidade.UF));

            CreateMap<Cidade, CidadeRequestDto>()
                .ForMember(m => m.Nome, map => map.MapFrom(cidade => cidade.Nome))
                .ForMember(m => m.UF, map => map.MapFrom(cidade => cidade.UF))
                .ReverseMap();
        }
    }
}

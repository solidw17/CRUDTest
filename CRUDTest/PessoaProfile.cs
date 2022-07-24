using AutoMapper;
using CRUDTest.DTO;
using CRUDTest.Models;

namespace CRUDTest
{
    public class PessoaProfile : Profile
    {
        public PessoaProfile()
        {
            CreateMap<Pessoa, PessoaListResponseDto>()
                .ForMember(m => m.Id, map => map.MapFrom(pessoa => pessoa.Id))
                .ForMember(m => m.Nome, map => map.MapFrom(pessoa => pessoa.Nome))
                .ForMember(m => m.CPF, map => map.MapFrom(pessoa => pessoa.CPF))
                .ForMember(m => m.Idade, map => map.MapFrom(pessoa => pessoa.Idade));

            CreateMap<Pessoa, PessoaResponseDto>()
                .ForMember(m => m.Id, map => map.MapFrom(pessoa => pessoa.Id))
                .ForMember(m => m.Nome, map => map.MapFrom(pessoa => pessoa.Nome))
                .ForMember(m => m.CPF, map => map.MapFrom(pessoa => pessoa.CPF))
                .ForMember(m => m.Idade, map => map.MapFrom(pessoa => pessoa.Idade))
                .ForMember(m => m.Id_Cidade, map => map.MapFrom(pessoa => pessoa.Id_Cidade));

            CreateMap<Pessoa, PessoaRequestDto>()
                .ForMember(m => m.Nome, map => map.MapFrom(pessoa => pessoa.Nome))
                .ForMember(m => m.CPF, map => map.MapFrom(pessoa => pessoa.CPF))
                .ForMember(m => m.Idade, map => map.MapFrom(pessoa => pessoa.Idade))
                .ForMember(m => m.Id_Cidade, map => map.MapFrom(pessoa => pessoa.Id_Cidade))
                .ReverseMap();
        }
    }
}

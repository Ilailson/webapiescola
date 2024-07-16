using AutoMapper;
using SmartSchool.Controllers.Models;
using SmartSchool.WebAPI.Helpers;
using SmartSchool.WebAPI.v2.Dtos;


namespace SmartSchool.WebAPI.v2.Profiles
{
    public class SmartShoolProfile : Profile
    {
        public SmartShoolProfile()
        {
            CreateMap<Aluno, AlunoDto>()
                .ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
                )
                .ForMember(
                    dest => dest.Idade,
                    opt => opt.MapFrom(src => src.DataNasc.GetCurrentAge())
                );

            CreateMap<AlunoDto, Aluno>();
            CreateMap<Aluno, AlunoRegistrarDto>().ReverseMap();
        }
    }
}
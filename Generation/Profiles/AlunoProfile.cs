using AutoMapper;
using Generation.Data.Dtos;
using Generation.Models;

namespace Generation.Profiles
{
    public class AlunoProfile : Profile
    {
        public AlunoProfile()
        {
            CreateMap<CreateAlunoDto, Aluno>();
            CreateMap<UpdateAlunoDto, Aluno>();
            CreateMap<Aluno, UpdateAlunoDto>();
            CreateMap<Aluno, ReadAlunoDto>();
        }
    }
}

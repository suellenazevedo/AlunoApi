using System.ComponentModel.DataAnnotations;

namespace Generation.Data.Dtos
{
    public class ReadAlunoDto
    {

        public string? Nome { get; set; }

        public int Idade { get; set; }

        public string? NomeProf { get; set; }

        public int NumSala { get; set; }

        public float NotaPrimeiroSemestre { get; set; }

        public float NotaSegundoSemestre { get; set; }

        //public DateTime HoraDaConsulta { get; set;} = DateTime.Now;
    }
}

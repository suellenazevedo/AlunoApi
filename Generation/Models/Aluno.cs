using System.ComponentModel.DataAnnotations;

namespace Generation.Models
{
    public class Aluno
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do aluno é obrigatório")]
        [StringLength(200, ErrorMessage = "Não deve ultrapassar de 200 caracteres")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "A idade do aluno é obrigatória")]
        [Range(0, 99)]
        public int Idade { get; set; }

        [Required(ErrorMessage = "O nome do professor é obrigatório")]
        [StringLength(200, ErrorMessage = "Não deve ultrapassar de 200 caracteres")]
        public string? NomeProf { get; set; }

        [Required(ErrorMessage = "O número da sala é obrigatório")]
        public int NumSala { get; set; }

        [Required(ErrorMessage = "A nota do aluno do primeiro semestre é obrigatória")]
        [Range(0, 10, ErrorMessage = "A nota do semestre tem que ser entre 0 e 10")]
        public float NotaPrimeiroSemestre { get; set; }

        [Required(ErrorMessage = "A nota do aluno do segundo semestre é obrigatória")]
        [Range(0, 10, ErrorMessage = "A nota do semestre tem que ser entre 0 e 10")]
        public float NotaSegundoSemestre { get; set; }
    }
}

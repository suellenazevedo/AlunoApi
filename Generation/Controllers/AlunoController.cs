using Microsoft.AspNetCore.Mvc;
using Generation.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Generation.Data;
using Generation.Data.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace Generation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlunoController : ControllerBase
    {

        private AlunoContext _context;
        private IMapper _mapper;

        public AlunoController(AlunoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona um aluno ao banco de dados
        /// </summary>
        /// <param name="alunoDto">Objeto com os campos necessários para criação de um aluno</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddAluno([FromBody] CreateAlunoDto alunoDto)
        {
            Aluno aluno = _mapper.Map<Aluno>(alunoDto);
            _context.Alunos.Add(aluno);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAlunosById), new {id = aluno.Id}, aluno);
        }

        /// <summary>
        /// Lista todos os alunos do banco de dados
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code="200"></response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<ReadAlunoDto> GetAlunos([FromQuery] int skip = 0, 
            [FromQuery]int take = 50)
        {
            return _mapper.Map<List<ReadAlunoDto>>(_context.Alunos.Skip(skip).Take(take));
        }

        /// <summary>
        /// Lista um aluno especifico por ID
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code="404">Caso o aluno em questão não esteja no banco de dados</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAlunosById(int id)
        {

            var aluno = _context.Alunos
                .FirstOrDefault(aluno => aluno.Id == id);
            if (aluno == null) return NotFound();
            var alunoDto = _mapper.Map<ReadAlunoDto>(aluno);
            return Ok(aluno);
        }

        /// <summary>
        /// Altera os dados de um Aluno
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code="200">Retorna o aluno ja atualizado</response>
        /// <response code="404">Caso o aluno em questão não esteja no banco de dados</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AtualizaAluno(int id,
            [FromBody] UpdateAlunoDto alunoDto) 
        {
            var aluno = _context.Alunos.FirstOrDefault(
                aluno =>  aluno.Id == id);
            if(aluno == null) return NotFound();
            _mapper.Map(alunoDto, aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        /// <summary>
        /// Altera um dado especifico de um Aluno
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code="200">Retorna o aluno ja atualizado</response>
        /// <response code="404">Caso o aluno em questão não esteja no banco de dados</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AtualizaAlunoParcial(int id,
           JsonPatchDocument<UpdateAlunoDto> patch)
        {
            var aluno = _context.Alunos.FirstOrDefault(
                aluno => aluno.Id == id);
            if (aluno == null) return NotFound();

            var alunoParaAtualizar = _mapper.Map<UpdateAlunoDto>(aluno);

            patch.ApplyTo(alunoParaAtualizar, ModelState);

            if(!TryValidateModel(alunoParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(alunoParaAtualizar, aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        /// <summary>
        /// Apaga um aluno do banco de dados
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code="204"></response>
        /// <response code="404">Caso o aluno em questão não esteja no banco de dados</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeletaAluno(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(
                aluno => aluno.Id == id);
            if (aluno == null) return NotFound();
            _context.Remove(aluno);
            _context.SaveChanges();
            return NoContent();

        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.Controllers.Models;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.v2.Dtos;

namespace SmartSchool.WebAPI.v2.Controllers
{
    /// <summary>
    /// Versão 2 do controlador de aluno
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly IMapper _mapper;

        public AlunoController(IRepository repo, IMapper mapper)//injecao dependencia
        {
            _repo = repo;
            _mapper = mapper;
        } //injetando contexto aluno... Dados banco


        /// <summary>
        /// Retornar todos alunos
        /// </summary>
        /// <returns></returns>
        // public AlunoController() { }
        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repo.GetAllAlunos(true);

            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));

        }


        /// <summary>
        /// Retornar aluno Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoByID(id, false);

            if (aluno == null) return BadRequest("O Aluno não foi encontrado");

            var alunoDto = _mapper.Map<AlunoDto>(aluno);

            return Ok(alunoDto);

        }


        [HttpPost] //api/aluno
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model); //mepeado AlunoDto...Aluno

            _repo.Add(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno)); //Mapeado Aluno...AlunoDto
            }

            return BadRequest("Aluno não cadastrado");
        }

        [HttpPut("{id}")] //api/aluno
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunoByID(id);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, aluno);

            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno)); //Mapeado Aluno...AlunoDto
            }

            return BadRequest("Aluno não atualizado");

        }


        [HttpDelete("{id}")] //api/aluno
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunoByID(id);
            if (aluno == null) return BadRequest("Akluno não encontrado");

            _repo.Delete(aluno);
            if (_repo.SaveChanges())
            {
                return Ok("Aluno Deletado");
            }

            return BadRequest("Aluno não Deletado");


        }
    }
}
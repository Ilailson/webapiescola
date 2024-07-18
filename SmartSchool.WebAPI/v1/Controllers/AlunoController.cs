using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.Controllers.Models;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.v1.Dtos;

namespace SmartSchool.WebAPI.v1.Controllers
{
    /// <summary>
    /// Versão 1 controlador de alunos
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
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
        public async Task<IActionResult> Get()
        {
            var alunos = await _repo.GetAllAlunosAsync(true);

            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));

        }

        /// <summary>
        /// Mostrar parametros a ser preenchido
        /// </summary>
        /// <returns></returns>
        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new AlunoRegistrarDto());
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

        [HttpPatch("{id}")] //api/aluno
        public IActionResult Patch(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunoByID(id);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, aluno);

            _repo.Update(aluno);

            if (_repo.SaveChanges())
            {
                Console.WriteLine($"Aluno: {aluno} ");
                Console.WriteLine($"Id: {aluno.Id}");
                Console.WriteLine($"Nome: {aluno.Nome}");
                Console.WriteLine($"Sobrenome: {aluno.Sobrenome}");
                Console.WriteLine($"Telefone: {aluno.Telefone}");

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
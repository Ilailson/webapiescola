using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.Controllers.Models;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Helpers;
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
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            var alunos = await _repo.GetAllAlunosAsync(pageParams, true);

            var alunosResult = _mapper.Map<IEnumerable<AlunoDto>>(alunos);

            // paginação
            Response.AddPagination(alunos.CurrentPage, alunos.PageSize, alunos.TotalCount, alunos.TotalPages);

            return Ok(alunosResult);

        }

        
        [HttpGet("ByDisciplina/{id}")]
        public async Task<IActionResult> GetByDisciplinaId(int id)
        {
            var result = await _repo.GetAllAlunosByDisciplinaIdAsync(id, false);
            return Ok(result); // Corrigido para usar Ok() com letra minúscula
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

            var alunoDto = _mapper.Map<AlunoRegistrarDto>(aluno);

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
        public IActionResult Patch(int id, AlunoPatchDto model)
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

                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoPatchDto>(aluno)); //Mapeado Aluno...AlunoDto
            }

            return BadRequest("Aluno não atualizado");
        }

        [HttpPatch("{id}/trocarEstado")] //api/aluno/{id}/trocarEstado
        public IActionResult trocarEstado(int id, TrocarEstadoDto trocarEstado)
        {
            var aluno = _repo.GetAlunoByID(id);
            
            if (aluno == null) return BadRequest("Aluno não encontrado");

            aluno.Ativo = trocarEstado.Estado;

            _repo.Update(aluno);

            if (_repo.SaveChanges())
            {
                var msn = aluno.Ativo ? "ativado" : "desativado";
                return Ok(new { message = $"Aluno {msn} com sucesso"});
            }

            return BadRequest("Aluno não atualizado");
        }

        [HttpDelete("{id}")] //api/aluno
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunoByID(id);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _repo.Delete(aluno);
            if (_repo.SaveChanges())
            {
                return Ok("Aluno Deletado");
            }

            return BadRequest("Aluno não Deletado");


        }
    }
}
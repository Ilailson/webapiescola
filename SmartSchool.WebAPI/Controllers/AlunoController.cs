using Microsoft.AspNetCore.Mvc;
using SmartSchool.Controllers.Models;
using SmartSchool.WebAPI.Data;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public readonly IRepository _repo;

        public AlunoController(IRepository repo)//injecao dependencia
        {
            _repo = repo;
        } //injetando contexto aluno... Dados banco


        // public AlunoController() { }
        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllAlunos(true);
            return Ok(result);
            
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoByID(id, false);
            
            
            if (aluno == null) return BadRequest("O Aluno não foi encontrado");
            return Ok(aluno);
            
        }


        [HttpPost] //api/aluno
        public IActionResult Post(Aluno aluno)
        {
            _repo.Add(aluno);
            if(_repo.SaveChanges())
            {
                return Ok(aluno);
            }

            return BadRequest("Aluno não cadastrado");
        }

        [HttpPut("{id}")] //api/aluno
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _repo.GetAlunoByID(id);
            if(alu == null) return BadRequest("Aluno não encontrado");

            _repo.Update(aluno);
            if(_repo.SaveChanges())
            {
                return Ok(aluno);
            }

            return BadRequest("Aluno não atualizado");
            
        }

        [HttpPatch("{id}")] //api/aluno
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _repo.GetAlunoByID(id);
            if(alu == null) return BadRequest("Aluno não encontrado");

            _repo.Update(aluno);
            if(_repo.SaveChanges())
            {
            Console.WriteLine($"Aluno: {aluno} ");
            Console.WriteLine($"Id: {aluno.Id}");
            Console.WriteLine($"Nome: {aluno.Nome}");
            Console.WriteLine($"Sobrenome: {aluno.Sobrenome}");
            Console.WriteLine($"Telefone: {aluno.Telefone}");

                return Ok(aluno);
            }

            return BadRequest("Aluno não atualizado");
        }

         [HttpDelete("{id}")] //api/aluno
        public IActionResult Delete(int id)
        {
            var alu = _repo.GetAlunoByID(id);
            if(alu == null) return BadRequest("Akluno não encontrado");

            _repo.Delete(alu);
            if(_repo.SaveChanges())
            {
                return Ok("Aluno Deletado");
            }

            return BadRequest("Aluno não Deletado");
            
            
        }
    }
}
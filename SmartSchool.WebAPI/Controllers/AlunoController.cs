using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public List<Aluno> Alunos = new List<Aluno>(){
            new Aluno(){
                Id = 1,
                Nome = "Linda",
                Sobrenome = "Cunha",
                Telefone = "1232121324"
            },
            new Aluno(){
                Id = 2,
                Nome = "Carla",
                Sobrenome = "Miranda",
                Telefone = "999999999"
            },
            new Aluno(){
                Id = 3,
                Nome = "Jamaica",
                Sobrenome = "Carla",
                Telefone = "888888888"
            }
        };

        public AlunoController() { }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Alunos);
            
        }
        [HttpGet("byId")]//api/aluno/id
        public IActionResult GetById(int id)
        {
            var aluno = Alunos.FirstOrDefault(x => x.Id == id);
            
            
            if (aluno == null) return BadRequest("O Aluno não foi encontrado");
            Console.WriteLine(aluno);
            return Ok(aluno);
            
        }

        [HttpGet("byName")] //api/aluno/byName?nome=Jamaica&sobrenome=Carla
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = Alunos.FirstOrDefault(x => 
            x.Nome.Contains(nome) && x.Sobrenome.Contains(sobrenome)
            );
            
            
            if (aluno == null) return BadRequest("O Aluno não foi encontrado");
            return Ok(aluno);
            
        }

        [HttpPost] //api/aluno
        public IActionResult Post(Aluno aluno)
        {
            return Ok(aluno);
            
        }

        [HttpPut("{id}")] //api/aluno
        public IActionResult Put(int id, Aluno aluno)
        {
            return Ok(aluno);
            
        }

        [HttpPatch("{id}")] //api/aluno
        public IActionResult Patch(int id, Aluno aluno)
        {
            return Ok(aluno);
            
        }

         [HttpDelete("{id}")] //api/aluno
        public IActionResult Delete(int id)
        {
            return Ok();
            
        }
    }
}
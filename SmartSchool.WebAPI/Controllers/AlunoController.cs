using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly SmartContext _context;

        public AlunoController(SmartContext context)
        {
            _context = context;
        } //injetando contexto aluno... Dados banco


        // public AlunoController() { }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Alunos);
            
        }
        [HttpGet("byId")]
        public IActionResult GetById(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(x => x.Id == id);
            
            
            if (aluno == null) return BadRequest("O Aluno não foi encontrado");
            return Ok(aluno);
            
        }

        [HttpGet("byName")] //api/aluno/byName?nome=Jamaica&sobrenome=Carla
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = _context.Alunos.FirstOrDefault(x => 
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
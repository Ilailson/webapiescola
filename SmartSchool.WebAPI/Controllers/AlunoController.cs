using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Controllers.Models;
using SmartSchool.WebAPI.Data;

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
        [HttpGet("byId/{id}")]
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
            _context.Add(aluno);
            _context.SaveChanges();
            return Ok(aluno);
            
        }

        [HttpPut("{id}")] //api/aluno
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _context.Alunos.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if(alu == null) return BadRequest("Aluno não encontrado");

            _context.Update(aluno);
            _context.SaveChanges();
            return Ok(aluno);
            
        }

        [HttpPatch("{id}")] //api/aluno
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _context.Alunos.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if(alu == null) return BadRequest("Aluno não encontrado");

            Console.WriteLine($"Aluno: {aluno} ");
            Console.WriteLine($"Id: {aluno.Id}");
            Console.WriteLine($"Nome: {aluno.Nome}");
            Console.WriteLine($"Sobrenome: {aluno.Sobrenome}");
            Console.WriteLine($"Telefone: {aluno.Telefone}");


            _context.Update(aluno);
            _context.SaveChanges();
            return Ok(aluno);
            
        }

         [HttpDelete("{id}")] //api/aluno
        public IActionResult Delete(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(x => x.Id == id);
            if(aluno == null) return BadRequest("Akluno não encontrado");

            _context.Remove(aluno);
            _context.SaveChanges();
            return Ok(aluno);
            
        }
    }
}
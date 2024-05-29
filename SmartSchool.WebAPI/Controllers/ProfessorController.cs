using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Controllers.Models;
using SmartSchool.WebAPI.Data;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly SmartContext _context;
        public ProfessorController(SmartContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Professores);
            
        }
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _context.Professores.FirstOrDefault(x => x.Id == id);
            
            
            if (professor == null) return BadRequest("O Professor não foi encontrado");
            return Ok(professor);
            
        }

        [HttpGet("byName")] //api/professor/byName?nome=Jamaica&sobrenome=Carla
        public IActionResult GetByName(string nome)
        {
            var professor = _context.Professores.FirstOrDefault(x => x.Nome.Contains(nome) 
            );
            
            
            if (professor == null) return BadRequest("O Professor não foi encontrado");
            return Ok(professor);
            
        }

        [HttpPost] //api/professor
        public IActionResult Post(Professor professor)
        {
            _context.Add(professor);
            _context.SaveChanges();
            return Ok(professor);
            
        }

        [HttpPut("{id}")] //api/professor
        public IActionResult Put(int id, Professor professor)
        {
            var alu = _context.Professores.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if(alu == null) return BadRequest("Professor não encontrado");

            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
            
        }

        [HttpPatch("{id}")] //api/professor
        public IActionResult Patch(int id, Professor professor)
        {
            var alu = _context.Professores.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if(alu == null) return BadRequest("Professor não encontrado");

            Console.WriteLine("Professor");
            Console.WriteLine($"Id: {professor.Id}");
            Console.WriteLine($"Nome: {professor.Nome}");
            Console.WriteLine($"Sobrenome: {professor.Sobrenome}");


            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
            
        }

         [HttpDelete("{id}")] //api/professor
        public IActionResult Delete(int id)
        {
            var professor = _context.Professores.FirstOrDefault(x => x.Id == id);
            if(professor == null) return BadRequest("Akluno não encontrado");

            _context.Remove(professor);
            _context.SaveChanges();
            return Ok(professor);
            
        }
    }
}
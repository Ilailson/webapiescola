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
        public readonly IRepository _repo;
        public ProfessorController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllProfessores(true);
            return Ok(result);
            
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorByID(id, false);

            
            if (professor == null) return BadRequest("O Professor não foi encontrado");
            return Ok(professor);
            
        }

        [HttpPost] //api/professor
        public IActionResult Post(Professor professor)
        {
            _repo.Add(professor);
            if(_repo.SaveChanges())
            {
                return Ok(professor);
            }

            return BadRequest("Professor não cadastrado");
            
        }

        [HttpPut("{id}")] 
        public IActionResult Put(int id, Professor professor)
        {
            var prof = _repo.GetProfessorByID(id, false);
            if(prof == null) return BadRequest("Professor não encontrado");

            _repo.Update(professor);
            if(_repo.SaveChanges())
            {
                return Ok(professor);
            }

            return BadRequest("Professor não atualizado");
            
        }

        [HttpPatch("{id}")] //api/professor
        public IActionResult Patch(int id, Professor professor)
        {
            var profe = _repo.GetProfessorByID(id);
            if(profe == null) return BadRequest("Professor não encontrado");

    


            _repo.Update(professor);
            if(_repo.SaveChanges())
            {
                Console.WriteLine("Professor");
                Console.WriteLine($"Id: {professor.Id}");
                Console.WriteLine($"Nome: {professor.Nome}");
                Console.WriteLine($"Sobrenome: {professor.Sobrenome}");

                return Ok(professor);
            }

            return BadRequest("Professor não atualizado");
            
        }

         [HttpDelete("{id}")] //api/professor
        public IActionResult Delete(int id)
        {
            var prof = _repo.GetProfessorByID(id, false);
            if(prof == null) return BadRequest("Professor não encontrado");

            _repo.Delete(prof);
            if(_repo.SaveChanges())
            {
                return Ok("Professor Deletado");
            }

            return BadRequest("Professor não Deletado");
            
        }
    }
}
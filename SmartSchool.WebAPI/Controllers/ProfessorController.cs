using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.Controllers.Models;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Dtos;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly IMapper _mapper;

        public ProfessorController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var professor = _repo.GetAllProfessores(true);
            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professor));
        }

        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new ProfessorRegistrarDto());
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorByID(id, false);

            if (professor == null) return BadRequest("O Professor não foi encontrado");
    
            var professorDto = _mapper.Map<ProfessorDto>(professor);

            return Ok(professor);
        }

        [HttpPost] //api/professor
        public IActionResult Post(ProfessorRegistrarDto model)
        {
            var prof = _mapper.Map<Professor>(model);

            _repo.Add(prof);

            if(_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(prof));
            }

            return BadRequest("Professor não cadastrado");
            
        }

        [HttpPut("{id}")] 
        public IActionResult Put(int id, ProfessorRegistrarDto model)
        {
            var prof = _repo.GetProfessorByID(id, false);
            if(prof == null) return BadRequest("Professor não encontrado");

            _mapper.Map(model, prof);
            _repo.Update(prof);


            if(_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(prof));
            }

            return BadRequest("Professor não atualizado");
            
        }

        [HttpPatch("{id}")] //api/professor
        public IActionResult Patch(int id, ProfessorRegistrarDto model)
        {
            var prof = _repo.GetProfessorByID(id);
            if(prof == null) return BadRequest("Professor não encontrado");

    
            _mapper.Map(model, prof);

            _repo.Update(prof);


            if(_repo.SaveChanges())
            {
                Console.WriteLine("Professor");
                Console.WriteLine($"Id: {prof.Id}");
                Console.WriteLine($"Nome: {prof.Nome}");
                Console.WriteLine($"Sobrenome: {prof.Sobrenome}");

                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(prof));
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
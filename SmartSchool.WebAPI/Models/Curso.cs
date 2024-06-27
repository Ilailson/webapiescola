using System.Collections;
using SmartSchool.Controllers.Models;

namespace SmartSchool.WebAPI.Models
{
    public class Curso
    {
        public Curso(){ }

        public Curso(int id, string nome) 
        {
            this.Id = id;
            this.Nome = nome;
            Disciplinas = new List<Disciplina>();
        }
                public int Id { get; set; } 
        public string Nome { get; set; }
        public IEnumerable <Disciplina> Disciplinas { get; set; }
    }
}
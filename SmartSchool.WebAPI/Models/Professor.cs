using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.WebAPI.Models
{
    public class Professor
    {
        public Professor(){}
        public Professor(int id) 
        {
            this.Id = id;
            this.Nome = Nome;
        }
                public int Id { get; set; }
        public string Nome { get; set; }
        public IEnumerable<Disciplina> Disciplinas { get; set; } //Professor muitas... disciplinas
    }
}
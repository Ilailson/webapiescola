using System.Collections.Generic;

namespace SmartSchool.Controllers.Models
{
    public class Aluno
    {
        public Aluno() { 
            AlunosDisciplinas = new List<AlunoDisciplina>();
        }

        public Aluno(int id, string nome, string sobrenome, string telefone)
        {
            this.Id = id;
            this.Nome = nome;
            this.Sobrenome = sobrenome;
            this.Telefone = telefone;
            AlunosDisciplinas = new List<AlunoDisciplina>();
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public IEnumerable<AlunoDisciplina> AlunosDisciplinas { get; set; }
    }
}
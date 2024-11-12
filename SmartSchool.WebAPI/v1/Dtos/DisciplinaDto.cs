using System.Collections.Generic;
using SmartSchool.WebAPI.v1.Dtos; // Adicione esta linha para resolver dependências

namespace SmartSchool.Controllers.v1.Dtos
{
    public class DisciplinaDto
    {
        // public Disciplina() { }

        // public Disciplina(int id, string nome, int professorId, int cursoId)
        // {
        //     this.id = id;
        //     this.Nome = nome;
        //     this.ProfessorId = professorId;
        //     this.CursoId = cursoId;
        // }
        public int id { get; set; }
        public string Nome { get; set; }
        public int CargaHoraria { get; set; }
        public int? PrerequisitoId { get; set; } = null;
        public DisciplinaDto Prerequisito { get; set; } // Autorelacionamento... DisciplinaToDisciplina
        public int ProfessorId { get; set; }
        public ProfessorDto Professor { get; set; }
        public int CursoId { get; set; }
        public CursoDto Curso { get; set; }
        public IEnumerable<AlunoDto> Alunos { get; set; }
    }
}

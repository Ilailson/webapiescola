using System.Collections.Generic;
using SmartSchool.Controllers.v1.Dtos; // Adicione esta linha para corrigir o erro

namespace SmartSchool.WebAPI.v1.Dtos
{
    public class ProfessorDto
    {
        public int Id { get; set; }
        public int Registro { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; } = true;
        public IEnumerable<DisciplinaDto> Disciplinas { get; set; }
    }
}

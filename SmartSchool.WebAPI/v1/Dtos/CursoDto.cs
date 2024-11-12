using System.Collections.Generic;
using SmartSchool.Controllers.v1.Dtos; // Adicione esta linha

namespace SmartSchool.WebAPI.v1.Dtos
{
    public class CursoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public IEnumerable<DisciplinaDto> Disciplinas { get; set; }
    }
}

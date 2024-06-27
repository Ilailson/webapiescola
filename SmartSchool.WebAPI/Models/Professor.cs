namespace SmartSchool.Controllers.Models
{
    public class Professor
    {
        public Professor() { 
            Disciplinas = new List<Disciplina>();
        }

        public Professor(int id, 
                         int registro, 
                         string nome, 
                         string sobrenome)
        {
            this.Id = id;
            this.Nome = nome;
            this.Sobrenome = sobrenome;
            this.Registro = registro;
            Disciplinas = new List<Disciplina>();
        }
        public int Id { get; set; }
         public int Registro { get; set; }
        public string Nome { get; set; }
        public DateTime DataIni { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; } = null;
        public bool Ativo { get; set; } = true;
        public string Sobrenome { get; set; }
        public IEnumerable<Disciplina> Disciplinas { get; set; }
    }
}
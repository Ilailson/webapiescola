namespace SmartSchool.Controllers.Models
{
    public class Aluno
    {
        public Aluno() { 
            AlunosDisciplinas = new List<AlunoDisciplina>();
        }

        public Aluno(int id, 
                     int matricula,
                     string nome,
                     string sobrenome, 
                     string telefone,
                     DateTime dataNasc)
        {
            this.Id = id;
            this.Matricula = matricula;
            this.Nome = nome;
            this.Sobrenome = sobrenome;
            this.Telefone = telefone;
            this.DataNasc = dataNasc;
            AlunosDisciplinas = new List<AlunoDisciplina>();
        }
        public int Id { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string? Sobrenome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNasc { get; set; }
        public DateTime DataIni { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; } = null;
        public bool Ativo { get; set; } = true;
        public IEnumerable<AlunoDisciplina> AlunosDisciplinas { get; set; }
    }
}
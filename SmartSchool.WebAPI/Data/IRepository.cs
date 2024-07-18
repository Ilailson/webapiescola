using SmartSchool.Controllers.Models;

namespace SmartSchool.WebAPI.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class; //receber tipo... Sera sempre classe
        void Update<T>(T entity) where T : class; //receber tipo... Sera sempre classe
        void Delete<T>(T entity) where T : class; //receber tipo... Sera sempre classe
        bool SaveChanges();

        //===========================Alunos=========================
        Task<Aluno[]> GetAllAlunosAsync(bool includeProfessor = false);
        Aluno[] GetAllAlunos(bool includeProfessor = false);
        Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false);
        Aluno GetAlunoByID(int alunoId, bool includeProfessor = false);

        //===========================Professores=========================
        Professor[] GetAllProfessores(bool includeAlunos = false);
        Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false);
        Professor GetProfessorByID(int professorId, bool includeProfessores = false);
    }
}
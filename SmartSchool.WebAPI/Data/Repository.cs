using Microsoft.EntityFrameworkCore;
using SmartSchool.Controllers.Models;
using SmartSchool.WebAPI.Helpers;

namespace SmartSchool.WebAPI.Data
{
    public class Repository : IRepository
    {
        private readonly SmartContext _context;

        public Repository(SmartContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges()>0); //verdadeiro... maior 0
        }

        public async Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if(includeProfessor)
            {
                query = query.Include(o => o.AlunosDisciplinas)
                .ThenInclude(o => o.Disciplina)
                .ThenInclude(o => o.Professor);
            }

            query = query.AsNoTracking().OrderBy(o => o.Id);

            if(!string.IsNullOrEmpty(pageParams.Nome))
                query = query.Where(aluno => aluno.Nome
                                                  .ToUpper()
                                                  .Contains(pageParams.Nome.ToUpper()) || 
                                             aluno.Sobrenome
                                                  .ToUpper()
                                                  .Contains(pageParams.Nome.ToUpper()));

            if(pageParams.Matricula > 0)
                query = query.Where(aluno => aluno.Matricula == pageParams.Matricula);

            if(pageParams.Ativo != null)
                query = query.Where(aluno => aluno.Ativo == (pageParams.Ativo != 0));

            
            // return await query.ToListAsync();

            return await PageList<Aluno>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public Aluno[] GetAllAlunos(bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if(includeProfessor)
            {
                query = query.Include(o => o.AlunosDisciplinas)
                .ThenInclude(o => o.Disciplina)
                .ThenInclude(o => o.Professor);
            }

            query = query.AsNoTracking().OrderBy(o => o.Id);
            
            return query.ToArray();
        }

        public Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if(includeProfessor)
            {
                query = query.Include(o => o.AlunosDisciplinas)
                .ThenInclude(o => o.Disciplina)
                .ThenInclude(o => o.Professor);
            }

            query = query.AsNoTracking()
                    .OrderBy(o => o.Id)
                    .Where(aluno => 
                    aluno.AlunosDisciplinas.Any(o => o.DisciplinaId == disciplinaId));

            return query.ToArray();
        }

        public Aluno GetAlunoByID(int alunoId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if(includeProfessor)
            {
                query = query.Include(o => o.AlunosDisciplinas)
                .ThenInclude(o => o.Disciplina)
                .ThenInclude(o => o.Professor);
            }

            query = query.AsNoTracking()
                    .OrderBy(o => o.Id)
                    .Where(aluno => aluno.Id == alunoId);
            
            return query.FirstOrDefault();
        }

        public Professor[] GetAllProfessores(bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if(includeAlunos)
            {
                query = query.Include(o => o.Disciplinas)
                .ThenInclude(o => o.AlunosDisciplinas)
                .ThenInclude(o => o.Aluno);
            }

        query = query.AsNoTracking().OrderBy(o => o.Id);

            return query.ToArray();
        }

        public Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if(includeAlunos)
            {
                query = query.Include(o => o.Disciplinas)
                .ThenInclude(o => o.AlunosDisciplinas)
                .ThenInclude(o => o.Aluno);
            }

        query = query.AsNoTracking()
        .OrderBy(o => o.Id).
        Where(o => o.Disciplinas.Any(
                o => o.AlunosDisciplinas.Any(
                o => o.DisciplinaId == disciplinaId
        )));

            return query.ToArray();

        }

        public Professor GetProfessorByID(int professorId, bool includeProfessores = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if(includeProfessores)
            {
                query = query.Include(o => o.Disciplinas)
                .ThenInclude(o => o.AlunosDisciplinas)
                .ThenInclude(o => o.Aluno);
            }

        query = query.AsNoTracking()
        .OrderBy(o => o.Id)
        .Where(o => o.Id == professorId);

            return query.FirstOrDefault();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public class SmartContext: DbContext
    {
        public SmartContext(DbContextOptions<SmartContext> options) : base(options) { }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<AlunoDisciplina> AlunoDisciplina { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //quando tem relacionamento Muitos para Muitos.
            modelBuilder.Entity<AlunoDisciplina>().HasKey(Add => new {Add.AlunoId, Add.DisciplinaId});
        }
    }
}
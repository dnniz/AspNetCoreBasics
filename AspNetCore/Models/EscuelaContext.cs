using CSharpNetCore.App;
using CSharpNetCore.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Models
{
    public class EscuelaContext : DbContext
    {
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Evaluacion> Evaluaciones { get; set; }
        public DbSet<Asignatura> Asignaturas { get; set; }
        public DbSet<Escuela> Escuelas { get; set; }
        public DbSet<Curso> Cursos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var engine = new EscuelaEngine();
            engine.Inicializar();
            var dicc = engine.ObtenerDiccionarioEscuela();

            dicc.TryGetValue(LlaveDiccionario.Alumno, out var lstAlumnos);
            dicc.TryGetValue(LlaveDiccionario.Evaluacion, out var lstEvaluaciones);
            dicc.TryGetValue(LlaveDiccionario.Escuela, out var lstEscuela);
            dicc.TryGetValue(LlaveDiccionario.Curso, out var lstCursos);
            dicc.TryGetValue(LlaveDiccionario.Asignatura, out var lstAsignaturas);

            var alumnos = lstAlumnos.Cast<Alumno>().Select(x => new Alumno { AlumnoId = x.AlumnoId, CursoId = x.CursoId, Nombre = x.Nombre }).ToList();
            var evaluaciones = lstEvaluaciones.Cast<Evaluacion>().Select(x => new Evaluacion { EvaluacionId = x.EvaluacionId, AlumnoId = x.AlumnoId, Nombre = x.Nombre, AsignaturaId = x.AsignaturaId, Nota = x.Nota}).ToList();
            var escuelas = lstEscuela.Cast<Escuela>().Select(x => new Escuela { EscuelaId = x.EscuelaId, Nombre = x.Nombre, CantAlumnos = x.CantAlumnos, TipoEscuela = x.TipoEscuela, AñoCreación = x.AñoCreación, Direccion = x.Direccion }).ToList();
            var cursos = lstCursos.Cast<Curso>().Select(x => new Curso { CursoId = x.CursoId, EscuelaId = x.EscuelaId, Direccion = x.Direccion, Nombre = x.Nombre, Jornada = x.Jornada} ) .ToList();
            var asignaturas = lstAsignaturas.Cast<Asignatura>().Select(x => new Asignatura { AsignaturaId = x.AsignaturaId, Nombre = x.Nombre, CursoId = x.CursoId} ) .ToList();


            modelBuilder.Entity<Escuela>().HasData(escuelas.ToArray());
            modelBuilder.Entity<Curso>().HasData(cursos.ToArray());
            modelBuilder.Entity<Asignatura>().HasData(asignaturas.ToArray());
            modelBuilder.Entity<Alumno>().HasData(alumnos.ToArray());
            modelBuilder.Entity<Evaluacion>().HasData(evaluaciones.ToArray());

            //modelBuilder.Entity<Evaluacion>().HasData(evaluaciones.ToArray());

            

            //modelBuilder.Entity<Evaluacion>().HasKey(x => x.EvaluacionId);


            //modelBuilder.Entity<Evaluacion>().HasOne(x => x.Alumno).WithOne().HasForeignKey<Alumno>(x => x.AlumnoId);
            //modelBuilder.Entity<Evaluacion>().HasData(evaluaciones.ToArray());



            //modelBuilder.Entity<Alumno>()//   .WithOne().HasForeignKey<Evaluacion>(x => x.AlumnoId);
            //modelBuilder.Entity<Alumno>().HasData(alumnos.ToArray());
            



            base.OnModelCreating(modelBuilder);
        }


        public EscuelaContext(DbContextOptions<EscuelaContext> options) : base(options)
        {

        }
    }
}

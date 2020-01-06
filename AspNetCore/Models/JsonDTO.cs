using CSharpNetCore.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Models
{
    public class JsonDTO
    {
        public Escuela Escuela { get; set; }
        public Asignatura Asignatura { get; set; }
        public List<Asignatura> Asignaturas { get; set; }
        public List<Escuela> Escuelas { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models
{
    public class Curso
    {
        [Key]
        public int CursoID { get; set; }
        public int Anho { get; set; }

        public virtual ICollection<TestCurso> TestCursos { get; set; }
    }
}
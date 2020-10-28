using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models
{
    public class Test
    {
        [Key]
        public int TestID { get; set; }
        public string Nombre { get; set; }

        public bool Visible { get; set; }
        public virtual ICollection<TestCurso> TestCursos { get; set; }
        public virtual ICollection<TestPregunta> TestPreguntas { get; set; }
        public virtual ICollection<TestEstudiante> TestEstudiantes { get; set; }
       
    }
}
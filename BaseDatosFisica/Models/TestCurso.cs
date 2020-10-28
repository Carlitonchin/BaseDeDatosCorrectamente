using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models
{
    public class TestCurso
    {
        [Key]
        public int TestCursoID { get; set; }
        public int CursoID { get; set; }
        public int TestID { get; set; }

        public virtual Curso Curso { get; set; }
        public virtual Test Test { get; set; }
    }
}
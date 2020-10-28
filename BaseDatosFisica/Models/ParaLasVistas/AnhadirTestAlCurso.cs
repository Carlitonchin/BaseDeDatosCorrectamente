using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models.ParaLasVistas
{
    public class AnhadirTestAlCurso
    {
        public IEnumerable<Test> tests { get; set; }
        
        public Curso curso { get; set; }
    }
}
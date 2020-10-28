using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models.ParaLasVistas
{
    public class TestCurso_Pregunta
    {
        [Key]
        public int TestCursoPreguntaID { get; set; }
        public Test test { get; set; }
        public Pregunta pregunta { get; set; }
        
        public virtual bool[] marcado { get; set; }
        public string respuesta { get; set; }
       
    }
}
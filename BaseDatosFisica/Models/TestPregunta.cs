using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models
{
    public class TestPregunta
    {
        [Key]
        public int TestPreguntaID { get; set; }
        public int PreguntaID { get; set; }
        public int TestID { get; set; }
        public virtual Pregunta Pregunta { get; set; }
        public virtual Test Test { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models
{
    public class TestEstudiante
    {
        [Key]
        public int TestEstudianteID { get; set; }
        public string Id { get; set; }
        public int TestID { get; set; }

        public virtual ApplicationUser estudiante { get; set; }
        public virtual Test test { get; set; }
        public virtual ICollection<RespuestaMarcada> RespuestasMarcadas { get; set; }
        public virtual ICollection<RespuestaEscrita> RespuestasEscritas { get; set; }

    }
}
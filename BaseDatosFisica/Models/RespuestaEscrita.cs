using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models
{
    public class RespuestaEscrita
    {
        [Key]
        public int RespuestaEscritaID { get; set; }
        public int TestEstudianteID { get; set; }
        public virtual TestEstudiante TestEstudiante { get; set; }
        public int PreguntaID { get; set; }
        public virtual Pregunta Pregunta { get; set; }
        public string Respuesta { get; set; }
    }
}
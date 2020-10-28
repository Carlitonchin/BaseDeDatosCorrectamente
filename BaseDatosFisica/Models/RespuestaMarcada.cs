using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models
{
    public class RespuestaMarcada
    {
        [Key]
        public int RespuestaMarcadaID { get; set; }
        public int TestEstudianteID { get; set; }
        public virtual TestEstudiante TestEstudiante { get; set; }
        public int RespuestaID { get; set; }
        public virtual Respuesta Respuesta { get; set; }
    }
}
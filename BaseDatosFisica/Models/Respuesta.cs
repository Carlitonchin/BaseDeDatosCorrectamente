using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models
{
    public class Respuesta
    {
        [Key]
        public int RespuestaID { get; set; }

       [Required(ErrorMessage ="Campo obligatorio")]
        public string Texto { get; set; }
        public string Imagen { get; set; }

        public virtual ICollection<RespuestaCategoria> RespuestaCategorias { get; set; }

        public int PreguntaID { get; set; }

        public virtual Pregunta Pregunta { get; set; }
        public virtual ICollection<RespuestaMarcada> RespuestasMarcadas { get; set; }
        
    }
}
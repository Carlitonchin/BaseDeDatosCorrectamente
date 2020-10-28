using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models
{
    public class RespuestaCategoria
    {
        [Key]
        public int RespuestaCategoriaID { get; set; }
        public int RespuestaID { get; set; }

        public int CategoriaID { get; set; }

        public virtual Respuesta Respuesta { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
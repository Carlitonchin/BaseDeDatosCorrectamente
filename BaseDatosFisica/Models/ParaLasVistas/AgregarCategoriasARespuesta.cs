using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models.ParaLasVistas
{
    public class AgregarCategoriasARespuesta
    {
        public Respuesta respuesta { get; set; }
        public IEnumerable<Categoria> categorias { get; set; }
    }
}
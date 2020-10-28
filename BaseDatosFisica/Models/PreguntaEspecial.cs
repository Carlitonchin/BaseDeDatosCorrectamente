using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models
{
    public class PreguntaEspecial : Pregunta
    {
        public int TiempoPrevio { get; set; }
        public string ImagenPrevia { get; set; }
        public string EnunciadoPrevio { get; set; }
    }
}
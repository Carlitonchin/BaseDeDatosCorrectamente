using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models
{
    public class Pregunta
    {
        [Key]
        public int PreguntaID { get; set; }
        public bool Tipo { get; set; }
        public string Nombre { get; set; }
        public string Enunciado { get; set; }
        public string Imagen { get; set; }
        public int Tiempo { get; set; }
        
        public bool? Introduccion { get; set; }
        public int? TiempoPrevio { get; set; }
        public string ImagenPrevia { get; set; }
        public string EnunciadoPrevio { get; set; }
        public virtual ICollection<Respuesta> Respuestas { get; set; }
        public virtual ICollection<TestPregunta> TestsPregunta { get; set; }
        public virtual ICollection<RespuestaEscrita> RespuestasEscritas { get; set; }
    }
}
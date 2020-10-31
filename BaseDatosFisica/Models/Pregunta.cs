using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models
{
    public class RequiredIfIntroduction : ValidationAttribute
    {
        private bool Introduccion;
        public RequiredIfIntroduction(bool Introduccion)
        {
            this.Introduccion = Introduccion;
        }

        public override bool IsValid(object value)
        {
            string texto = value as string;

            return texto != null;
        }

        
    }
    public class Pregunta
    {
        [Key]
        public int PreguntaID { get; set; }

        [Display(Name ="De Marcar")]
        public bool Tipo { get; set; }

        [Required(ErrorMessage ="Una pregunta sin nombre no es una pregunta")]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="Una pregunta sin enunciado no es una pregunta")]
        public string Enunciado { get; set; }
        public string Imagen { get; set; }
        public int Tiempo { get; set; }
        
        public bool Introduccion { get; set; }

        
        public int? TiempoPrevio { get; set; }
        public string ImagenPrevia { get; set; }
        
        public string EnunciadoPrevio { get; set; }
        public virtual ICollection<Respuesta> Respuestas { get; set; }
        public virtual ICollection<TestPregunta> TestsPregunta { get; set; }
        public virtual ICollection<RespuestaEscrita> RespuestasEscritas { get; set; }
    }
}
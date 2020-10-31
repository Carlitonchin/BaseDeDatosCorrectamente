using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaID { get; set; }

        [Display(Name ="Nombre")]
        [Required(ErrorMessage ="Campo obligatorio")]
        public string CategoriaNombre { get; set; }

        public virtual ICollection<RespuestaCategoria> RespuestasCategoria { get; set; }
    }
}
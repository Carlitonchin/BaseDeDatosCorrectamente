using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models
{
    public class Procedencia
    {
        [Key]
        public int ProcedenciaID { get; set; }
        [Display(Name = "Procedencia")]
        public string valor { get; set; }
        public virtual ICollection<ApplicationUser> estudiantes { get; set; }
    }
}
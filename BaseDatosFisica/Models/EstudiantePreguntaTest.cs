using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models
{
    public class EstudiantePreguntaTest
    {
        [Key]
        public int EstudiantePreguntaTestID { get; set; }
        public string IdEstudiante { get; set; }
        public int TestID { get; set; }
        public int PreguntaID { get; set; }
        public DateTime Inicio { get; set; }
        public bool TiempoAgotado { get; set; }
    }
}
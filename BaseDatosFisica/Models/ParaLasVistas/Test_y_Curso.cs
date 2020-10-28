using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models.ParaLasVistas
{
    public class Test_y_Curso
    {
        
        public  Test Test { get; set; }
        public  Curso Curso { get; set; }
    }
}
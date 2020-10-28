using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models.ParaLasVistas
{
    public class ResponderTestViewModel
    {
        public Test test { get; set; }
        public Pregunta pregunta { get; set; }

        public double TiempoRestante { get; set; }
    }
}
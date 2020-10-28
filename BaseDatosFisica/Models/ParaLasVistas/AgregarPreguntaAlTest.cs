using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models.ParaLasVistas
{
    public class AgregarPreguntaAlTest
    {
        public Test test { get; set; }
        public IEnumerable<Pregunta> preguntas { get; set; }
        public string FiltroNombre { get; set; }
        public IEnumerable<Categoria> FiltroCategorias { get; set; }
        public IEnumerable<int> CategoriasID { get; set; }
    }
}
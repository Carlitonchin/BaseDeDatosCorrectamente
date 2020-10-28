using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models.ParaLasVistas
{
    public class EstudianteTestItemView
    {
        private ApplicationDbContext _dbContext;
        public EstudianteTestItemView(ApplicationUser estudiante)
        {
            Estudiante = estudiante;

            _dbContext = new ApplicationDbContext();

            Correctas = new Dictionary<int, int>();

            Prepare();
        }

        public void Prepare()
        {
            var estudianteTests = _dbContext.TestsEstudiantes.Where(et => et.Id == Estudiante.Id).ToList();
            foreach (var estudianteTest in estudianteTests)
            {
                var test = estudianteTest.test;
                var CantCorrectas = 0;
                foreach (var respuestaMarcada in estudianteTest.RespuestasMarcadas)
                {
                    if (respuestaMarcada.Respuesta.RespuestaCategorias?.Count > 0)
                        CantCorrectas++;
                }
                foreach (var respuestaEscrita in estudianteTest.RespuestasEscritas)
                {
                    foreach (var posibleRespuesta in respuestaEscrita.Pregunta.Respuestas)
                    {
                        if (posibleRespuesta?.Texto?.ToUpper() == respuestaEscrita?.Respuesta?.ToUpper() &&
                            posibleRespuesta?.RespuestaCategorias?.Count > 0)
                        {
                            CantCorrectas++;
                            break;
                        }
                    }
                }

                Correctas[test.TestID] = CantCorrectas;
            }
        }

        public ApplicationUser Estudiante { get; set; }
        public Dictionary<int, int> Correctas { get; set; }
    }

}
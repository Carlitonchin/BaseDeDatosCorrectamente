using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models.ParaLasVistas
{
    public class ProcedenciaCursosItemView
    {
        private ApplicationDbContext _dbContext;
        public ProcedenciaCursosItemView(Procedencia procedencia)
        {
            _dbContext = new ApplicationDbContext();

            Correctas = new Dictionary<int, int>();
            Total = new Dictionary<int, int>();

            Procedencia = procedencia;

            Prepare();
        }

        private void Prepare()
        {
            var cursos = _dbContext.Cursoes.ToList();
            var procedenciaEstudiantes = _dbContext.Users.Where(e => 
                    e.ProcedenciaID == Procedencia.ProcedenciaID).ToList();

            foreach (var curso in cursos)
            {
                var testCursos = curso.TestCursos;
                int correctas = 0;
                int total = 0;

                foreach (var testCurso in testCursos)
                {
                    var test = testCurso.Test;
                    var testEstudiantes = test.TestEstudiantes.Where(
                        te => te.estudiante.ProcedenciaID == Procedencia.ProcedenciaID);

                    foreach (var testEstudiante in testEstudiantes)
                    {
                        foreach (var respuestaMarcada in testEstudiante.RespuestasMarcadas)
                        {
                            if (respuestaMarcada.Respuesta.RespuestaCategorias?.Count > 0)
                                correctas++;
                        }

                        foreach (var respuestaEscrita in testEstudiante.RespuestasEscritas)
                        {
                            Pregunta p = respuestaEscrita.Pregunta;
                            var r = respuestaEscrita.Respuesta != null ? respuestaEscrita.Respuesta : "";

                            var bien = p.Respuestas.FirstOrDefault(m => m.Texto.ToUpper() == r.ToUpper() && m.RespuestaCategorias?.Count > 0);
                            if (bien != null)
                                correctas++;
                        }

                        total += testEstudiante.test.TestPreguntas.Count;
                    }
                }

                Correctas[curso.CursoID] = correctas;
                Total[curso.CursoID] = total;
            }
        }

        public Procedencia Procedencia { get; set; }
        public Dictionary<int, int> Correctas { get; set; }
        public Dictionary<int, int> Total { get; set; }
    }
}
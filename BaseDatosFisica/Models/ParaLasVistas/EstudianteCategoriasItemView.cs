using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models.ParaLasVistas
{
    public class EstudianteCategoriasItemView
    {
        private ApplicationDbContext _dbContext;
        public EstudianteCategoriasItemView(ApplicationUser estudiante)
        {
            _dbContext = new ApplicationDbContext();

            Correctas = new Dictionary<int, int>();

            Total = new Dictionary<int, int>();

            Estudiante = estudiante;

            Prepare();
        }

        private void Prepare()
        {
            foreach (var categoria in _dbContext.Categorias)
            {
                Correctas[categoria.CategoriaID] = Total[categoria.CategoriaID] = 0;
            }

            var testEstudiantes = _dbContext.TestsEstudiantes.Where(te => te.Id == Estudiante.Id).ToList();
            foreach (var testEstudiante in testEstudiantes)
            {
                var test = testEstudiante.test;

                foreach (var testPregunta in test.TestPreguntas)
                {
                    var pregunta = testPregunta.Pregunta;
                    foreach (var respuesta in pregunta.Respuestas)
                    {
                        foreach (var respuestaCategoria in respuesta?.RespuestaCategorias)
                        {
                            var categoria = respuestaCategoria.Categoria;
                            Total[categoria.CategoriaID]++;
                        }
                    }
                }

                foreach (var respuestaMarcada in testEstudiante.RespuestasMarcadas)
                {
                    foreach (var respuestaCategoria in respuestaMarcada.Respuesta.RespuestaCategorias)
                        Correctas[respuestaCategoria.CategoriaID]++;
                }

                foreach (var respuestaEscrita in testEstudiante.RespuestasEscritas)
                {
                    var texto = respuestaEscrita.Respuesta;
                    var pregunta = respuestaEscrita.Pregunta;

                    var respuestaCorrecta = pregunta.Respuestas.FirstOrDefault(r => r.Texto.ToUpper() == texto.ToUpper());

                    if (respuestaCorrecta != null)
                        foreach (var categoria in respuestaCorrecta.RespuestaCategorias)
                            Correctas[categoria.CategoriaID]++;
                }
            }
        }

        public ApplicationUser Estudiante { get; set; }
        public Dictionary<int, int> Correctas { get; set; }
        public Dictionary<int, int> Total { get; set; }
    }
}
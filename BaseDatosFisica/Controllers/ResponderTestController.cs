using BaseDatosFisica.Models;
using BaseDatosFisica.Models.ParaLasVistas;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaseDatosFisica.Controllers
{
    public class ResponderTestController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
       
        // GET: ResponderTest
        public ActionResult Index()
        {
            return View(db.Tests.Where(m=>m.Visible));
        }

        // GET: ResponderTest/Details/5
        
        [Authorize]
        public ActionResult RespondeTest(int? idTest)
        {
            if(idTest == null )
                return HttpNotFound();

            Test test = db.Tests.Find(idTest);
            if (test == null)
                return HttpNotFound();
            
            TestEstudiante te = new TestEstudiante();
            ApplicationUser user =  db.Users.FirstOrDefault(m => User.Identity.Name == m.UserName);

            if (user == null)
                return Content("Error");
           
            return View(test);
        }

        [Authorize]
        public ActionResult ResponderPreguntas(int? idTest)
        {
            Test test = db.Tests.Find(idTest);
            if (test == null)
                return Content("error: test no encontrado");

            string userid = db.Users.Where(m => m.UserName == User.Identity.Name).ToList()[0].Id;
            IEnumerable<TestEstudiante> lista = db.TestsEstudiantes.Where(m => m.Id == userid && m.TestID == idTest);
            TestEstudiante test_est = new TestEstudiante();

            if (lista.Count() == 0)
            {
                test_est.Id = userid;
                test_est.TestID = (int)idTest;

                db.TestsEstudiantes.Add(test_est);
                db.SaveChanges();
            }

            ResponderTestViewModel resp_test = new ResponderTestViewModel();
            resp_test.test = test;
            if (test.TestPreguntas.Count == 0)
                return Content("error: el test no tiene preguntas");

            // seleccionar la pregunta que toca en el momento
            List<TestPregunta> preguntas = test.TestPreguntas.ToList();
           
            string estudianteID = db.Users.FirstOrDefault(m => m.UserName == User.Identity.Name).Id;
            int i = 0;
            while (i < preguntas.Count)
            {

                while (i != preguntas.Count && (db.RespuestasEscritas.ToList().Where(m => m.PreguntaID == preguntas[i].PreguntaID
                        && m.TestEstudiante.Id == estudianteID && m.TestEstudiante.TestID == idTest).Count() != 0
                        || db.RespuestasMarcadas.ToList().Where(m => m.Respuesta.PreguntaID == preguntas[i].PreguntaID
                        && m.TestEstudiante.Id == estudianteID && m.TestEstudiante.TestID == idTest).Count() != 0))
                { i++; }

                if (i == preguntas.Count)
                    break;
                int idP = preguntas[i].PreguntaID;
                var etp = db.EstudiantePreguntaTest.FirstOrDefault(m => m.IdEstudiante == estudianteID && m.PreguntaID == idP && m.TestID == idTest);
                if (etp == null)
                    break;

                else if (!etp.TiempoAgotado)
                    break;

                else
                    i++;
                    
            }
            if (i == preguntas.Count)
                return RedirectToAction("Calificaciones", new { idEstudiante = estudianteID });

            resp_test.pregunta = preguntas[i].Pregunta;
            var estudianteTestPregunta = db.EstudiantePreguntaTest.FirstOrDefault(m => m.TestID == idTest && m.IdEstudiante == estudianteID && m.PreguntaID == resp_test.pregunta.PreguntaID);
            if(estudianteTestPregunta == null)
            {
                 estudianteTestPregunta = new EstudiantePreguntaTest() {

                    TestID = (int)idTest,
                    IdEstudiante = estudianteID,
                    PreguntaID = resp_test.pregunta.PreguntaID,
                    Inicio = DateTime.Now,
                    TiempoAgotado = false

                };

                db.EstudiantePreguntaTest.Add(estudianteTestPregunta);
                db.SaveChanges();
            }


            int tiempoPrevio = 0;
            if (resp_test.pregunta.Introduccion)
                tiempoPrevio = (int)resp_test.pregunta.TiempoPrevio;

            resp_test.TiempoRestante = resp_test.pregunta.Tiempo + tiempoPrevio - DateTime.Now.Subtract(estudianteTestPregunta.Inicio).TotalSeconds;
           

            if(resp_test.TiempoRestante <= 0)
            {
                var e = db.EstudiantePreguntaTest.FirstOrDefault(m => m.TestID == idTest && m.IdEstudiante == estudianteID && m.PreguntaID == resp_test.pregunta.PreguntaID);
                e.TiempoAgotado = true;
                db.Entry(e).State = EntityState.Modified;
                db.SaveChanges();

                return View("TiempoAgotado", idTest);
            }
            if (resp_test.TiempoRestante - resp_test.pregunta.Tiempo >= 0)
                return View("Introduccion", resp_test);

                return View(resp_test);
        }
        
        [HttpPost]
        [Authorize]
        public ActionResult ResponderPreguntas(int? idPregunta, int? idTest, string respuesta_actual, IEnumerable<int> idRespuestas)
        {
            string userid = db.Users.Where(m => m.UserName == User.Identity.Name).ToList()[0].Id;
            IEnumerable<TestEstudiante> lista = db.TestsEstudiantes.Where(m => m.Id == userid && m.TestID == idTest);
            TestEstudiante test_est = new TestEstudiante();

            if (lista.Count() == 0)
            {
                return Content("Error");
                
            }
            else
            {
                test_est = lista.ToList().First();
                if (test_est == null)
                    return Content("TestEstudiante null");
            }

            if (idRespuestas != null)
            {
                if (idRespuestas.Count() > 1)
                    return Content("Error: Solo se puede marcar una respuesta");

                int idrespuesta = idRespuestas.First();

                RespuestaMarcada respuesta = new RespuestaMarcada();

                respuesta.TestEstudianteID = test_est.TestEstudianteID;
                respuesta.RespuestaID = idrespuesta;

                var respuesta_ = db.Respuestas.Find(idrespuesta);
                if (respuesta_ == null)
                    return Content("Esa Respuesta no existe");

                if (db.RespuestasMarcadas.ToList().Where(m => m.Respuesta.PreguntaID == respuesta_.PreguntaID &&
                                m.TestEstudianteID == respuesta.TestEstudianteID).Count() > 0)
                {
                    return Content("Ya has respondido esta pregunta");
                }

                db.RespuestasMarcadas.Add(respuesta);
                db.SaveChanges();
            }
            else
            {
                RespuestaEscrita respuesta = new RespuestaEscrita();

                respuesta.PreguntaID = (int)idPregunta;
                respuesta.TestEstudianteID = test_est.TestEstudianteID;
                respuesta.Respuesta = respuesta_actual;

                if (db.RespuestasEscritas.ToList().Where(m => m.PreguntaID == respuesta.PreguntaID &&
                                m.TestEstudianteID == respuesta.TestEstudianteID).Count() > 0)
                {
                    return Content("Ya has respondido esta pregunta");
                }

                db.RespuestasEscritas.Add(respuesta);
                db.SaveChanges();
            }

            ResponderTestViewModel resp_test = new ResponderTestViewModel();

            //Buscar la proxima pregunta
            /*Test test_actual = db.Tests.Where(m => m.TestID == (int)idTest).ToList()[0];
            int i = 0;
            while (test_actual.TestPreguntas.ToList()[i++].PreguntaID != idPregunta) { }

            if (i == test_actual.TestPreguntas.Count)
                return RedirectToAction("Calificaciones", new { idEstudiante = userid});

            Pregunta prox_pregunta = test_actual.TestPreguntas.ToList()[i].Pregunta;
            resp_test.pregunta = prox_pregunta;
            resp_test.test = test_actual;

            return View(resp_test);*/
            return RedirectToAction("ResponderPreguntas", new { idTest = idTest });
        }

        [Authorize]
        public ActionResult Calificaciones(string idEstudiante)
        {
            if (idEstudiante == null)
                return Content("Error");

            var user = db.Users.Find(idEstudiante);
            if (user == null)
                return Content("Error");

            if (!User.IsInRole("admin") && user.UserName != User.Identity.Name)
                return Content("Error");

            return View(user.TestsEstudiante.ToList());
        }

        public ActionResult CalificacionesEstudiante()
        {
            var estudiante = db.Users.FirstOrDefault(e => e.UserName == User.Identity.Name);
            if (estudiante == null)
                return Content("Estudiante nulo");

            return RedirectToAction("Calificaciones", new { idEstudiante = estudiante.Id });
        }

        public ActionResult Nota(int? idTestEstudiante)
        {
            if (idTestEstudiante == null)
                return Content("Error");
            

            var TestEstudiante = db.TestsEstudiantes.Find(idTestEstudiante);

            var user = db.Users.Find(TestEstudiante.Id);
            if (user == null)
                return Content("Error");

            if (!User.IsInRole("admin") && user.UserName != User.Identity.Name)
                return Content("Error");

            if (TestEstudiante == null)
                return Content("Error");

            TestEstudiantesCategoriasViewModel tec = new TestEstudiantesCategoriasViewModel();
            tec.categorias = db.Categorias.ToList();
            tec.testEstudiante = TestEstudiante;

            return View(tec);
            
        }

        // POST: ResponderTest/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ResponderTest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ResponderTest/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ResponderTest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ResponderTest/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

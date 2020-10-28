using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaseDatosFisica.Models;
using BaseDatosFisica.Models.ParaLasVistas;

namespace BaseDatosFisica.Controllers
{
    public class TestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tests
        public ActionResult Index()
        {
            return View(db.Cursoes.ToList());
        }

        public ActionResult QuitarDelCurso(int? id)
        {
            if (id == null)
                return HttpNotFound();

            TestCurso tc = db.TestCursoes.Find(id);
            if (tc == null)
                return Content("Intente no navegar a mano");


            TestsCursosController tcController = new TestsCursosController();
            tcController.DeleteConfirmed((int)id);

            return RedirectToAction("Test", new { id = tc.TestID });
        }

        public ActionResult MostrarPregunta(int? idTest, int? idPregunta)
        {
            if (idTest == null || idPregunta == null)
                return Content("algo anda mal, evite navegar a mano");

            Pregunta p = db.Preguntas.Find(idPregunta);
            Test t = db.Tests.Find(idTest);

            if(p == null || t == null)
                return Content("algo anda mal, evite navegar a mano");

            ViewBag.idTest = idTest;
            
            return View(p);
        }
        public ActionResult QuitarPregunta(int? id)
        {
            if (id == null)
                return Content("Algo anda mal, intente no navegar a mano");

            TestPregunta tp = db.TestPreguntas.Find(id);

            if (id == null)
                return Content("Test y preguntas no encontrados, intente no navegar a mano");

            TestsPreguntasController tpController = new TestsPreguntasController();
            tpController.DeleteConfirmed((int)id);

            return RedirectToAction("Test", new { id = tp.TestID });
        }

        public ActionResult AnhadirTest(int? idCurso)
        {
            if (idCurso == null)
                return Content("Algo anda mal, intente no navegar a mano");

            Curso curso = db.Cursoes.Find(idCurso);
            if(curso == null)
                return Content("Algo anda mal, intente no navegar a mano");

            AnhadirTestAlCurso atc = new AnhadirTestAlCurso();
            atc.curso = curso;
            atc.tests = from t in db.Tests.ToList() where curso.TestCursos.FirstOrDefault(m=>m.TestID == t.TestID)==null select t;
            

            return View(atc);
        }

        [HttpPost]
        public ActionResult AnhadirTest(int? idCurso, int? idTest)
        {
            if (idCurso == null || idTest == null)
                return Content("algo anda mal, evita navegar a mano");



            TestCurso tc = new TestCurso();
            tc.TestID = (int)idTest;
            tc.CursoID = (int)idCurso;

            TestsCursosController tcController = new TestsCursosController();
            tcController.Create(tc);

            return RedirectToAction("Index");

        }

        public ActionResult Visibilidad(int? idTest)
        {
            if(idTest == null)
                return Content("algo anda mal, evita navegar a mano");

            Test test = db.Tests.Find(idTest);
            if(test == null)
                return Content("algo anda mal, evita navegar a mano");

            test.Visible = !test.Visible;
            db.Entry(test).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Test", new { id = idTest });
        }
        public ActionResult Test(int? id)
        {
            if (id == null)
                return HttpNotFound();

            Test t = db.Tests.Find(id);
            if (t == null)
                return Content("Test inexistente");
            
            return View(t);
        }

        public ActionResult AgregarPregunta(int? idTest, IEnumerable<Pregunta> preguntasFiltradas)
        {
            if (idTest == null)
                return Content("Algo anda mal, intente no navegar a mano");

            Test test = db.Tests.Find(idTest);

            if (test == null)
                return Content("Algo anda mal, intente no navegar a mano");

            IEnumerable<Pregunta> preguntas = null;

            //db.Preguntas.RemoveRange(db.Preguntas.Where(p => p.Nombre == null));
            //db.SaveChanges();

            if (preguntasFiltradas == null)
                preguntas = from p in db.Preguntas.ToList()
                            where test.TestPreguntas.FirstOrDefault(m => m.PreguntaID == p.PreguntaID) == null
                            select p;

            else
                preguntas = preguntasFiltradas;

            AgregarPreguntaAlTest a = new AgregarPreguntaAlTest();
            a.test = test;
            a.preguntas = preguntas;
            a.FiltroCategorias = db.Categorias.ToList();
            
            return View("AgregarPregunta" , a);
        }

        [HttpPost]
        public ActionResult Filtrar(string Nombre, IEnumerable<int> idCategorias, int? idTest)
        {
            if (idTest == null)
                return Content("Algo anda mal");

            Test t = db.Tests.Find(idTest);
            if (t == null)
                return Content("Test inexistente");

            if (Nombre == "" && idCategorias == null)
                return RedirectToAction("AgregarPregunta", new { idTest = idTest});

            IEnumerable<Pregunta> preguntasFiltradas = null;
            
            preguntasFiltradas = from p in db.Preguntas.ToList() where t.TestPreguntas.FirstOrDefault(n=>n.PreguntaID == p.PreguntaID) == null && (idCategorias == null || ( p.Respuestas!= null && p.Respuestas.FirstOrDefault(c => c.RespuestaCategorias != null && idCategorias.All(h=>c.RespuestaCategorias.FirstOrDefault(l=>l.CategoriaID == h)!=null)) != null)) && (Nombre == "" || p.Nombre == Nombre) select p;

            return AgregarPregunta(idTest, preguntasFiltradas);
            
        }

        [HttpPost]
        public ActionResult AgregarPregunta(int? idTest, IEnumerable<int> preguntas)
        {
            if (idTest == null)
                return Content("error");

            if(preguntas == null)
                return RedirectToAction("AgregarPregunta", new { idTest = idTest });


            foreach (var p in preguntas)
            {
                TestPregunta tp = new TestPregunta();
                tp.PreguntaID = p;
                tp.TestID = (int)idTest;

                db.TestPreguntas.Add(tp);
                db.SaveChanges();
            }


            return RedirectToAction("AgregarPregunta", new { idTest = idTest });
        }

        // GET: Tests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = db.Tests.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        // GET: Tests/Create
        public ActionResult Create()
        {
            ViewBag.Cursos = new SelectList(db.Cursoes, "CursoID", "Anho");
           
            return View();
        }

        // POST: Tests/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TestID,Nombre")] Test test, int? idCurso)
        {
            if (idCurso == null)
                return Content("Algo anda mal");

            Curso c = db.Cursoes.Find((int)idCurso);
            if (c == null)
                return Content("Curso inexistente");

            

            if (ModelState.IsValid)
            {
                db.Tests.Add(test);
                db.SaveChanges();

                TestCurso tc = new TestCurso();
                tc.CursoID = c.CursoID;
                tc.TestID = test.TestID;
                db.TestCursoes.Add(tc);
                db.SaveChanges();
               
                return RedirectToAction("Index");
            }

            return View(test);
        }

        // GET: Tests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = db.Tests.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            
            return View(test);
        }

     
        // POST: Tests/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TestID,Nombre")] Test test)
        {
            if (ModelState.IsValid)
            {
                db.Entry(test).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Test", new { id = test.TestID});
            }
            return View(test);
        }

        // GET: Tests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = db.Tests.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View("Delete", test);
        }

        // POST: Tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Test test = db.Tests.Find(id);
            db.Tests.Remove(test);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaseDatosFisica.Models;

namespace BaseDatosFisica.Controllers
{
    public class TestsPreguntasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TestsPreguntas
        public ActionResult Index()
        {
            var testPreguntas = db.TestPreguntas.Include(t => t.Pregunta).Include(t => t.Test);
            return View(testPreguntas.ToList());
        }

        // GET: TestsPreguntas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestPregunta testPregunta = db.TestPreguntas.Find(id);
            if (testPregunta == null)
            {
                return HttpNotFound();
            }
            return View(testPregunta);
        }

        // GET: TestsPreguntas/Create
        public ActionResult Create()
        {
            ViewBag.PreguntaID = new SelectList(db.Preguntas, "PreguntaID", "Nombre");
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "Nombre");
            return View();
        }

        // POST: TestsPreguntas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TestPreguntaID,PreguntaID,TestID")] TestPregunta testPregunta)
        {
            if (ModelState.IsValid)
            {
                db.TestPreguntas.Add(testPregunta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PreguntaID = new SelectList(db.Preguntas, "PreguntaID", "Nombre", testPregunta.PreguntaID);
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "Nombre", testPregunta.TestID);
            return View(testPregunta);
        }

        // GET: TestsPreguntas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestPregunta testPregunta = db.TestPreguntas.Find(id);
            if (testPregunta == null)
            {
                return HttpNotFound();
            }
            ViewBag.PreguntaID = new SelectList(db.Preguntas, "PreguntaID", "Nombre", testPregunta.PreguntaID);
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "Nombre", testPregunta.TestID);
            return View(testPregunta);
        }

        // POST: TestsPreguntas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TestPreguntaID,PreguntaID,TestID")] TestPregunta testPregunta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testPregunta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PreguntaID = new SelectList(db.Preguntas, "PreguntaID", "Nombre", testPregunta.PreguntaID);
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "Nombre", testPregunta.TestID);
            return View(testPregunta);
        }

        // GET: TestsPreguntas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestPregunta testPregunta = db.TestPreguntas.Find(id);
            if (testPregunta == null)
            {
                return HttpNotFound();
            }
            return View(testPregunta);
        }

        // POST: TestsPreguntas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestPregunta testPregunta = db.TestPreguntas.Find(id);
            db.TestPreguntas.Remove(testPregunta);
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

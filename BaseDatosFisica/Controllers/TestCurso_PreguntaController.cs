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
    public class TestCurso_PreguntaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TestCurso_Pregunta
        public ActionResult Index()
        {
            return View(db.TestCurso_Pregunta.ToList());
        }

        // GET: TestCurso_Pregunta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestCurso_Pregunta testCurso_Pregunta = db.TestCurso_Pregunta.Find(id);
            if (testCurso_Pregunta == null)
            {
                return HttpNotFound();
            }
            return View(testCurso_Pregunta);
        }

        // GET: TestCurso_Pregunta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestCurso_Pregunta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TestCursoPreguntaID,respuesta")] TestCurso_Pregunta testCurso_Pregunta)
        {
            if (ModelState.IsValid)
            {
                db.TestCurso_Pregunta.Add(testCurso_Pregunta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(testCurso_Pregunta);
        }

        // GET: TestCurso_Pregunta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestCurso_Pregunta testCurso_Pregunta = db.TestCurso_Pregunta.Find(id);
            if (testCurso_Pregunta == null)
            {
                return HttpNotFound();
            }
            return View(testCurso_Pregunta);
        }

        // POST: TestCurso_Pregunta/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TestCursoPreguntaID,respuesta")] TestCurso_Pregunta testCurso_Pregunta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testCurso_Pregunta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(testCurso_Pregunta);
        }

        // GET: TestCurso_Pregunta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestCurso_Pregunta testCurso_Pregunta = db.TestCurso_Pregunta.Find(id);
            if (testCurso_Pregunta == null)
            {
                return HttpNotFound();
            }
            return View(testCurso_Pregunta);
        }

        // POST: TestCurso_Pregunta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestCurso_Pregunta testCurso_Pregunta = db.TestCurso_Pregunta.Find(id);
            db.TestCurso_Pregunta.Remove(testCurso_Pregunta);
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

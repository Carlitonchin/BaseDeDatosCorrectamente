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
    public class TestsCursosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TestsCursos
        public ActionResult Index()
        {
            var testCursoes = db.TestCursoes.Include(t => t.Curso).Include(t => t.Test);
            return View(testCursoes.ToList());
        }

        // GET: TestsCursos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestCurso testCurso = db.TestCursoes.Find(id);
            if (testCurso == null)
            {
                return HttpNotFound();
            }
            return View(testCurso);
        }

        // GET: TestsCursos/Create
        public ActionResult Create()
        {
            
            ViewBag.CursoID = new SelectList(db.Cursoes, "CursoID", "Anho");
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "Nombre");
            return View();
        }

        // POST: TestsCursos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TestCursoID,CursoID,TestID")] TestCurso testCurso)
        {
            
            if (ModelState.IsValid)
            {
                db.TestCursoes.Add(testCurso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CursoID = new SelectList(db.Cursoes, "CursoID", "CursoID", testCurso.CursoID);
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "Nombre", testCurso.TestID);
            return View(testCurso);
        }

        // GET: TestsCursos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestCurso testCurso = db.TestCursoes.Find(id);
            if (testCurso == null)
            {
                return HttpNotFound();
            }
            ViewBag.CursoID = new SelectList(db.Cursoes, "CursoID", "CursoID", testCurso.CursoID);
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "Nombre", testCurso.TestID);
            return View(testCurso);
        }

        // POST: TestsCursos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TestCursoID,CursoID,TestID")] TestCurso testCurso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testCurso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CursoID = new SelectList(db.Cursoes, "CursoID", "CursoID", testCurso.CursoID);
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "Nombre", testCurso.TestID);
            return View(testCurso);
        }

        // GET: TestsCursos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestCurso testCurso = db.TestCursoes.Find(id);
            if (testCurso == null)
            {
                return HttpNotFound();
            }
            return View(testCurso);
        }

        // POST: TestsCursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestCurso testCurso = db.TestCursoes.Find(id);
            db.TestCursoes.Remove(testCurso);
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

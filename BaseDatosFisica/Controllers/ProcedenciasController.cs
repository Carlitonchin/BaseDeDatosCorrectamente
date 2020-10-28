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
    public class ProcedenciasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Procedencias
        public ActionResult Index()
        {
            return View(db.Procedencias.ToList());
        }

        // GET: Procedencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Procedencia procedencia = db.Procedencias.Find(id);
            if (procedencia == null)
            {
                return HttpNotFound();
            }
            return View(procedencia);
        }

        // GET: Procedencias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Procedencias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProcedenciaID,valor")] Procedencia procedencia)
        {
            if (ModelState.IsValid)
            {
                db.Procedencias.Add(procedencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(procedencia);
        }

        // GET: Procedencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Procedencia procedencia = db.Procedencias.Find(id);
            if (procedencia == null)
            {
                return HttpNotFound();
            }
            return View(procedencia);
        }

        // POST: Procedencias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProcedenciaID,valor")] Procedencia procedencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(procedencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(procedencia);
        }

        // GET: Procedencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Procedencia procedencia = db.Procedencias.Find(id);
            if (procedencia == null)
            {
                return HttpNotFound();
            }
            return View(procedencia);
        }

        // POST: Procedencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Procedencia procedencia = db.Procedencias.Find(id);
            db.Procedencias.Remove(procedencia);
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

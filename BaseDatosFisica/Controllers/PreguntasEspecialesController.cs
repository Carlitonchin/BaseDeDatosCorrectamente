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
    public class PreguntasEspecialesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PreguntasEspeciales
        public ActionResult Index()
        {
            return View(db.Preguntas.ToList());
        }

        // GET: PreguntasEspeciales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreguntaEspecial preguntaEspecial = (PreguntaEspecial)db.Preguntas.Find(id);
            if (preguntaEspecial == null)
            {
                return HttpNotFound();
            }
            return View(preguntaEspecial);
        }

        // GET: PreguntasEspeciales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PreguntasEspeciales/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PreguntaID,Tipo,Nombre,Enunciado,Imagen,Tiempo,TiempoPrevio,ImagenPrevia,EnunciadoPrevio")] PreguntaEspecial preguntaEspecial)
        {
            if (ModelState.IsValid)
            {
                db.Preguntas.Add(preguntaEspecial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(preguntaEspecial);
        }

        // GET: PreguntasEspeciales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreguntaEspecial preguntaEspecial = (PreguntaEspecial)db.Preguntas.Find(id);
            if (preguntaEspecial == null)
            {
                return HttpNotFound();
            }
            return View(preguntaEspecial);
        }

        // POST: PreguntasEspeciales/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PreguntaID,Tipo,Nombre,Enunciado,Imagen,Tiempo,TiempoPrevio,ImagenPrevia,EnunciadoPrevio")] PreguntaEspecial preguntaEspecial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(preguntaEspecial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(preguntaEspecial);
        }

        // GET: PreguntasEspeciales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreguntaEspecial preguntaEspecial = (PreguntaEspecial)db.Preguntas.Find(id);
            if (preguntaEspecial == null)
            {
                return HttpNotFound();
            }
            return View(preguntaEspecial);
        }

        // POST: PreguntasEspeciales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PreguntaEspecial preguntaEspecial = (PreguntaEspecial)db.Preguntas.Find(id);
            db.Preguntas.Remove(preguntaEspecial);
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

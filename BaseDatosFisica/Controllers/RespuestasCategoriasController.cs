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
    public class RespuestasCategoriasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RespuestasCategorias
        public ActionResult Index()
        {
            var respuestaCategorias = db.RespuestaCategorias.Include(r => r.Categoria).Include(r => r.Respuesta);
            return View(respuestaCategorias.ToList());
        }

        // GET: RespuestasCategorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RespuestaCategoria respuestaCategoria = db.RespuestaCategorias.Find(id);
            if (respuestaCategoria == null)
            {
                return HttpNotFound();
            }
            return View(respuestaCategoria);
        }

        // GET: RespuestasCategorias/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "CategoriaNombre");
            ViewBag.RespuestaID = new SelectList(db.Respuestas, "RespuestaID", "Texto");
            return View();
        }

        // POST: RespuestasCategorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RespuestaCategoriaID,RespuestaID,CategoriaID")] RespuestaCategoria respuestaCategoria)
        {
            if (ModelState.IsValid)
            {
                db.RespuestaCategorias.Add(respuestaCategoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "CategoriaNombre", respuestaCategoria.CategoriaID);
            ViewBag.RespuestaID = new SelectList(db.Respuestas, "RespuestaID", "Texto", respuestaCategoria.RespuestaID);
            return View(respuestaCategoria);
        }

        // GET: RespuestasCategorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RespuestaCategoria respuestaCategoria = db.RespuestaCategorias.Find(id);
            if (respuestaCategoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "CategoriaNombre", respuestaCategoria.CategoriaID);
            ViewBag.RespuestaID = new SelectList(db.Respuestas, "RespuestaID", "Texto", respuestaCategoria.RespuestaID);
            return View(respuestaCategoria);
        }

        // POST: RespuestasCategorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RespuestaCategoriaID,RespuestaID,CategoriaID")] RespuestaCategoria respuestaCategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(respuestaCategoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "CategoriaNombre", respuestaCategoria.CategoriaID);
            ViewBag.RespuestaID = new SelectList(db.Respuestas, "RespuestaID", "Texto", respuestaCategoria.RespuestaID);
            return View(respuestaCategoria);
        }

        // GET: RespuestasCategorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RespuestaCategoria respuestaCategoria = db.RespuestaCategorias.Find(id);
            if (respuestaCategoria == null)
            {
                return HttpNotFound();
            }
            return View(respuestaCategoria);
        }

        // POST: RespuestasCategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RespuestaCategoria respuestaCategoria = db.RespuestaCategorias.Find(id);
            db.RespuestaCategorias.Remove(respuestaCategoria);
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

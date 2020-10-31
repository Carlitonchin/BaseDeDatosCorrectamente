﻿using System;
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
    public class CategoriasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categorias
        public ActionResult Index()
        {
            return View(db.Categorias.ToList());
        }

        // GET: Categorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateDesdeRespuesta(int? idRespuesta, int? idTest)
        {
            if (idRespuesta == null)
                return Content("error");

            ViewBag.idRespuesta = idRespuesta;
            ViewBag.idTest = idTest;

            return View();
        }

        // POST: Categorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoriaID,CategoriaNombre")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Categorias.Add(categoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDesdeRespuesta([Bind(Include = "CategoriaID,CategoriaNombre")] Categoria categoria, int? idRespuesta, int? idTest)
        {
            if (idRespuesta == null)
                return Content("error");

            if (ModelState.IsValid)
            {
                db.Categorias.Add(categoria);
                db.SaveChanges();
                return RedirectToAction("AgregarCategoria", "Respuestas", new { idRespuesta = idRespuesta, idTest = idTest});
            }
            ViewBag.idRespuesta = idRespuesta;
            ViewBag.idTest = idTest;
            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.idRespuesta = -1;
            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public ActionResult EditDesdeRespuesta(int? idCategoria,int? idRespuesta, int? idTest)
        {
            if (idCategoria == null || idRespuesta == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(idCategoria);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.idRespuesta = idRespuesta;
            ViewBag.idTest = idTest;
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoriaID,CategoriaNombre")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
                return View(categoria);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDesdeRespuesta([Bind(Include = "CategoriaID,CategoriaNombre")] Categoria categoria, int? idRespuesta, int? idTest)
        {
            if (idRespuesta == null)
                return Content("error");

            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AgregarCategoria", "Respuestas", new { idRespuesta = idRespuesta, idTest = idTest });
            }


            ViewBag.idRespuesta = idRespuesta;
            ViewBag.idTest = idTest;
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categoria categoria = db.Categorias.Find(id);
            db.Categorias.Remove(categoria);
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

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
    public class PreguntasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Preguntas
        public ActionResult Index()
        {
           
            return View(db.Preguntas.ToList());
        }

        // GET: Preguntas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta pregunta = db.Preguntas.Find(id);
            if (pregunta == null)
            {
                return HttpNotFound();
            }
            return View(pregunta);
        }

        // GET: Preguntas/Create
        public ActionResult Create(int? Tiempo_, bool? Tipo_, string Enunciado_, string Nombre_, string Imagen_, bool? Introduccion_)
        {

            Pregunta pregunta = new Pregunta
            {
                Enunciado = Enunciado_,
                Nombre = Nombre_,
                Imagen = Imagen_
            };

            if (Tiempo_ != null)
                pregunta.Tiempo = (int)Tiempo_;

            if (Tipo_ != null)
                pregunta.Tipo = (bool)Tipo_;

            pregunta.Introduccion = Introduccion_;
            if (pregunta.Introduccion == null)
                pregunta.Introduccion = false;

            return View(pregunta);
        }

        [HttpPost]
        public ActionResult AgregarIntroduccion(int? Tiempo_, bool? Tipo_, string Enunciado_, string Nombre_, string Imagen_, bool? Introduccion_)
        {
            
            bool intr = !(bool)Introduccion_;
            
            return RedirectToAction("Create", new { Tiempo_ = Tiempo_, Tipo_ = Tipo_, Enunciado_ = Enunciado_, Nombre_ = Nombre_, Imagen_ = Imagen_, Introduccion_ = intr});
        }

        // POST: Preguntas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PreguntaID,Tipo,Nombre,Enunciado,Imagen,Tiempo, TiempoPrevio, ImagenPrevia, EnunciadoPrevio")] Pregunta pregunta)
        {
               
            if (ModelState.IsValid)
            {
                db.Preguntas.Add(pregunta);
                db.SaveChanges();
                return RedirectToAction("AgregarRespuesta", new { idPregunta = pregunta.PreguntaID });
            }

            return View(pregunta);
        }

        // GET: Preguntas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta pregunta = db.Preguntas.Find(id);
            if (pregunta == null)
            {
                return HttpNotFound();
            }
            return View("Edit", pregunta);
        }

        public ActionResult AgregarRespuesta(int? idPregunta)
        {
            if (idPregunta == null)
                return Content("algo anda mal, intenta no navegar a mano");

            Pregunta p = db.Preguntas.Find(idPregunta);

            if(p == null)
                return Content("algo anda mal, intenta no navegar a mano");

            return View(p);
        }

        public ActionResult QuitarRespuesta(int? idRespuesta, int? idPregunta)
        {
            if (idRespuesta == null || idPregunta == null)
                return Content("Algo anda mal, intente no navegar a mano");

            Respuesta r = db.Respuestas.Find(idRespuesta);

            if (r == null)
                return Content("Respuesta no encontrada, intente no navegar a mano");

            RespuestasController rcController = new RespuestasController();
            rcController.DeleteConfirmed((int)idRespuesta);

            return RedirectToAction("AgregarRespuesta", new { idPregunta = idPregunta });
        }

        // POST: Preguntas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PreguntaID,Tipo,Nombre,Enunciado,Imagen,Tiempo")] Pregunta pregunta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pregunta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pregunta);
        }

        // GET: Preguntas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta pregunta = db.Preguntas.Find(id);
            if (pregunta == null)
            {
                return HttpNotFound();
            }
            return View(pregunta);
        }

        // POST: Preguntas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pregunta pregunta = db.Preguntas.Find(id);
            db.Preguntas.Remove(pregunta);
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

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
    public class RespuestasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Respuestas
        public ActionResult Index()
        {
            var respuestas = db.Respuestas.Include(r => r.Pregunta);
            return View(respuestas.ToList());
        }

        // GET: Respuestas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuesta respuesta = db.Respuestas.Find(id);
            if (respuesta == null)
            {
                return HttpNotFound();
            }
            return View(respuesta);
        }

        // GET: Respuestas/Create
        public ActionResult Create(int? idPregunta)
        {
            if (idPregunta != null)
                ViewBag.PreguntaID = new SelectList(db.Preguntas, "PreguntaID", "Nombre", idPregunta);
            else
                ViewBag.PreguntaID = new SelectList(db.Preguntas, "PreguntaID", "Nombre");

            ViewBag.idPregunta = idPregunta;
            return View();
        }

        // POST: Respuestas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RespuestaID,Texto,Imagen,PreguntaID")] Respuesta respuesta)
        {

            if (respuesta == null)
                return HttpNotFound();

            /* Pregunta p = db.Preguntas.Find(respuesta.PreguntaID);
             if (p == null)
                 return HttpNotFound();

             respuesta.Pregunta = p;
             if (p.Respuestas == null)
                 p.Respuestas = new List<Respuesta>();*/



            if (ModelState.IsValid)
            {

                db.Respuestas.Add(respuesta);
                db.SaveChanges();
                /* respuesta = db.Respuestas.Find(respuesta.RespuestaID);
                 p.Respuestas.Add(respuesta);
                 db.Entry(p).State = EntityState.Modified;
                 db.SaveChanges();*/
                return RedirectToAction("AgregarRespuesta", "Preguntas", new { idPregunta = respuesta.PreguntaID });
            }

            ViewBag.PreguntaID = new SelectList(db.Preguntas, "PreguntaID", "Nombre", respuesta.PreguntaID);
            return View(respuesta);
        }

        public ActionResult AgregarCategoria(int? idRespuesta)
        {
            if (idRespuesta == null)
                return Content("algo anda mal, intenta no navegar a mano");

            Respuesta r = db.Respuestas.Find(idRespuesta);

            if (r == null)
                return Content("algo anda mal, intenta no navegar a mano");

            IEnumerable<Categoria> categorias = from cat in db.Categorias.ToList() where r.RespuestaCategorias.FirstOrDefault(m => m.CategoriaID == cat.CategoriaID) == null select cat;
            AgregarCategoriasARespuesta a = new AgregarCategoriasARespuesta();
            a.categorias = categorias;
            a.respuesta = r;

            return View(a);
        }

        [HttpPost]
        public ActionResult AgregarCategoria(int? idRespuesta, IEnumerable<int> categorias)
        {
            if (idRespuesta == null || categorias == null)
                return Content("Algo anda mal, intente no navegar a mano");


            foreach (var c in categorias)
            {
                RespuestaCategoria rc = new RespuestaCategoria();
                rc.CategoriaID = c;
                rc.RespuestaID = (int)idRespuesta;

                db.RespuestaCategorias.Add(rc);
                db.SaveChanges();
            }
            
            return RedirectToAction("AgregarCategoria", new { idRespuesta = idRespuesta });
        }

        public ActionResult QuitarCategoria(int? id)
        {
            if (id == null)
                return Content("Algo anda mal, intente no navegar a mano");

            RespuestaCategoria rc = db.RespuestaCategorias.Find(id);

            if (id == null)
                return Content("Respuesta y categoria no encontrados, intente no navegar a mano");

            RespuestasCategoriasController rcController = new RespuestasCategoriasController();
            rcController.DeleteConfirmed((int)id);

            return RedirectToAction("Respuesta", new { idRespuesta = rc.RespuestaID });
        }

        public ActionResult Respuesta(int? idRespuesta)
        {
            if (idRespuesta == null)
                return Content("Algo anda mal, intente no navegar a mano");

            Respuesta resp = db.Respuestas.Find(idRespuesta);

            if(resp == null)
                return Content("Algo anda mal, intente no navegar a mano");

            return View(resp);
        }
        // GET: Respuestas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuesta respuesta = db.Respuestas.Find(id);
            if (respuesta == null)
            {
                return HttpNotFound();
            }
            ViewBag.PreguntaID = new SelectList(db.Preguntas, "PreguntaID", "Nombre", respuesta.PreguntaID);
            return View(respuesta);
        }

        // POST: Respuestas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RespuestaID,Texto,Imagen,PreguntaID")] Respuesta respuesta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(respuesta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Respuesta", new { idRespuesta = respuesta.RespuestaID });
            }
            ViewBag.PreguntaID = new SelectList(db.Preguntas, "PreguntaID", "Nombre", respuesta.PreguntaID);
            return View(respuesta);
        }

        // GET: Respuestas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuesta respuesta = db.Respuestas.Find(id);
            if (respuesta == null)
            {
                return HttpNotFound();
            }
            return View(respuesta);
        }

        // POST: Respuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Respuesta respuesta = db.Respuestas.Find(id);
            db.Respuestas.Remove(respuesta);
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

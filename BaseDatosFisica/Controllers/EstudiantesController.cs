using BaseDatosFisica.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaseDatosFisica.Controllers
{
    [Authorize]
    public class EstudiantesController : Controller
    {
        // GET: Estudiantes
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            if (!User.IsInRole("admin"))
                return Content("No tienes permisos de administrador");

            return View(db.Users.Where(m=>m.UserName != User.Identity.Name).ToList());
        }

        public ActionResult Delete(string idEstudiante)
        {
            if (idEstudiante == null)
                return Content("Error");

            var estudiante = db.Users.Find(idEstudiante);
            if (estudiante == null)
                return Content("Estudiante no encontrado");

            return View(estudiante);
        }

        
        public ActionResult DeleteConfirmed(string idEstudiante)
        {
            if (!User.IsInRole("admin"))
            {
                return Content("No tiene permisos de administrador");
            }

            var estudiante = db.Users.Find(idEstudiante);
            if (estudiante == null)
                return Content("Estudiante no encontrado");

            var testEstudiantes = estudiante.TestsEstudiante;
            db.TestsEstudiantes.RemoveRange(testEstudiantes);

            db.Users.Remove(estudiante);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
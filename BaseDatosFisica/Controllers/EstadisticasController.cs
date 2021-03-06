﻿using BaseDatosFisica.Models;
using BaseDatosFisica.Models.ParaLasVistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaseDatosFisica.Controllers
{
    public class EstadisticasController : Controller
    {
        private ApplicationDbContext _dbContext;
        public EstadisticasController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Estadisticas
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Estudiante()
        {
            var EstadisticaEstudiante = new EstadisticaEstudianteTestViewModel();
            
            return View(EstadisticaEstudiante);
        }

        public ActionResult Categoria()
        {
            var EstadisticaCategoria = new CategoriaEstadisticasViewModel();

            return View(EstadisticaCategoria);
        }

        public ActionResult Procedencia()
        {
            EstadisticaProcedenciaViewModel stats = new EstadisticaProcedenciaViewModel();
            return View(stats);
        }
    }
}
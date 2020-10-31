using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models.ParaLasVistas
{
    public class CategoriaEstadisticasViewModel
    {
        private ApplicationDbContext _dbContext;
        public CategoriaEstadisticasViewModel()
        {
            _dbContext = new ApplicationDbContext();

            Categorias = _dbContext.Categorias.ToList();

            EstudiantesCategorias = new List<EstudianteCategoriasItemView>();

            Prepare();
        }

        private void Prepare()
        {
            var estudiantes = _dbContext.Users.ToList();

            foreach (var estudiante in estudiantes)
            {
                EstudiantesCategorias.Add(new EstudianteCategoriasItemView(estudiante));
            }
        }

        public List<Categoria> Categorias { get; set; }
        public List<EstudianteCategoriasItemView> EstudiantesCategorias { get; set; }
    }
}
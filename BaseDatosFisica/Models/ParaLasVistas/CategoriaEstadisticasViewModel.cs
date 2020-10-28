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

            Prepare();
        }

        private void Prepare()
        {
            
        }

        public List<Categoria> Categorias { get; set; }
    }
}
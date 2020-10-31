using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models.ParaLasVistas
{
    public class EstadisticaProcedenciaViewModel
    {
        private ApplicationDbContext _dbContext;
        public EstadisticaProcedenciaViewModel()
        {
            _dbContext = new ApplicationDbContext();
            Procedencias = _dbContext.Procedencias.ToList();
            Cursos = _dbContext.Cursoes.ToList();
            Prepare();
        }

        private void Prepare()
        {
            ProcedenciaCursos = new List<ProcedenciaCursosItemView>();

            foreach (var item in Procedencias)
            {
                ProcedenciaCursos.Add(new ProcedenciaCursosItemView(item));
            }
        }

        public List<Procedencia> Procedencias { get; set; }
        public List<ProcedenciaCursosItemView> ProcedenciaCursos { get; set; }
        public List<Curso> Cursos { get; set; }
    }
}
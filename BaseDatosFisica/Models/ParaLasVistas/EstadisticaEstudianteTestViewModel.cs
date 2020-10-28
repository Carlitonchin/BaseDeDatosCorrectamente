using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseDatosFisica.Models.ParaLasVistas
{
    public class EstadisticaEstudianteTestViewModel
    {
        private ApplicationDbContext _dbContext;
        public EstadisticaEstudianteTestViewModel()
        {
            _dbContext = new ApplicationDbContext();

            Estudiantes = new List<EstudianteTestItemView>();

            Total = new Dictionary<int, int>();

            Tests = _dbContext.Tests.ToList();

            Prepare();
        }

        public void Prepare()
        {
            var estudiantes = _dbContext.Users.ToList();

            foreach (var estudiante in estudiantes)
            {
                Estudiantes.Add(new EstudianteTestItemView(estudiante));
            }

            foreach (var test in _dbContext.Tests.ToList())
                Total[test.TestID] = test.TestPreguntas.Count; 
        }
        
        public List<Test> Tests { get; set; }
        public List<EstudianteTestItemView> Estudiantes { get; set; }
        public Dictionary<int, int> Total { get; set; }
    }
}
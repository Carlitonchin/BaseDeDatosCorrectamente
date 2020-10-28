using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;

namespace BaseDatosFisica.Models
{
    // Puede agregar datos del perfil del usuario agregando más propiedades a la clase ApplicationUser. Para más información, visite http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int ProcedenciaID { get; set; }
        public virtual Procedencia procedencia { get; set; }
        public int NumeroOpcion { get; set; }
        public float Promedio { get; set; }
        public virtual ICollection<TestEstudiante> TestsEstudiante { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<BaseDatosFisica.Models.Curso> Cursoes { get; set; }

        public System.Data.Entity.DbSet<BaseDatosFisica.Models.Test> Tests { get; set; }

        public System.Data.Entity.DbSet<BaseDatosFisica.Models.TestCurso> TestCursoes { get; set; }

        public System.Data.Entity.DbSet<BaseDatosFisica.Models.Pregunta> Preguntas { get; set; }

        public System.Data.Entity.DbSet<BaseDatosFisica.Models.TestPregunta> TestPreguntas { get; set; }

        public System.Data.Entity.DbSet<BaseDatosFisica.Models.Respuesta> Respuestas { get; set; }

        public System.Data.Entity.DbSet<BaseDatosFisica.Models.Categoria> Categorias { get; set; }

        public System.Data.Entity.DbSet<BaseDatosFisica.Models.RespuestaCategoria> RespuestaCategorias { get; set; }

        public System.Data.Entity.DbSet<BaseDatosFisica.Models.ParaLasVistas.TestCurso_Pregunta> TestCurso_Pregunta { get; set; }

        public System.Data.Entity.DbSet<BaseDatosFisica.Models.Procedencia> Procedencias { get; set; }

        public System.Data.Entity.DbSet<BaseDatosFisica.Models.RespuestaEscrita> RespuestasEscritas { get; set; }

        public System.Data.Entity.DbSet<BaseDatosFisica.Models.RespuestaMarcada> RespuestasMarcadas { get; set; }

        public System.Data.Entity.DbSet<BaseDatosFisica.Models.TestEstudiante> TestsEstudiantes { get; set; }

        public System.Data.Entity.DbSet<BaseDatosFisica.Models.EstudiantePreguntaTest> EstudiantePreguntaTest { get; set; }
    }
}
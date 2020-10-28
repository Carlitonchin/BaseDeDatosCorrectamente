namespace BaseDatosFisica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newMigration : DbMigration
    {
        public override void Up()
        {   
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RespuestaEscritas", "TestEstudianteID", "dbo.TestEstudiantes");
            DropForeignKey("dbo.RespuestaMarcadas", "TestEstudianteID", "dbo.TestEstudiantes");
            DropForeignKey("dbo.RespuestaMarcadas", "RespuestaID", "dbo.Respuestas");
            DropForeignKey("dbo.RespuestaEscritas", "PreguntaID", "dbo.Preguntas");
            DropIndex("dbo.RespuestaMarcadas", new[] { "RespuestaID" });
            DropIndex("dbo.RespuestaMarcadas", new[] { "TestEstudianteID" });
            DropIndex("dbo.RespuestaEscritas", new[] { "PreguntaID" });
            DropIndex("dbo.RespuestaEscritas", new[] { "TestEstudianteID" });
            DropTable("dbo.RespuestaMarcadas");
            DropTable("dbo.RespuestaEscritas");
        }
    }
}

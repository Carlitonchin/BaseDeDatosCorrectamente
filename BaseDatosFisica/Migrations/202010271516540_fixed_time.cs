namespace BaseDatosFisica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixed_time : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EstudiantePreguntaTests", "Estudiante_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.EstudiantePreguntaTests", "Pregunta_PreguntaID", "dbo.Preguntas");
            DropForeignKey("dbo.EstudiantePreguntaTests", "Test_TestID", "dbo.Tests");
            DropIndex("dbo.EstudiantePreguntaTests", new[] { "Estudiante_Id" });
            DropIndex("dbo.EstudiantePreguntaTests", new[] { "Pregunta_PreguntaID" });
            DropIndex("dbo.EstudiantePreguntaTests", new[] { "Test_TestID" });
            AddColumn("dbo.EstudiantePreguntaTests", "IdEstudiante", c => c.String());
            AddColumn("dbo.EstudiantePreguntaTests", "TestID", c => c.Int(nullable: false));
            AddColumn("dbo.EstudiantePreguntaTests", "PreguntaID", c => c.Int(nullable: false));
            AddColumn("dbo.EstudiantePreguntaTests", "Inicio", c => c.DateTime(nullable: false));
            DropColumn("dbo.EstudiantePreguntaTests", "Estudiante_Id");
            DropColumn("dbo.EstudiantePreguntaTests", "Pregunta_PreguntaID");
            DropColumn("dbo.EstudiantePreguntaTests", "Test_TestID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EstudiantePreguntaTests", "Test_TestID", c => c.Int());
            AddColumn("dbo.EstudiantePreguntaTests", "Pregunta_PreguntaID", c => c.Int());
            AddColumn("dbo.EstudiantePreguntaTests", "Estudiante_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.EstudiantePreguntaTests", "Inicio");
            DropColumn("dbo.EstudiantePreguntaTests", "PreguntaID");
            DropColumn("dbo.EstudiantePreguntaTests", "TestID");
            DropColumn("dbo.EstudiantePreguntaTests", "IdEstudiante");
            CreateIndex("dbo.EstudiantePreguntaTests", "Test_TestID");
            CreateIndex("dbo.EstudiantePreguntaTests", "Pregunta_PreguntaID");
            CreateIndex("dbo.EstudiantePreguntaTests", "Estudiante_Id");
            AddForeignKey("dbo.EstudiantePreguntaTests", "Test_TestID", "dbo.Tests", "TestID");
            AddForeignKey("dbo.EstudiantePreguntaTests", "Pregunta_PreguntaID", "dbo.Preguntas", "PreguntaID");
            AddForeignKey("dbo.EstudiantePreguntaTests", "Estudiante_Id", "dbo.AspNetUsers", "Id");
        }
    }
}

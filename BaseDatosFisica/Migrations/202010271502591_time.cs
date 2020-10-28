namespace BaseDatosFisica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class time : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EstudiantePreguntaTests",
                c => new
                    {
                        EstudiantePreguntaTestID = c.Int(nullable: false, identity: true),
                        Estudiante_Id = c.String(maxLength: 128),
                        Pregunta_PreguntaID = c.Int(),
                        Test_TestID = c.Int(),
                    })
                .PrimaryKey(t => t.EstudiantePreguntaTestID)
                .ForeignKey("dbo.AspNetUsers", t => t.Estudiante_Id)
                .ForeignKey("dbo.Preguntas", t => t.Pregunta_PreguntaID)
                .ForeignKey("dbo.Tests", t => t.Test_TestID)
                .Index(t => t.Estudiante_Id)
                .Index(t => t.Pregunta_PreguntaID)
                .Index(t => t.Test_TestID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EstudiantePreguntaTests", "Test_TestID", "dbo.Tests");
            DropForeignKey("dbo.EstudiantePreguntaTests", "Pregunta_PreguntaID", "dbo.Preguntas");
            DropForeignKey("dbo.EstudiantePreguntaTests", "Estudiante_Id", "dbo.AspNetUsers");
            DropIndex("dbo.EstudiantePreguntaTests", new[] { "Test_TestID" });
            DropIndex("dbo.EstudiantePreguntaTests", new[] { "Pregunta_PreguntaID" });
            DropIndex("dbo.EstudiantePreguntaTests", new[] { "Estudiante_Id" });
            DropTable("dbo.EstudiantePreguntaTests");
        }
    }
}

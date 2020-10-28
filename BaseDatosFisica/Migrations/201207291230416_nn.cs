namespace BaseDatosFisica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TestCurso_Pregunta", "testCurso_TestCursoID", "dbo.TestCursoes");
            DropIndex("dbo.TestCurso_Pregunta", new[] { "testCurso_TestCursoID" });
            AddColumn("dbo.TestCurso_Pregunta", "test_TestID", c => c.Int());
            CreateIndex("dbo.TestCurso_Pregunta", "test_TestID");
            AddForeignKey("dbo.TestCurso_Pregunta", "test_TestID", "dbo.Tests", "TestID");
            DropColumn("dbo.TestCurso_Pregunta", "testCurso_TestCursoID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TestCurso_Pregunta", "testCurso_TestCursoID", c => c.Int());
            DropForeignKey("dbo.TestCurso_Pregunta", "test_TestID", "dbo.Tests");
            DropIndex("dbo.TestCurso_Pregunta", new[] { "test_TestID" });
            DropColumn("dbo.TestCurso_Pregunta", "test_TestID");
            CreateIndex("dbo.TestCurso_Pregunta", "testCurso_TestCursoID");
            AddForeignKey("dbo.TestCurso_Pregunta", "testCurso_TestCursoID", "dbo.TestCursoes", "TestCursoID");
        }
    }
}

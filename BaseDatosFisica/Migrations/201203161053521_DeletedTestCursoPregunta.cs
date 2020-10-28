namespace BaseDatosFisica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedTestCursoPregunta : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestCurso_Pregunta",
                c => new
                    {
                        TestCursoPreguntaID = c.Int(nullable: false, identity: true),
                        respuesta = c.String(),
                        pregunta_PreguntaID = c.Int(),
                        testCurso_TestCursoID = c.Int(),
                    })
                .PrimaryKey(t => t.TestCursoPreguntaID)
                .ForeignKey("dbo.Preguntas", t => t.pregunta_PreguntaID)
                .ForeignKey("dbo.TestCursoes", t => t.testCurso_TestCursoID)
                .Index(t => t.pregunta_PreguntaID)
                .Index(t => t.testCurso_TestCursoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestCurso_Pregunta", "testCurso_TestCursoID", "dbo.TestCursoes");
            DropForeignKey("dbo.TestCurso_Pregunta", "pregunta_PreguntaID", "dbo.Preguntas");
            DropIndex("dbo.TestCurso_Pregunta", new[] { "testCurso_TestCursoID" });
            DropIndex("dbo.TestCurso_Pregunta", new[] { "pregunta_PreguntaID" });
            DropTable("dbo.TestCurso_Pregunta");
        }
    }
}

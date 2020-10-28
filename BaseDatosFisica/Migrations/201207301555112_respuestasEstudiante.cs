namespace BaseDatosFisica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class respuestasEstudiante : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RespuestaEscritas",
                c => new
                    {
                        RespuestaEscritaID = c.Int(nullable: false, identity: true),
                        TestEstudianteID = c.Int(nullable: false),
                        PreguntaID = c.Int(nullable: false),
                        Respuesta = c.String(),
                    })
                .PrimaryKey(t => t.RespuestaEscritaID)
                .ForeignKey("dbo.Preguntas", t => t.PreguntaID, cascadeDelete: true)
                .ForeignKey("dbo.TestEstudiantes", t => t.TestEstudianteID, cascadeDelete: true)
                .Index(t => t.TestEstudianteID)
                .Index(t => t.PreguntaID);
            
            CreateTable(
                "dbo.RespuestaMarcadas",
                c => new
                    {
                        RespuestaMarcadaID = c.Int(nullable: false, identity: true),
                        TestEstudianteID = c.Int(nullable: false),
                        RespuestaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RespuestaMarcadaID)
                .ForeignKey("dbo.Respuestas", t => t.RespuestaID, cascadeDelete: true)
                .ForeignKey("dbo.TestEstudiantes", t => t.TestEstudianteID, cascadeDelete: true)
                .Index(t => t.TestEstudianteID)
                .Index(t => t.RespuestaID);
            
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

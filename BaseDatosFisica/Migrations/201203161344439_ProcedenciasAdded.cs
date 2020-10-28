namespace BaseDatosFisica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProcedenciasAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestEstudiantes",
                c => new
                    {
                        TestEstudianteID = c.Int(nullable: false, identity: true),
                        Id = c.String(maxLength: 128),
                        TestID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TestEstudianteID)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .ForeignKey("dbo.Tests", t => t.TestID, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.TestID);
            
            CreateTable(
                "dbo.Procedencias",
                c => new
                    {
                        ProcedenciaID = c.Int(nullable: false, identity: true),
                        valor = c.String(),
                    })
                .PrimaryKey(t => t.ProcedenciaID);
            
            AddColumn("dbo.AspNetUsers", "Nombre", c => c.String());
            AddColumn("dbo.AspNetUsers", "Apellidos", c => c.String());
            AddColumn("dbo.AspNetUsers", "ProcedenciaID", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "NumeroOpcion", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Promedio", c => c.Single(nullable: false));
            CreateIndex("dbo.AspNetUsers", "ProcedenciaID");
            AddForeignKey("dbo.AspNetUsers", "ProcedenciaID", "dbo.Procedencias", "ProcedenciaID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestEstudiantes", "TestID", "dbo.Tests");
            DropForeignKey("dbo.TestEstudiantes", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "ProcedenciaID", "dbo.Procedencias");
            DropIndex("dbo.AspNetUsers", new[] { "ProcedenciaID" });
            DropIndex("dbo.TestEstudiantes", new[] { "TestID" });
            DropIndex("dbo.TestEstudiantes", new[] { "Id" });
            DropColumn("dbo.AspNetUsers", "Promedio");
            DropColumn("dbo.AspNetUsers", "NumeroOpcion");
            DropColumn("dbo.AspNetUsers", "ProcedenciaID");
            DropColumn("dbo.AspNetUsers", "Apellidos");
            DropColumn("dbo.AspNetUsers", "Nombre");
            DropTable("dbo.Procedencias");
            DropTable("dbo.TestEstudiantes");
        }
    }
}

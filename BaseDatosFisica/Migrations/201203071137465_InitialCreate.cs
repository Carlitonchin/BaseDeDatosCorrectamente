namespace BaseDatosFisica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        CategoriaID = c.Int(nullable: false, identity: true),
                        CategoriaNombre = c.String(),
                    })
                .PrimaryKey(t => t.CategoriaID);
            
            CreateTable(
                "dbo.RespuestaCategorias",
                c => new
                    {
                        RespuestaCategoriaID = c.Int(nullable: false, identity: true),
                        RespuestaID = c.Int(nullable: false),
                        CategoriaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RespuestaCategoriaID)
                .ForeignKey("dbo.Categorias", t => t.CategoriaID, cascadeDelete: true)
                .ForeignKey("dbo.Respuestas", t => t.RespuestaID, cascadeDelete: true)
                .Index(t => t.RespuestaID)
                .Index(t => t.CategoriaID);
            
            CreateTable(
                "dbo.Respuestas",
                c => new
                    {
                        RespuestaID = c.Int(nullable: false, identity: true),
                        Texto = c.String(),
                        Imagen = c.String(),
                        PreguntaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RespuestaID)
                .ForeignKey("dbo.Preguntas", t => t.PreguntaID, cascadeDelete: true)
                .Index(t => t.PreguntaID);
            
            CreateTable(
                "dbo.Preguntas",
                c => new
                    {
                        PreguntaID = c.Int(nullable: false, identity: true),
                        Tipo = c.Boolean(nullable: false),
                        Nombre = c.String(),
                        Enunciado = c.String(),
                        Imagen = c.String(),
                        Tiempo = c.Int(nullable: false),
                        TiempoPrevio = c.Int(),
                        ImagenPrevia = c.String(),
                        EnunciadoPrevio = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.PreguntaID);
            
            CreateTable(
                "dbo.TestPreguntas",
                c => new
                    {
                        TestPreguntaID = c.Int(nullable: false, identity: true),
                        PreguntaID = c.Int(nullable: false),
                        TestID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TestPreguntaID)
                .ForeignKey("dbo.Preguntas", t => t.PreguntaID, cascadeDelete: true)
                .ForeignKey("dbo.Tests", t => t.TestID, cascadeDelete: true)
                .Index(t => t.PreguntaID)
                .Index(t => t.TestID);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        TestID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.TestID);
            
            CreateTable(
                "dbo.TestCursoes",
                c => new
                    {
                        TestCursoID = c.Int(nullable: false, identity: true),
                        CursoID = c.Int(nullable: false),
                        TestID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TestCursoID)
                .ForeignKey("dbo.Cursoes", t => t.CursoID, cascadeDelete: true)
                .ForeignKey("dbo.Tests", t => t.TestID, cascadeDelete: true)
                .Index(t => t.CursoID)
                .Index(t => t.TestID);
            
            CreateTable(
                "dbo.Cursoes",
                c => new
                    {
                        CursoID = c.Int(nullable: false, identity: true),
                        Anho = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CursoID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RespuestaCategorias", "RespuestaID", "dbo.Respuestas");
            DropForeignKey("dbo.TestPreguntas", "TestID", "dbo.Tests");
            DropForeignKey("dbo.TestCursoes", "TestID", "dbo.Tests");
            DropForeignKey("dbo.TestCursoes", "CursoID", "dbo.Cursoes");
            DropForeignKey("dbo.TestPreguntas", "PreguntaID", "dbo.Preguntas");
            DropForeignKey("dbo.Respuestas", "PreguntaID", "dbo.Preguntas");
            DropForeignKey("dbo.RespuestaCategorias", "CategoriaID", "dbo.Categorias");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.TestCursoes", new[] { "TestID" });
            DropIndex("dbo.TestCursoes", new[] { "CursoID" });
            DropIndex("dbo.TestPreguntas", new[] { "TestID" });
            DropIndex("dbo.TestPreguntas", new[] { "PreguntaID" });
            DropIndex("dbo.Respuestas", new[] { "PreguntaID" });
            DropIndex("dbo.RespuestaCategorias", new[] { "CategoriaID" });
            DropIndex("dbo.RespuestaCategorias", new[] { "RespuestaID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Cursoes");
            DropTable("dbo.TestCursoes");
            DropTable("dbo.Tests");
            DropTable("dbo.TestPreguntas");
            DropTable("dbo.Preguntas");
            DropTable("dbo.Respuestas");
            DropTable("dbo.RespuestaCategorias");
            DropTable("dbo.Categorias");
        }
    }
}

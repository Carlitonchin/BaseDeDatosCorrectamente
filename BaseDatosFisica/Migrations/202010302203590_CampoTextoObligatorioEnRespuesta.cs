namespace BaseDatosFisica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoTextoObligatorioEnRespuesta : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Respuestas", "Texto", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Respuestas", "Texto", c => c.String());
        }
    }
}

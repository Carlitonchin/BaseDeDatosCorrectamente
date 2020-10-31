namespace BaseDatosFisica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CamposObligatorios : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Preguntas", "Nombre", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Preguntas", "Nombre", c => c.String());
        }
    }
}

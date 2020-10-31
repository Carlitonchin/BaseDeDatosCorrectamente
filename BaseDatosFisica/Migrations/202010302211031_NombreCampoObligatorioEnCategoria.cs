namespace BaseDatosFisica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NombreCampoObligatorioEnCategoria : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categorias", "CategoriaNombre", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categorias", "CategoriaNombre", c => c.String());
        }
    }
}

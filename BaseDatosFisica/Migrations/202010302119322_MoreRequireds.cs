namespace BaseDatosFisica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreRequireds : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Preguntas", "Enunciado", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Preguntas", "Enunciado", c => c.String());
        }
    }
}

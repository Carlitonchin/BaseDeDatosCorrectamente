namespace BaseDatosFisica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoNulleableIntroduccionEnPreguntas : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Preguntas", "Introduccion", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Preguntas", "Introduccion", c => c.Boolean());
        }
    }
}

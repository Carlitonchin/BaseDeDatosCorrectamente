namespace BaseDatosFisica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tiempoAgotado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EstudiantePreguntaTests", "TiempoAgotado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EstudiantePreguntaTests", "TiempoAgotado");
        }
    }
}

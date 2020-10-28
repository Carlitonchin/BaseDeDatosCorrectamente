namespace BaseDatosFisica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestVisible : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tests", "Visible", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tests", "Visible");
        }
    }
}

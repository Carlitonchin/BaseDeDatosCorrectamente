namespace BaseDatosFisica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Roles : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO[dbo].[AspNetUsers]([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Nombre], [Apellidos], [ProcedenciaID], [NumeroOpcion], [Promedio]) VALUES(N'b434e112-592b-40f3-a88c-7baf363b7864', N'admin@web.encuesta', 0, N'AAeZfB0WSMAspevtZt1i4RkgJqMNj6oNCnShihv9HE+ibqdDv9k6KeJz2V/LmOCzfw==', N'3c0b029a-d4d4-4374-9cb4-036158a1a984', NULL, 0, 0, NULL, 1, 0, N'admin@web.encuesta', N'Julio', N'Vidal', 1, 0, 100)
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'bba14255-f5ec-4076-9749-79f531609a95', N'admin')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b434e112-592b-40f3-a88c-7baf363b7864', N'bba14255-f5ec-4076-9749-79f531609a95')
                ");
        }
        
        public override void Down()
        {
        }
    }
}

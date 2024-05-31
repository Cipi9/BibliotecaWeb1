namespace PA_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Book1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "ImageUrl");
        }
    }
}

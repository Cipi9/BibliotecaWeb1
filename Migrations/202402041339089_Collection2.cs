namespace PA_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Collection2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Collections", "IconUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Collections", "IconUrl");
        }
    }
}

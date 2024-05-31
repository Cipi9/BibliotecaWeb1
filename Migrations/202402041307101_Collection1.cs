namespace PA_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Collection1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Collections",
                c => new
                    {
                        CollectionId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CollectionId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Collections", "UserId", "dbo.Users");
            DropIndex("dbo.Collections", new[] { "UserId" });
            DropTable("dbo.Collections");
        }
    }
}

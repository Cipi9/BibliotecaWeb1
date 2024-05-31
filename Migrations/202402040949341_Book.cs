namespace PA_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Book : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                {
                    BookId = c.Int(nullable: false, identity: true),
                    UserId = c.Int(),
                    Title = c.String(),
                    Author = c.String(),
                    Description = c.String(),
                    Genre = c.String(),
                    PublicationYear = c.Long(),
                })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Books", "UserId", "dbo.Users");
            DropIndex("dbo.Books", new[] { "UserId" });
            DropTable("dbo.Books");
        }
    }
}


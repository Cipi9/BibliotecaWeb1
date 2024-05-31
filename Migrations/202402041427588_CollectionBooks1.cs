namespace PA_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CollectionBooks1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CollectionBooks", "BOOK_BookId", "dbo.Books");
            DropForeignKey("dbo.CollectionBooks", "USER_UserId", "dbo.Users");
            DropIndex("dbo.CollectionBooks", new[] { "BOOK_BookId" });
            DropIndex("dbo.CollectionBooks", new[] { "USER_UserId" });
            DropColumn("dbo.CollectionBooks", "BOOK_BookId");
            DropColumn("dbo.CollectionBooks", "USER_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CollectionBooks", "USER_UserId", c => c.Int());
            AddColumn("dbo.CollectionBooks", "BOOK_BookId", c => c.Int());
            CreateIndex("dbo.CollectionBooks", "USER_UserId");
            CreateIndex("dbo.CollectionBooks", "BOOK_BookId");
            AddForeignKey("dbo.CollectionBooks", "USER_UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.CollectionBooks", "BOOK_BookId", "dbo.Books", "BookId");
        }
    }
}

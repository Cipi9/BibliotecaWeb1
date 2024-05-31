namespace PA_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CollectionBooks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CollectionBooks",
                c => new
                    {
                        CollectionBooksId = c.Int(nullable: false, identity: true),
                        CollectionId = c.Int(nullable: false),
                        BooksId = c.Int(nullable: false),
                        BOOK_BookId = c.Int(),
                        USER_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.CollectionBooksId)
                .ForeignKey("dbo.Books", t => t.BOOK_BookId)
                .ForeignKey("dbo.Users", t => t.USER_UserId)
                .Index(t => t.BOOK_BookId)
                .Index(t => t.USER_UserId);
            
            AddColumn("dbo.Books", "Review", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CollectionBooks", "USER_UserId", "dbo.Users");
            DropForeignKey("dbo.CollectionBooks", "BOOK_BookId", "dbo.Books");
            DropIndex("dbo.CollectionBooks", new[] { "USER_UserId" });
            DropIndex("dbo.CollectionBooks", new[] { "BOOK_BookId" });
            DropColumn("dbo.Books", "Review");
            DropTable("dbo.CollectionBooks");
        }
    }
}

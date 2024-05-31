namespace PA_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateBooksTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO dbo.Books (Title, Author, Description, Genre, PublicationYear, UserId) VALUES ('Sample Title', 'Sample Author', 'Sample Description', 'Sample Genre', 2022, 1)");
            Sql("INSERT INTO dbo.Books (Title, Author, Description, Genre, PublicationYear, UserId) VALUES ('Sample Title', 'Sample Author', 'Sample Description', 'Sample Genre', 2022, 1)");
            Sql("INSERT INTO dbo.Books (Title, Author, Description, Genre, PublicationYear, UserId) VALUES ('Sample Title', 'Sample Author', 'Sample Description', 'Sample Genre', 2022, 1)");
            Sql("INSERT INTO dbo.Books (Title, Author, Description, Genre, PublicationYear, UserId) VALUES ('Sample Title', 'Sample Author', 'Sample Description', 'Sample Genre', 2022, 1)");
            Sql("INSERT INTO dbo.Books (Title, Author, Description, Genre, PublicationYear, UserId) VALUES ('Sample Title', 'Sample Author', 'Sample Description', 'Sample Genre', 2022, 1)");
        }
        
        public override void Down()
        {
        }
    }
}

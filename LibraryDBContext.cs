using PA_project.Models;
using System.Data.Entity;


namespace PA_project
{
    public class LibraryDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Review> Reviews { get; set; }  
        public DbSet<CollectionBooks> CollectionBooks { get; set; }
        public LibraryDBContext() { }
    }
}
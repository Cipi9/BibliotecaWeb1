using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PA_project.Models
{
    public class Book
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private int bookId;
        [Required]
        private string title;
        [Required]
        private string author;
        [Required]
        private string description;
        [Required]
        private string genre;
        [Required]
        private long publicationYear;

        private string imageUrl;

        private string review;

        private int userId;

        public Book() { }


        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public long PublicationYear { get; set; }
        public string ImageUrl { get; set; }

        public string Review { get; set; }
        public int UserId { get; set; }

        public virtual User USER { get; set; }
    }
}